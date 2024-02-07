/*
* Author:	Iris Bermudez
* Date:		08/12/2023
*/



using System;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Cards;



namespace Solitaire.Gameplay.Spider {
    public class SpiderGameMode : AbstractGameMode {
        #region Variables
        [SerializeField]
        private List<AbstractCardContainer> completedColumnContainers; 
        #endregion


        #region Public methods
        public override void Initialize() {
            cards = deckController.InitializeCards(suits, amountOfEachSuit);

            List<CardFacade> auxCards = new List<CardFacade>();

            foreach (CardFacade auxCard in cards) {
                auxCards.Add(auxCard);
            }

            foreach (AbstractCardContainer auxCardContainer in cardContainers) {
                auxCards = auxCardContainer.Initialize(auxCards);
            }

            deckController.SubscribeToDragStartEvent( ValidateCardDragging );
            deckController.SubscribeToCardPlacedEventWithCollision( 
                                                ValidateCardPlacementWithCollison );
            deckController.SubscribeToCardPlacedEventWithoutCollision( 
                                                ValidateCardPlacementWithoutCollison );
        }


        public override void SubscribeToOnGameClearedEvent(
                                                    EventHandler eventHandler) {
            deckController.onCardsCleared += eventHandler;
        }


        public void ValidateCardDragging( CardFacade _card ) {
            bool canBeDragged = CanBeDragged(_card);

            _card.SetCanBeDragged( canBeDragged );

            if( canBeDragged ) {
                // Deactivating childs physics to avoid the parent to detect them during dragging
                _card.ActivateChildsPhysics(false);
            }
        }


        public void DistributorCardsDistribution( AbstractCardContainer _cardContainer ) {
            Debug.Log("Distributing cards.");

            List<CardFacade> auxCardsToDistribute = _cardContainer.GetCards();

            for( int i = auxCardsToDistribute.Count - 1; i >= 0; i-- ) {
                auxCardsToDistribute[i].RenderOnTop();
                auxCardsToDistribute[i].FlipCard( true );

                // Setting up parenting
                auxCardsToDistribute[i].ParentCard = cardContainers[i].GetTopCard();
                cardContainers[i].GetTopCard().ChildCard = auxCardsToDistribute[i];

                cardContainers[i].AddCard( auxCardsToDistribute[i] );
                _cardContainer.RemoveCard( auxCardsToDistribute[i] );

            }

            Destroy( _cardContainer.gameObject );
        }
        #endregion


        #region Protected methods
        protected override void ValidateCardPlacementWithCollison( CardFacade _placedCard,
                                                        GameObject _detectedGameObject ) {           

            if ( _detectedGameObject.layer == LayerMask.NameToLayer( cardsLayer ) ) {
                CardFacade detectedCardFacade = _detectedGameObject.GetComponent<CardFacade>();

                if ( !detectedCardFacade )
                    throw new Exception($"The object {_detectedGameObject.name} doesn't have a "
                                                                + $"CardFacade component.");

                // Logic to move card from one container to another
                // Case: Card CANNOT be child of potential parent                
                if (!CanBeChildOf(_placedCard, detectedCardFacade)  
                                        ||   _placedCard.ParentCard == detectedCardFacade ) {
                    Debug.Log("!CanBeChildOf");
                    _placedCard.transform.position = GetCardOriginalPositionInContainer(_placedCard);


                    // Recursively set child cards to old position
                    var auxCardChild = _placedCard;

                    while (auxCardChild != null) {
                        auxCardChild.transform.position = GetCardOriginalPositionInContainer(
                                                                                        auxCardChild);
                        auxCardChild = auxCardChild.ChildCard;
                    }
                    
                   
                // Case: Card CAN be child of potential parent
                } else {
                    // 1- Removing from previous Parent and setting detected card as new parent's child
                    if (_placedCard.ParentCard != null) {
                        _placedCard.ParentCard.ChildCard = null;
                    }

                    detectedCardFacade.ChildCard = _placedCard;
                    _placedCard.ParentCard = detectedCardFacade;

                    // 2- Get parent card container
                    AbstractCardContainer parentCardContainer = GetCardContainer( detectedCardFacade );

                    // Recursively check childs
                    var auxCardFacade = _placedCard;

                    while (auxCardFacade != null) {
                        // 3- Remove card from its card container
                        GetCardContainer(auxCardFacade).RemoveCard(auxCardFacade);

                        // 4- Add card to new card container
                        parentCardContainer.AddCard(auxCardFacade);

                        // 5- Set ChildCard as card to check on next loop
                        auxCardFacade = auxCardFacade.ChildCard;
                    }
                }

                // Activating droped card childs physics again after dragging ends


            // Case: The detected GameObject is a CardContainer
            } else if ( _detectedGameObject.layer == LayerMask.NameToLayer( cardContainersLayer ) ) {
                Debug.Log("Adding card to Card container");
                
                var detectedCardContainer = _detectedGameObject
                                                        .GetComponent<AbstractCardContainer>();

                if( !detectedCardContainer )
                    throw new Exception($"The object {_detectedGameObject.name} doesn't have an "
                                                            + $"AbstractCardContainer component.");
                
                if(_placedCard.ParentCard )
                    _placedCard.ParentCard.ChildCard = null;

                _placedCard.ParentCard = null;
                GetCardContainer(_placedCard).RemoveCard(_placedCard);
                detectedCardContainer.AddCard(_placedCard);                

                var auxChild = _placedCard.ChildCard;

                while( auxChild ) {
                    GetCardContainer(auxChild).RemoveCard(auxChild);

                    detectedCardContainer.AddCard( auxChild );
                    auxChild = auxChild.ChildCard;
                }


            } else {
                throw new Exception($"The object {_detectedGameObject.name}'s layer ("
                    + LayerMask.LayerToName( _detectedGameObject.layer ) + ") is not valid." );
            
            }
            
            
            _placedCard.ActivateChildsPhysics(true);
            CheckIfColumnWasCompleted( _placedCard );            
        }



        protected override void ValidateCardPlacementWithoutCollison( CardFacade _card ) {
            // Logic to make card return to previous position
            _card.transform.position = GetCardOriginalPositionInContainer( _card );

            if( _card.ChildCard != null ) {
                Debug.Log("Has a child");
                CardFacade auxCard = _card.ChildCard;

                while( auxCard != null ) {
                    Debug.Log("Has multiple childs");
                    auxCard.transform.position = GetCardOriginalPositionInContainer(auxCard);

                    auxCard = auxCard.ChildCard;
                }

            } else {
                Debug.Log("Does not have a child");
            }

            // Activating droped card childs physics again after dragging ends
            _card.ActivateChildsPhysics( true );
        }


        protected override bool CanBeChildOf( CardFacade _card, CardFacade _potentialParent ) {
            return _potentialParent.GetCardNumber() == _card.GetCardNumber() + 1;
        }


        protected override bool CanBeDragged( CardFacade _card ) {
            if( !_card.ChildCard )
                return true;


            CardFacade auxCard = _card;

            while( auxCard.ChildCard ) {
                if( !CanBeChildOf( auxCard.ChildCard, auxCard )
                            ||  !auxCard.GetSuit().Equals(auxCard.ChildCard.GetSuit() ) ) {
                    return false;
                }

                auxCard = auxCard.ChildCard;
            }

            return true;
        }
        #endregion



        #region Private methods
        private void CheckIfColumnWasCompleted( CardFacade _placedCard ) {
            if( IsColumnCompleted( _placedCard ) )
                MoveColumnToCompletedColumnContainer( _placedCard );
        }


        private bool IsColumnCompleted( CardFacade _card ) {
            Debug.Log("CheckIfColumnWasCompleted");
            int auxCardNumber = _card.GetCardNumber();
            string placedCardSuit = _card.GetSuit();


            /*
            * Check parents one by one until finding a King or the column is
            */
            CardFacade auxCard = _card.ParentCard;

            while (auxCard) {
                if (auxCard.GetCardNumber() == 13) {
                    auxCardNumber = 13;
                    auxCard = null;

                } else if (auxCard.GetSuit().Equals(placedCardSuit)
                            && auxCard.GetCardNumber() == auxCardNumber + 1) {
                    auxCardNumber = auxCard.GetCardNumber();
                    auxCard = auxCard.ParentCard;

                } else {
                    Debug.Log("CheckIfColumnWasCompleted: Invalid parent");
                    Debug.Log($"Previous checked card number: {auxCardNumber}. "
                            + $"Current card number: {auxCard.GetCardNumber()}. "
                            + $"Expected number: {auxCardNumber + 1}.");

                    return false;
                }
            }


            /*
            * If greatest parent is not a king abort check up
            */
            if (auxCardNumber != 13) {
                Debug.Log("CheckIfColumnWasCompleted: biggest parent is "
                                                                + auxCardNumber);

                return false;
            }


            /*
            * Check childs one by one
            */
            auxCardNumber = _card.GetCardNumber();
            auxCard = _card.ChildCard;

            while (auxCard) {
                if (auxCard.GetSuit().Equals(placedCardSuit)
                            && auxCard.GetCardNumber() == auxCardNumber - 1) {
                    auxCardNumber = auxCard.GetCardNumber();
                    auxCard = auxCard.ChildCard;

                } else {
                    Debug.Log("CheckIfColumnWasCompleted: invalid child");
                    Debug.Log("CheckIfColumnWasCompleted: Invalid parent");
                    Debug.Log($"Previous checked card number: {auxCardNumber}. "
                            + $"Current card number: {auxCard.GetCardNumber()}. "
                            + $"Expected number: {auxCardNumber - 1}.");

                    return false;
                }
            }


            /*
            *  If smallest child is not an as abort check up
            */
            if (auxCardNumber != 1) {
                Debug.Log("CheckIfColumnWasCompleted: smallest child is not an as");

                return false;
            }


            /*
            * Move column from SpiderCardContainer to CompletedCardContainer
            */
            return true;
        }
        
        
        private void MoveColumnToCompletedColumnContainer( List<CardFacade> _cards ) {
            completedColumnContainers[completedColumnContainers.Count - 1]
                                                                .AddCards( _cards );
            completedColumnContainers.RemoveAt( completedColumnContainers.Count - 1 );
        }


        private List<CardFacade> GetCardColumn( CardFacade _card ) {
            List<CardFacade> columnOfCards = new List<CardFacade>();
            CardFacade auxCard = _card;

            while( auxCard ) {
                if(  auxCard.GetCardNumber() + 1  
                                    ==  auxCard.ParentCard.GetCardNumber()  ) {
                    auxCard = auxCard.ParentCard;

                } else {
                    break;
                }
            }


            while( auxCard ) {
                if( auxCard.ChildCard != null  
                                            &&  auxCard.GetCardNumber() + 1 == 
                                                auxCard.ChildCard.GetCardNumber()  ) {
                    columnOfCards.Add( auxCard );

                } else {
                    break;
                }
            }


            return columnOfCards;
        }
        #endregion
    }
}
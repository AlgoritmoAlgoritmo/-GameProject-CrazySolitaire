/*
* Author:	Iris Bermudez
* Date:		08/12/2023
*/



using System;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Gameplay.Cards;
using Solitaire.Gameplay.CardContainers;



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


        public override void SubscribeToOnGameClearedEvent( EventHandler eventHandler ) {
            deckController.onCardsCleared += eventHandler;
        }


        public void ValidateCardDragging( CardFacade _card ) {
            bool canBeDragged = CanBeDragged( _card );

            _card.SetCanBeDragged( canBeDragged );

            if( canBeDragged ) {
                // Deactivating childs physics to avoid the parent to detect them during dragging
                _card.ActivateChildsPhysics(false);
            }
        }


        public void DistributorCardsDistribution( AbstractCardContainer _cardContainer ) {
            List<CardFacade> auxCardsToDistribute = _cardContainer.GetCards();

            for( int i = auxCardsToDistribute.Count - 1; i >= 0; i-- ) {
                auxCardsToDistribute[i].RenderOnTop();
                auxCardsToDistribute[i].FlipCard( true );

                // Setting up parenting
                auxCardsToDistribute[i].SetParentCard( cardContainers[i].GetTopCard() );

                if( cardContainers[i].GetTopCard() )
                    cardContainers[i].GetTopCard().SetChildCard( auxCardsToDistribute[i] );

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
                        _placedCard.ParentCard.SetChildCard( null );
                    }

                    detectedCardFacade.SetChildCard( _placedCard );
                    _placedCard.SetParentCard( detectedCardFacade );

                    // 2- Get parent card container
                    AbstractCardContainer parentCardContainer = GetCardContainer( detectedCardFacade );

                    // Recursively check childs
                    var auxCardFacade = _placedCard;

                    while (auxCardFacade != null) {
                        // 3- Remove card from its card container
                        GetCardContainer( auxCardFacade ).RemoveCard( auxCardFacade );

                        // 4- Add card to new card container
                        parentCardContainer.AddCard( auxCardFacade );

                        // 5- Set ChildCard as card to check on next loop
                        auxCardFacade = auxCardFacade.ChildCard;
                    }
                }

                // Activating droped card childs physics again after dragging ends


            // Case: The detected GameObject is a CardContainer
            } else if ( _detectedGameObject.layer == LayerMask.NameToLayer( 
                                                                        cardContainersLayer ) ) {
                Debug.Log("Adding card to Card container");
                
                var detectedCardContainer = _detectedGameObject
                                                        .GetComponent<AbstractCardContainer>();

                if( !detectedCardContainer )
                    throw new Exception($"The object {_detectedGameObject.name} doesn't have an "
                                                            + $"AbstractCardContainer component.");
                
                if(_placedCard.ParentCard )
                    _placedCard.ParentCard.SetChildCard( null );

                _placedCard.SetParentCard( null );
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
                CardFacade auxCard = _card.ChildCard;

                while( auxCard != null ) {
                    auxCard.transform.position = GetCardOriginalPositionInContainer(auxCard);

                    auxCard = auxCard.ChildCard;
                }

            } else {
                
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
            List<CardFacade> columnOfCards = GetCardColumn(_placedCard);

            if ( IsColumnCompleted( columnOfCards ) ) {
                MoveColumnToCompletedColumnContainer( columnOfCards );
                deckController.RemoveCardsFromGame( columnOfCards );
            }
        }


        private bool IsColumnCompleted( List<CardFacade> _columnOfCards ) {
            return     _columnOfCards.Count == 13
                    && _columnOfCards[0].GetCardNumber() == 13
                    && _columnOfCards[12].GetCardNumber() == 1;
        }
        
        
        private void MoveColumnToCompletedColumnContainer( List<CardFacade> _cards ) {
            AbstractCardContainer auxCardContainer = GetCardContainer( _cards[0] );
            _cards[0].SetParentCard( null );

            foreach ( CardFacade auxCard in _cards ) {
                auxCard.ActivatePhysics(false);
                auxCard.SetCanBeDragged(false);
            }

            auxCardContainer.RemoveCards( _cards );

            completedColumnContainers[completedColumnContainers.Count - 1]
                                                                .AddCards( _cards );
            completedColumnContainers.RemoveAt( completedColumnContainers.Count - 1 );
        }


        private List<CardFacade> GetCardColumn( CardFacade _card ) {
            List<CardFacade> columnOfCards = new List<CardFacade>();
            CardFacade auxCard = _card;

            while( auxCard ) {
                if( auxCard.ParentCard  
                            && auxCard.GetCardNumber() != 13
                            && auxCard.GetCardNumber() + 1  ==  auxCard.ParentCard.
                                                                GetCardNumber()  ) {
                    auxCard = auxCard.ParentCard;

                } else {
                    break;
                }
            }


            while( auxCard ) {
                if(  auxCard.GetCardNumber() == 1  ) {
                    columnOfCards.Add( auxCard );


                } if(  auxCard.GetCardNumber() != 1
                                            &&  auxCard.ChildCard != null  
                                            &&  auxCard.GetCardNumber() == 
                                                auxCard.ChildCard.GetCardNumber() + 1 ) {
                    columnOfCards.Add( auxCard );
                    auxCard = auxCard.ChildCard;

                } else {
                    break;
                }
            }

            return columnOfCards;
        }
        #endregion
    }
}
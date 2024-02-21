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
        public override void Initialize(List<CardFacade> _cards) {
            List<CardFacade> auxCards = new List<CardFacade>();

            foreach (CardFacade auxCard in _cards) {
                auxCards.Add(auxCard);
                auxCard.SubscribeToOnStartDragging(ValidateCardDragging);
                auxCard.SubscribeToCardEvent(ManageCardEvent);
            }

            foreach (AbstractCardContainer auxCardContainer in cardContainers) {
                auxCards = auxCardContainer.Initialize(auxCards);
            }
        }


        public void ValidateCardDragging( CardFacade _card ) {
            bool canBeDragged = CanCardBeDragged(_card);
            _card.SetCanBeDragged(canBeDragged);

            if( canBeDragged ) {
                Debug.Log( "Deactivating childs physics." );
                // Deactivating childs physics to avoid the parent to detect them
                // during dragging
                _card.ActivateChildsPhysics( false );
                _card.ActivatePhysics( true );
            }
        }


        public void DistributeCardsBetweenCardContainers(
                                            AbstractCardContainer _cardContainer) {
            List<CardFacade> auxCardsToDistribute = _cardContainer.GetCards();

            for (int i = auxCardsToDistribute.Count - 1; i >= 0; i--) {
                cardContainers[i].AddCard(auxCardsToDistribute[i]);
            }

            Destroy(_cardContainer.gameObject);
        }
        #endregion


        #region Protected methods
        protected override void ManageCardEvent(CardFacade _placedCard,
                                                        GameObject _detectedGameObject) {
            //  CASE: no object colliding
            if (_detectedGameObject is null) {
                GetCardContainer(_placedCard).Refresh();

                // Activating droped card childs physics again after dragging ends
                _placedCard.ActivateChildsPhysics(true);


            //  CASE: colliding object is a Card
            } else if (_detectedGameObject.layer == LayerMask.NameToLayer(cardsLayer)) {
                CardFacade detectedCardFacade = _detectedGameObject
                                                            .GetComponent<CardFacade>();
                if (!detectedCardFacade)
                    throw new Exception($"The object {_detectedGameObject.name} doesn't"
                                                + $" have a CardFacade component.");

                // Logic to move card from one container to another
                // Case: Card CANNOT be child of potential parent                
                if (!CanBeChildOf(_placedCard, detectedCardFacade)
                                    || _placedCard.ParentCard == detectedCardFacade) {
                    GetCardContainer(_placedCard).Refresh();

                    // Case: Card CAN be child of potential parent
                } else {
                    MoveCardToNewContainer(_placedCard,
                                                GetCardContainer(detectedCardFacade));
                }


            //  CASE:  detected object is a CardContainer
            } else if (_detectedGameObject.layer == LayerMask.NameToLayer(
                                                                cardContainersLayer)) {
                   var detectedCardContainer = _detectedGameObject
                                                    .GetComponent<AbstractCardContainer>();
                   if (!detectedCardContainer)
                        throw new Exception($"The object {_detectedGameObject.name} "
                                    + $"doesn't have an AbstractCardContainer component.");

                   MoveCardToNewContainer(_placedCard, detectedCardContainer);


            //  CASE: detected object isn't a Card nor a CardContainer
            } else {
                throw new Exception($"The object {_detectedGameObject.name}'s layer ("
                                    + LayerMask.LayerToName(_detectedGameObject.layer)
                                    + ") is not valid.");
            }



            _placedCard.ActivateChildsPhysics( true );
            CheckIfColumnWasCompleted( _placedCard );
        }


        protected override bool CanBeChildOf( CardFacade _card,
                                                CardFacade _potentialParent ) {
            Debug.Log( "CanBeChildOf " + _card.GetCardNumber() 
                        + " " + _potentialParent.GetCardNumber() );

            return _potentialParent.GetCardNumber() == _card.GetCardNumber() + 1;
        }


        protected override bool CanCardBeDragged( CardFacade _card ) {
            if ( !_card.ChildCard ) {
                return true;
            }

            CardFacade auxCard = _card;

            while (auxCard.ChildCard) {
                if( !CanBeChildOf(auxCard.ChildCard, auxCard)
                            || !auxCard.GetSuit().Equals(auxCard.ChildCard.GetSuit())) {
                    return false;
                }

                auxCard = auxCard.ChildCard;
            }


            return true;
        }
        #endregion



        #region Private methods
        private void MoveCardToNewContainer(CardFacade _card,
                                                AbstractCardContainer _cardContainer) {
            // Recursively check childs
            var auxCardFacade = _card;

            while (auxCardFacade != null) {
                // 1- Remove card from its card container
                GetCardContainer(auxCardFacade).RemoveCard(auxCardFacade);

                // 2- Add card to new card container
                _cardContainer.AddCard(auxCardFacade);

                // 3- Set ChildCard as card to check on next loop
                auxCardFacade = auxCardFacade.ChildCard;
            }
        }


        private void CheckIfColumnWasCompleted(CardFacade _placedCard) {
            List<CardFacade> columnOfCards = GetCardColumn(_placedCard);

            if (IsColumnCompleted(columnOfCards)) {
                MoveColumnToCompletedColumnContainer(columnOfCards);
                OnCardsCleared.Invoke(columnOfCards);
            }
        }


        private bool IsColumnCompleted(List<CardFacade> _columnOfCards) {
            return _columnOfCards.Count == 13
                    && _columnOfCards[0].GetCardNumber() == 13
                    && _columnOfCards[12].GetCardNumber() == 1;
        }


        private void MoveColumnToCompletedColumnContainer(List<CardFacade> _cards) {
            AbstractCardContainer auxCardContainer = GetCardContainer(_cards[0]);
            _cards[0].SetParentCard(null);

            foreach (CardFacade auxCard in _cards) {
                auxCard.ActivatePhysics(false);
                auxCard.SetCanBeDragged(false);
            }

            auxCardContainer.RemoveCards(_cards);

            completedColumnContainers[completedColumnContainers.Count - 1]
                                                                .AddCards(_cards);
            completedColumnContainers.RemoveAt(completedColumnContainers.Count - 1);
        }


        private List<CardFacade> GetCardColumn(CardFacade _card) {
            List<CardFacade> columnOfCards = new List<CardFacade>();
            CardFacade auxCard = _card;

            while (auxCard) {
                if (auxCard.ParentCard
                            && auxCard.GetCardNumber() != 13
                            && auxCard.GetCardNumber() + 1 == auxCard.ParentCard.
                                                                GetCardNumber()) {
                    auxCard = auxCard.ParentCard;

                } else {
                    break;
                }
            }


            while (auxCard) {
                if (auxCard.GetCardNumber() == 1) {
                    columnOfCards.Add(auxCard);


                }
                if (auxCard.GetCardNumber() != 1
                                          && auxCard.ChildCard != null
                                          && auxCard.GetCardNumber() ==
                                              auxCard.ChildCard.GetCardNumber() + 1) {
                    columnOfCards.Add(auxCard);
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
/*
* Author:	Iris Bermudez
* Date:		11/06/2024
*/



using System;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Gameplay;
using Solitaire.Gameplay.Cards;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Common;

namespace Solitaire.GameModes.Klondike {
    public class KlondikeGameMode : AbstractGameMode {
        #region Variables
        #endregion


        #region Public methods
        public override void Initialize(List<CardFacade> _cards) {
            if (_cards.Contains(null)) {
                throw new NullReferenceException("The list of cards passed for "
                                        + "initialization contains a null element.");

            } else if (_cards.Count < 1) {
                throw new IndexOutOfRangeException("The list of cards passed for "
                                                    + "initialization is empty.");
            }

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


        public override void ValidateCardDragging( CardFacade _card ) {
            bool canBeDragged = CanCardBeDragged(_card);
            _card.SetCanBeDragged(canBeDragged);

            if (canBeDragged) {
                Debug.Log("Deactivating childs physics.");
                // Deactivating childs physics to avoid the parent to detect them
                // during dragging
                _card.ActivateChildsPhysics(false);
                _card.ActivatePhysics(true);
                _card.OnValidDrag?.Invoke();

            } else {
                _card.OnInvalidDrag?.Invoke();
            }
        }
        #endregion


        #region Protected methods
        protected override bool CanBeChildOf(CardFacade _card, CardFacade _potentialParent) {
            return _potentialParent.GetCardNumber() == _card.GetCardNumber() + 1
                        &&  !_potentialParent.GetColor().Equals( _card.GetColor() );
        }
    

        protected override bool CanCardBeDragged(CardFacade _card) {
            if (!_card.IsFacingUp()) {
                return false;
            }

            if (!_card.ChildCard) {
                return true;
            }

            // Check every single child card
            CardFacade auxCard = _card;

            while (auxCard.ChildCard) {
                // Check if card number and suit are incorrect
                if ( !CanBeChildOf(auxCard.ChildCard, auxCard)
                            || auxCard.GetColor().Equals(auxCard.ChildCard.GetColor())) {

                    return false;
                }

                auxCard = auxCard.ChildCard;
            }

            // Passed child checking so return true
            return true;
        }


        protected override void ManageCardEvent( CardFacade _placedCard, GameObject _detectedGameObject ) {
            if (_detectedGameObject is null) {
                GetCardContainer(_placedCard).Refresh();
                _placedCard.OnInvalidDrag?.Invoke();

            //  CASE: colliding object is a Card
            } else if (_detectedGameObject.layer == LayerMask.NameToLayer( Constants.CARDS_LAYER_NAME ) ) {
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
                    _placedCard.OnInvalidDrop?.Invoke();

                // Case: Card CAN be child of potential parent
                } else {
                    MoveCardToNewContainer(_placedCard, GetCardContainer(detectedCardFacade));

                    _placedCard.OnValidDrop?.Invoke();
                }

            //  CASE: colliding object is a KlondikeCompletedColumnContainer
            } else if (_detectedGameObject.layer == LayerMask.NameToLayer( Constants.CARD_CONTAINERS_LAYER_NAME ) ) {
                // IF PLACED CARD IS A KING
                if( _placedCard.GetCardNumber() == 13 ) {
                    MoveCardToNewContainer( _placedCard,
                                            _detectedGameObject.GetComponent<AbstractCardContainer>() );
                    _placedCard.OnValidDrop?.Invoke();

                // ELSE RESET CARD POSITION
                } else {
                    GetCardContainer(_placedCard).Refresh();
                    _placedCard.OnInvalidDrop?.Invoke();
                }
            }
        }
        #endregion


        #region Private methods
        private void MoveCardToNewContainer(CardFacade _card, AbstractCardContainer _cardContainer) {
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
        #endregion
    }
}
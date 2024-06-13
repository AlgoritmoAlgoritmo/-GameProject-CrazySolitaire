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


        public override void ValidateCardDragging(CardFacade _card) {
            bool canBeDragged = CanCardBeDragged(_card);
            Debug.Log("canBeDragged " + canBeDragged);
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
            return _potentialParent.GetCardNumber() == _card.GetCardNumber() + 1;
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
                            || !auxCard.GetSuit().Equals(auxCard.ChildCard.GetSuit())) {

                    return false;
                }

                auxCard = auxCard.ChildCard;
            }

            // Passed child checking so return true
            return true;
        }

        protected override void ManageCardEvent(CardFacade _placedCard, GameObject _detectedGameObject) {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
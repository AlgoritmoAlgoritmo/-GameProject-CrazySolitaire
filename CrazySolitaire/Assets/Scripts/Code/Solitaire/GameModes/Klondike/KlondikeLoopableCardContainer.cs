/*
* Author:	Iris Bermudez
* Date:		13/06/2024
*/



using System.Collections.Generic;
using UnityEngine;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;



namespace Solitaire.GameModes.Klondike {
	public class KlondikeLoopableCardContainer : AbstractCardContainer {
		#region Variables
		[SerializeField]
		private RectTransform cardDisplayPosition;

		private List<CardFacade> displayedCards = new List<CardFacade>();
		private short currentDisplayedCard = -1;
		#endregion


		#region Public methods
		public override List<CardFacade> Initialize(List<CardFacade> _cards) {
			if (_cards == null || _cards.Count == 0) {
				throw new System.Exception("Cards list is empty.");

			} else if (_cards.Contains(null)) {
				throw new System.NullReferenceException("There's a null element in the list of cards"
														+ " passed for initialization.");

			} else if (_cards.Count < initialCardsAmount) {
				throw new System.Exception("There aren't enough cards to initialize CardContainer. "
											+ $"It was expected to {initialCardsAmount} but received"
											+ $" {_cards.Count} instead. ");
			}

			return AddInitializationCards(_cards);
		}

		public void ShowNextCard() {
			currentDisplayedCard++;

			if( currentDisplayedCard >= 0 ) {
				if( currentDisplayedCard >= cards.Count ) {
					currentDisplayedCard = -1;

					foreach( var auxCard in cards ) {
						HideCard( auxCard );
                    }

				} else {
					DisplayCard( cards[currentDisplayedCard] );
                }
            } 
        }

		public override void AddCard(CardFacade _card) {
			throw new System.NotImplementedException();
		}

		public override bool AddCards(List<CardFacade> _cards) {
			throw new System.NotImplementedException();
		}
		
		public override void RemoveCard(CardFacade _card) {
			if (!_card) {
				throw new System.NullReferenceException("The card intended to be removed is "
															+ "null.");
			}

			cards.Remove(_card);
			displayedCards.Remove(_card);
			Refresh();
		}

		public override void RemoveCards(List<CardFacade> _cards) {
			throw new System.NotImplementedException();
		}

		public override void Refresh() {
			if( cards.Count > 0 ) {
				int index = 0;

				foreach( CardFacade auxCard in cards ) {
					auxCard.transform.position = GetCardPosition( index );
					auxCard.RenderOnTop();
					index++;
				}
			}
		}

		#endregion


		#region Protected methods
		protected override Vector2 GetCardPosition( int _index ) {
			if( displayedCards.Contains( cards[_index] ) ) {
				return cardDisplayPosition.position;

			} else {
				return transform.position;
			}
		}

		protected override void SetUpStarterCards() {
			short cardIndex = 0;

			foreach( var auxCard in cards ) {
				HideCard(auxCard);

				cardIndex++;
            }
		}

        protected override void FlipCard( CardFacade _card, bool _facingUp ) {
			_card.FlipCard(_facingUp);
			_card.SetCanBeInteractable(_facingUp);
		}
        #endregion




        #region Private methods
        private void DisplayCard( CardFacade _card ) {
			displayedCards.Add(_card);

			_card.transform.GetComponent<RectTransform>().position = cardDisplayPosition.position;
			FlipCard( _card, true );
		}

		private void HideCard( CardFacade _card ) {
			displayedCards.Remove(_card);

			_card.transform.GetComponent<RectTransform>().position = transform.position;
			FlipCard( _card, false );
		}
        #endregion
    }
}
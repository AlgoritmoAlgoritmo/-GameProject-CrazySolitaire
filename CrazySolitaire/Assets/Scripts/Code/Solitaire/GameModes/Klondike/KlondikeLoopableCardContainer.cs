/*
* Author:	Iris Bermudez
* Date:		13/06/2024
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;

namespace Solitaire.GameModes.Klondike {
	public class KlondikeLoopableCardContainer : BasicCardContainer {
		#region Variables
		[SerializeField]
		private RectTransform orginalCardPosition;
		[SerializeField]
		private RectTransform cardDisplayPosition;

		private int currentDisplayedCard = -1;
		#endregion


		#region Public methods
		public void ShowNextCard() {
			Debug.Log("ShowNextCard");

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
		#endregion


		#region Protected methods
		protected override void SetUpStarterCards() {
			short cardIndex = 0;

			foreach( var auxCard in cards ) {
				HideCard(auxCard);

				cardIndex++;
            }
		}
        #endregion


        #region Private methods
		private void DisplayCard( CardFacade _card ) {
			_card.transform.GetComponent<RectTransform>().position
						= cardDisplayPosition.position;
			_card.FlipCard( true );
			_card.SetCanBeInteractable( true );
		}

		private void HideCard( CardFacade _card ) {
			_card.transform.GetComponent<RectTransform>().position
						= orginalCardPosition.position;
			_card.FlipCard( false );
			_card.SetCanBeInteractable( false );
		}
        #endregion
    }
}
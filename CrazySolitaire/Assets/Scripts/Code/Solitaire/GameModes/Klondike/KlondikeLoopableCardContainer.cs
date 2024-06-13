/*
* Author:	Iris Bermudez
* Date:		13/06/2024
*/



using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Solitaire.Gameplay.CardContainers;



namespace Solitaire.GameModes.Klondike {
	public class KlondikeLoopableCardContainer : BasicCardContainer {
		#region Variables
		[SerializeField]
		private Transform orginalCardPosition;
		[SerializeField]
		private Transform cardDisplayPosition;
		#endregion


		#region Public methods

		#endregion


		#region Protected methods
		protected override void SetUpStarterCards() {
			short cardIndex = 0;

			foreach( var auxCard in cards ) {
				auxCard.FlipCard(false);

				cardIndex++;
            }

			cards[cardIndex - 1].FlipCard(true);

		}
		#endregion
	}
}
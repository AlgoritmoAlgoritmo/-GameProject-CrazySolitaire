/*
* Author:	Iris Bermudez
* Date:		12/07/2024
*/



using System.Collections.Generic;
using Solitaire.GameModes.Klondike;
using Solitaire.Gameplay.CardContainers;



namespace Tests.Solitaire.GameModes.Klondike {
	public class KlondikeGameModeMock : KlondikeGameMode {
		#region Public methods
		public void SetCardContainers( List<AbstractCardContainer> _containerList ) {
			cardContainers = _containerList;
		}

		public int GetAmountOfDistributedCards() {
			int amountOfCards = 0;

			foreach( var auxContainer in cardContainers ) {
				amountOfCards += auxContainer.GetCardCount();
			}

			return amountOfCards;
		}
		#endregion
	}
}
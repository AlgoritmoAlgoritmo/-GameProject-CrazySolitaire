/*
* Author:	Iris Bermudez
* Date:		08/03/2024
*/



using System.Collections.Generic;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Common;
using Solitaire.Gameplay.Spider;



namespace Tests.Solitaire.Gameplay.Spider {
    public class SpiderGameModeMock : SpiderGameMode {
        #region Variables
        #endregion


        #region Public methods
        public int GetAmountOfDistributedCards() {
            int amountOfCards = 0;

            foreach( var auxContainer in cardContainers ) {
                amountOfCards += auxContainer.GetCards().Count;
            }

            return amountOfCards;
        }

        public void SetCardContainers( List<AbstractCardContainer> _containersList ) {
            cardContainers = _containersList;
        }

        public void SetSuits(List<BasicSuitData> _suits) {
            suits = _suits;
        }

        public void SetAmountPerSuit(short _amountPerSuit ) {
            amountOfEachSuit = _amountPerSuit;
        }
        #endregion


        #region Mocked SpiderGameMode private methods

        #endregion
    }
}
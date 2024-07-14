/*
* Author:	Iris Bermudez
* Date:		08/12/2023
*/



using System;



namespace Solitaire.Gameplay.Spider {
    public class SpiderGameMode : AbstractGameMode {
        #region Variables

        #endregion


        #region Public methods
        public override void Initialize() {
            cards = deckController.InitializeCards( suits, amountOfEachSuit );

            foreach( AbstractCardContainer auxCardContainer in cardContainers ) {
                cards = auxCardContainer.Initialize( cards );
            }
        }

        public override void SubscribeToOnGameClearedEvent(
                                        EventHandler eventHandler) {
            deckController.onCardsCleared += eventHandler;
        }
        #endregion
    }
}
/*
* Author:	Iris Bermudez
* Date:		08/12/2023
*/

using Solitaire.Cards;
using System;
using System.Collections.Generic;



namespace Solitaire.Gameplay {
    public class SpiderGameMode : AbstractGameMode {
        #region Variables

        #endregion


        #region MonoBehaviour methods

        #endregion


        #region Public methods
        public override void Initialize() {
            cards = new List<CardController>();
        }

        public override void SubscribeToOnGameClearedEvent(
                                    EventHandler eventHandler ) {
            deckController.onCardsCleared += eventHandler;
        }
        #endregion


        #region Private methods

        #endregion
    }
}
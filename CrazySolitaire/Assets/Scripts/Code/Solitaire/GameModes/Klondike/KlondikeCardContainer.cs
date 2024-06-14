/*
* Author:	Iris Bermudez
* Date:		12/06/2024
*/



using UnityEngine;
using System.Collections.Generic;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;



namespace Solitaire.GameModes.Klondike {
    public class KlondikeCardContainer : BasicCardContainer {
        #region Variables

        #endregion


        #region Public methods

        #endregion


        #region Protected methods
        protected override void SetUpStarterCards() {
            int counter = 0;

            foreach( var auxCard in cards ) {
                counter++;

                if( counter < cards.Count ) {
                    auxCard.FlipCard( false );
                } else {
                    auxCard.FlipCard( true );
                }
            }
        }
        #endregion
    }
}
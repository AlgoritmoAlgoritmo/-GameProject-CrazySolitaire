/*
* Author:	Iris Bermudez
* Date:		29/07/2024
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.GameModes.Spider;
using Solitaire.Gameplay.Cards;



namespace Tests.Solitaire.GameModes.Spider {
	public class SpiderCardContainerForCardDistributionMock : SpiderCardContainerForCardDistribution {
        #region Variables

        #endregion


        #region Public methods
        public override bool AddCards( List<CardFacade> _cards ) {
            foreach( var auxCard in _cards ) {
                cards.Add( auxCard );
            }

            return true;
        }
        #endregion


        #region Private methods

        #endregion
    }
}
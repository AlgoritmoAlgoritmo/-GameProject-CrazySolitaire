/*
* Author:	Iris Bermudez
* Date:		11/12/2023
*/

using Solitaire.Cards;
using System.Collections.Generic;
using UnityEngine;


namespace Solitaire.Gameplay.Spider {
    public class SpiderCardContainer : AbstractCardContainer {
        #region Variables

        #endregion


        #region MonoBehaviour methods
        #endregion


        #region Public methods
        public override List<CardController> Initialize(List<CardController> _cards) {
            foreach( CardController auxCard in _cards ) {
                Debug.Log(auxCard);
            }

            return new List<CardController>();
        }

        public override bool AddCard(CardController _card) {
            throw new System.NotImplementedException();
        }

        public override bool AddCards(List<CardController> _cards) {
            throw new System.NotImplementedException();
        }

        public override CardController GetTopCard() {
            throw new System.NotImplementedException();
        }        
        #endregion


        #region Private methods

        #endregion

    }
}
/*
* Author:	Iris Bermudez
* Date:		07/12/2023
*/


using System.Collections.Generic;
using UnityEngine;
using Solitaire.Cards;


namespace Solitaire.Gameplay {

    public abstract class AbstractCardContainer : MonoBehaviour {
        #region Variables

        protected List<CardController> cards = new List<CardController>();

        #endregion


        #region Public variables

        public abstract List<CardController> AddCards( List<CardController> _cards );

        public abstract bool AddCard( CardController _card );

        public abstract int GetCardAmount();

        #endregion
    }
}
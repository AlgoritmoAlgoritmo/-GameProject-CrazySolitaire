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
        protected List<CardController> cards;
        #endregion


        #region Public methods
        public abstract List<CardController> Initialize(List<CardController> _cards);
        public abstract bool AddCards( List<CardController> _cards );
        public abstract bool AddCard( CardController _card );
        public abstract CardController GetTopCard();
        #endregion
    }
}
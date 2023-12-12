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
        [SerializeField]
        protected Vector2 cardsOffset = Vector2.zero;
        [SerializeField]
        protected short initialCardsAmount = 0;

        protected List<CardController> cards = new List<CardController>();
        #endregion


        #region Public methods
        public abstract List<CardController> Initialize(List<CardController> _cards);
        public abstract bool AddCards( List<CardController> _cards );
        public abstract bool AddCard( CardController _card );
        public abstract CardController GetTopCard();
        #endregion
    }
}
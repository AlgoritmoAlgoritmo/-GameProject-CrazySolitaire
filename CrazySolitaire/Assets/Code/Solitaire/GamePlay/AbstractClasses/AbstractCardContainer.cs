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
        [SerializeField]
        protected bool canAddCards = true;

        protected List<CardController> cards = new List<CardController>();
        #endregion


        #region Public methods
        public abstract List<CardController> Initialize(List<CardController> _cards);
        public abstract bool AddCards( List<CardController> _cards );
        public abstract bool AddCard( CardController _card );
        public abstract bool CandAddCards();
        
        public CardController GetTopCard() {
            return cards[ cards.Count - 1 ];
        }
        #endregion


        #region Protected methods
        protected List<CardController> AddInitializationCards( List<CardController> _cards ) {
            List<CardController> auxCardList = _cards;
            Vector2 auxPosition = Vector2.zero;

            for (int i = 0; i < initialCardsAmount; i++) {
                cards.Add(auxCardList[0]);
                cards[i].transform.SetParent(transform);
                cards[i].transform.localPosition = auxPosition;
                auxCardList.RemoveAt(0);

                auxPosition += cardsOffset;
            }

            SetCardsFacingDirection();

            return auxCardList;
        }

        protected abstract void SetCardsFacingDirection();
        #endregion
    }
}
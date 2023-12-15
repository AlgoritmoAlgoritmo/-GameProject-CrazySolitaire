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

        protected List<CardFacade> cards = new List<CardFacade>();
        #endregion


        #region Public methods
        public abstract List<CardFacade> Initialize(List<CardFacade> _cards);
        public abstract bool AddCards( List<CardFacade> _cards );
        public abstract bool AddCard( CardFacade _card );
        public abstract bool CandAddCards();
        
        public CardFacade GetTopCard() {
            return cards[ cards.Count - 1 ];
        }
        #endregion


        #region Protected methods
        protected List<CardFacade> AddInitializationCards( List<CardFacade> _cards ) {
            List<CardFacade> auxCardList = _cards;
            Vector2 auxPosition = transform.position;

            for (int i = 0; i < initialCardsAmount; i++) {
                cards.Add(auxCardList[0]);
                cards[i].transform.position = auxPosition;
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
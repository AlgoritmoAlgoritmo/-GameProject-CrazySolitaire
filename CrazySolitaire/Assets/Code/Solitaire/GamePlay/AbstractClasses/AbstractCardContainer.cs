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
        public abstract void RemoveCards( List<CardFacade> _cards );
        public abstract void AddCard( CardFacade _card );
        public abstract void RemoveCard( CardFacade _card );
        public abstract bool CandAddCards();


        public Vector2 GetCardPosition( CardFacade _card ) {
            int index = cards.IndexOf( _card );

            return (Vector2) transform.position + ( cardsOffset * index );
        }

        public bool ContainsCard( CardFacade _card ) {
            foreach( var auxCard in cards ) {
                if( _card == auxCard  ) {
                    return true;
                }
            }

            return false;
        }
        
        public CardFacade GetTopCard() {
            if( cards.Count == 0 ) 
                return null;

            return cards[cards.Count - 1];
        }

        public List<CardFacade> GetCards() {
            return cards;
        }
        #endregion


        #region Protected methods
        protected List<CardFacade> AddInitializationCards( List<CardFacade> _cards ) {
            List<CardFacade> auxCardList = _cards;

            for (int i = 0; i < initialCardsAmount; i++) {
                cards.Add(auxCardList[0]);
                cards[i].transform.position = GetCardPosition( cards[i] );
                auxCardList.RemoveAt(0);
            }

            SetUpStarterCards();

            return auxCardList;
        }

        protected abstract void SetUpStarterCards();
        #endregion
    }
}
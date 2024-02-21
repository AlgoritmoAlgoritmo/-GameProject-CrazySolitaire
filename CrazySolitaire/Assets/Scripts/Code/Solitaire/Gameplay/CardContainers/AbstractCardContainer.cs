/*
* Author:	Iris Bermudez
* Date:		07/12/2023
*/



using System.Collections.Generic;
using UnityEngine;
using Solitaire.Gameplay.Cards;



namespace Solitaire.Gameplay.CardContainers {

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
        public abstract List<CardFacade> Initialize( List<CardFacade> _cards );
        public abstract bool AddCards( List<CardFacade> _cards );
        public abstract void RemoveCards( List<CardFacade> _cards );
        public abstract void AddCard( CardFacade _card );
        public abstract void RemoveCard( CardFacade _card );
        public abstract bool CandAddCards();
        
        public bool ContainsCard( CardFacade _card ) {
            return GetCardIndex( _card.GetID() ) != -1;
        }
                
        public CardFacade GetTopCard() {
            if( cards.Count == 0 ) 
                return null;

            return cards[cards.Count - 1];
        }

        public List<CardFacade> GetCards() {
            return cards;
        }
        
        public void Refresh() {
            if( cards.Count > 0 ) {
                for( int i = 0; i < cards.Count; i++ ) {
                    cards[i].transform.position = GetCardPosition( i );
                }
            }
        }
        #endregion


        #region Protected methods
        protected List<CardFacade> AddInitializationCards( List<CardFacade> _cards ) {
            List<CardFacade> auxCardList = _cards;

            for( int i = 0; i < initialCardsAmount; i++ ) {
                cards.Add(auxCardList[0]);
                cards[i].transform.position = GetCardPosition( i );
                auxCardList.RemoveAt(0);
            }

            SetUpStarterCards();

            return auxCardList;
        }
        
        protected abstract void SetUpStarterCards();

        protected int GetCardIndex( string _cardID ) {
            for ( int index = 0 ; index < cards.Count; index++ ) {
                if ( _cardID.Equals( cards[index].GetID() ) ) {
                    return index;
                }
            }

            return -1;
        }

        protected Vector2 GetCardPosition( int _index ) {
            return (Vector2) transform.position + ( cardsOffset * _index );
        }
        #endregion
    }
}
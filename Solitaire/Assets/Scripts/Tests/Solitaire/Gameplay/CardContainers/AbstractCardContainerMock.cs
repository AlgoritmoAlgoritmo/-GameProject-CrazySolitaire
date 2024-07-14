/*
* Author:	Iris Bermudez
* Date:		29/02/2024
*/



using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using System.Collections.Generic;
using UnityEngine;



namespace Tests.Solitaire.Gameplay.CardContainers {
    public class AbstractCardContainerMock : AbstractCardContainer {
        #region Public methods
        public override void AddCard(CardFacade _card) {
            cards.Add(_card);
        }

        public override bool AddCards(List<CardFacade> _cards) {
            foreach( CardFacade auxCard in _cards ) {
                cards.Add( auxCard );
            }

            return true;
        }

        public void SetOffset( Vector2 _offset ) {
            cardsOffset = _offset;
        }

        public Vector3 GetCardPosition_MockPublicAccess( int _index ) {
            return GetCardPosition( _index );
        }

        public void SetInitialCardsAmount( short _initialCardAmount ) {
            initialCardsAmount = _initialCardAmount;
        }

        public override List<CardFacade> Initialize(List<CardFacade> _cards) {
            return AddInitializationCards( _cards );
        }


        public override void RemoveCard(CardFacade _card) {
            throw new System.NotImplementedException();
        }

        public override void RemoveCards(List<CardFacade> _cards) {
            throw new System.NotImplementedException();
        }
        #endregion


        #region Protected Methods
        protected override void SetUpStarterCards() {
            // INTENTIONALLY LEFT BLANK TO AVOID EXCEPTIONS
        }
        #endregion
    }
}

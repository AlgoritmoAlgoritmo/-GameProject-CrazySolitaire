/*
* Author:	Iris Bermudez
* Date:		11/12/2023
*/



using Solitaire.Cards;
using System.Collections.Generic;



namespace Solitaire.Gameplay.Spider {
    public class SpiderCardContainer : AbstractCardContainer {
        #region Variables

        #endregion


        #region MonoBehaviour methods
        #endregion


        #region Public methods
        public override List<CardFacade> Initialize( List<CardFacade> _cards ) {
            if( _cards == null     ||     _cards.Count == 0 ) {
                throw new System.Exception( "Cards list is empty." );

            } else if( _cards.Count < initialCardsAmount ) {
                throw new System.Exception( "There aren't enough cards to initialize CardContainer." );
            }

            SetCardsFacingDirection();


            return AddInitializationCards( _cards );
        }


        public override bool AddCard( CardFacade _card ) {
            throw new System.NotImplementedException();
        }


        public override bool AddCards( List<CardFacade> _cards ) {
            throw new System.NotImplementedException();
        }


        public override bool CandAddCards() {
            return canAddCards;
        }
        #endregion


        #region Protected methods
        protected override void SetCardsFacingDirection() {
            for( int i = 0; i <= cards.Count - 1; i++ ) {
                if( i != cards.Count - 1)
                    cards[i].FlipCard( false );
                else
                    cards[i].FlipCard( true );
            }
        }
        #endregion
    }
}
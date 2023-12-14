/*
* Author:	Iris Bermudez
* Date:		14/12/2023
*/



using Solitaire.Cards;
using System.Collections.Generic;



namespace Solitaire.Gameplay.Spider {
    public class SpiderCardDistributor : AbstractCardContainer {
        #region Variables

        #endregion


        #region Public methods
        public override List<CardController> Initialize( List<CardController> _cards ) {
            return AddInitializationCards( _cards );
        }


        public override bool AddCards( List<CardController> _cards ) {
            throw new System.NotImplementedException();
        }


        public override bool AddCard( CardController _card ) {
            throw new System.NotImplementedException();
        }


        public override bool CandAddCards() {
            return canAddCards;
        }
        #endregion


        #region Protected methods
        protected override void SetCardsFacingDirection() {
            for (int i = 0; i <= cards.Count - 1; i++) {
                cards[i].FlipCard(false);
            }
        }
        #endregion
    }
}
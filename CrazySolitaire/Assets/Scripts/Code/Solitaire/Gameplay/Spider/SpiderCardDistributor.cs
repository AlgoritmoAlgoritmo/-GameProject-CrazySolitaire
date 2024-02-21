﻿/*
* Author:	Iris Bermudez
* Date:		14/12/2023
*/



using System.Collections.Generic;
using Solitaire.Gameplay.Cards;
using Solitaire.Gameplay.CardContainers;



namespace Solitaire.Gameplay.Spider {
    public class SpiderCardDistributor : AbstractCardContainer {
        #region Variables

        #endregion


        #region Public methods
        public override List<CardFacade> Initialize( List<CardFacade> _cards ) {
            return AddInitializationCards( _cards );
        }


        public override bool AddCards( List<CardFacade> _cards ) {
            throw new System.NotImplementedException();
        }


        public override void RemoveCards( List<CardFacade> _cards ) {
            throw new System.NotImplementedException();
        }


        public override void AddCard( CardFacade _card ) {
            throw new System.NotImplementedException();
        }


        public override void RemoveCard(CardFacade _card) {
            cards.Remove( _card );
        }
        #endregion


        #region Protected methods
        protected override void SetUpStarterCards() {
            for (int i = 0; i <= cards.Count - 1; i++) {
                cards[i].FlipCard( false );
                cards[i].SetCanBeDragged( false );
                cards[i].ActivateParentDetection( false );
            }
        }
        #endregion
    }
}
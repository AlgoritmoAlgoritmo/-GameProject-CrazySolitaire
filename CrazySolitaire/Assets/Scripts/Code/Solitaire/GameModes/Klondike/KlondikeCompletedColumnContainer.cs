/*
* Author:	Iris Bermudez
* Date:		12/06/2024
*/



using UnityEngine;
using System.Collections.Generic;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;



namespace Solitaire.GameModes.Klondike {
    public class KlondikeCompletedColumnContainer : AbstractCardContainer {
        #region Variables

        #endregion


        #region Public methods
        public override void AddCard(CardFacade _card) {
            throw new System.NotImplementedException();
        }

        public override bool AddCards(List<CardFacade> _cards) {
            throw new System.NotImplementedException();
        }

        public override List<CardFacade> Initialize(List<CardFacade> _cards) {
            throw new System.NotImplementedException();
        }

        public override void RemoveCard(CardFacade _card) {
            throw new System.NotImplementedException();
        }

        public override void RemoveCards(List<CardFacade> _cards) {
            throw new System.NotImplementedException();
        }
        #endregion


        #region Protected methods
        protected override void SetUpStarterCards() {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
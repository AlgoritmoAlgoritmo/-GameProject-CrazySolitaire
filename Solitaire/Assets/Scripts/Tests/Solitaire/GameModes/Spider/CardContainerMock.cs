/*
* Author:	Iris Bermudez
* Date:		12/03/2024
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;



namespace Tests.Solitaire.GameModes.Spider {
    public class CardContainerMock : AbstractCardContainer {    
        #region Public methods
        public override void AddCard(CardFacade _card) {
            throw new System.NotImplementedException();
        }

        public override bool AddCards(List<CardFacade> _cards) {
            cards = _cards;
            return true;
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
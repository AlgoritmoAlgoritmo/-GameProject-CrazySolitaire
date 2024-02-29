/*
* Author:	Iris Bermudez
* Date:		29/02/2024
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;



namespace Tests.Solitaire.Gameplay.CardContainers {
    public class AbstractCardContainerMock : AbstractCardContainer {
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

        protected override void SetUpStarterCards() {
            throw new System.NotImplementedException();
        }
    }
}
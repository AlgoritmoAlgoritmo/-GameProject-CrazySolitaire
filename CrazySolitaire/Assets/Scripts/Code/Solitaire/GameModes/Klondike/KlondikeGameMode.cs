/*
* Author:	Iris Bermudez
* Date:		11/06/2024
*/



using UnityEngine;
using System.Collections.Generic;
using Solitaire.Gameplay;
using Solitaire.Gameplay.Cards;



namespace Solitaire.GameModes.Klondike {
    public class KlondikeGameMode : AbstractGameMode {
        #region Variables

        #endregion


        #region Public methods
        public override void Initialize(List<CardFacade> _cards) {
            throw new System.NotImplementedException();
        }

        #endregion


        #region Protected methods
        protected override bool CanBeChildOf(CardFacade _card, CardFacade _potentialParent) {
            throw new System.NotImplementedException();
        }

        protected override bool CanCardBeDragged(CardFacade _card) {
            throw new System.NotImplementedException();
        }

        protected override void ManageCardEvent(CardFacade _placedCard, GameObject _detectedGameObject) {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
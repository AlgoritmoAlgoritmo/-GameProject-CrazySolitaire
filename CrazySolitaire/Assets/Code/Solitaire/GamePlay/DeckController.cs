/*
* Author:	Iris Bermudez
* Date:		07/12/2023
*/



using System.Collections.Generic;
using UnityEngine;
using Solitaire.Cards;
using Solitaire.Common;
using System;



namespace Solitaire.Gameplay {

    public class DeckController : MonoBehaviour {
        #region Variables
        [SerializeField]
        private CardController cardPrefab;
        [SerializeField]
        private CardSpritesScriptableObject cardSprites;

        public event EventHandler onCardsCleared;

        private List<CardController> inGamecards;
        private List<CardController> clearedCards;
        #endregion


        #region Public methods
        public void InstantiateCards() {

        }
        #endregion


        #region PrivateMethods

        #endregion
    }
}
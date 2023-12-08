/*
* Author:	Iris Bermudez
* Date:		08/07/2023
*/



using System;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Cards;



namespace Solitaire.Gameplay {

    public abstract class AbstractGameMode : MonoBehaviour {
        #region Variables
        [SerializeField]
        protected DeckController deckController;
        [SerializeField]
        protected List<AbstractCardContainer> cardContainers;
        [SerializeField]
        protected HashSet<string> suits;
        [SerializeField]
        protected short amountOfCardsPerSuit;


        protected List<CardController> cards;
        #endregion


        #region Public methods
        public abstract void Initialize();

        public abstract void SubscribeToOnGameClearedEvent( Action action );
        #endregion


        #region Private methods

        #endregion
    }
}
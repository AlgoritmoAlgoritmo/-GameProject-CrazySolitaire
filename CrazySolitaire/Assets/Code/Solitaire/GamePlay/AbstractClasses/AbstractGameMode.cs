/*
* Author:	Iris Bermudez
* Date:		08/12/2023
*/



using System;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Cards;
using Solitaire.Common;

namespace Solitaire.Gameplay {

    public abstract class AbstractGameMode : MonoBehaviour {
        #region Variables
        [SerializeField]
        protected DeckController deckController;
        [SerializeField]
        protected List<AbstractCardContainer> cardContainers;
        [SerializeField]
        protected HashSet<SuitData> suits;
        [SerializeField]
        protected short amountOfCardsPerSuit;


        protected List<CardController> cards;
        #endregion


        #region Public methods
        public abstract void Initialize();

        public abstract void SubscribeToOnGameClearedEvent( EventHandler _eventHandler );
        #endregion


        #region Protected methods
        #endregion
    }
}
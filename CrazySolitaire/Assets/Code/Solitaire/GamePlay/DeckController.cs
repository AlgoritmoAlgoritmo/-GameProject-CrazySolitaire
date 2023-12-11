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

        private List<CardController> inGamecards = new List<CardController>();
        private List<CardController> clearedCards;
        #endregion


        #region Public methods
        public void InstantiateCards( HashSet<SuitData> _suits,
                                short _amountOfCardsPerSuit ) {
            
            foreach( SuitData auxSuitKey in _suits ) {
                for( short i = 0; i < _amountOfCardsPerSuit; i++ ) {
                    CardData auxCardData = new CardData( i, auxSuitKey.suitName,
                                                                auxSuitKey.color );
                    CardController auxCardController = InstantiateCard();
                    auxCardController.SetCardData( auxCardData );
                    auxCardController.SetBackSprite( cardSprites.GetSuitCardsSprites( auxSuitKey ).ba );

                    inGamecards.Add( auxCardController );
                }
            }
        }
        #endregion


        #region PrivateMethods
        protected CardController InstantiateCard() {
            CardController cardInstance = Instantiate( cardPrefab );

            return cardInstance;
        }
        #endregion
    }
}
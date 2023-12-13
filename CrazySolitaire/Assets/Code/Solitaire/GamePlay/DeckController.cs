/*
* Author:	Iris Bermudez
* Date:		07/12/2023
*/



using System;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Cards;
using Solitaire.Common;



namespace Solitaire.Gameplay {

    public class DeckController : MonoBehaviour {
        #region Variables
        [SerializeField]
        private CardController cardPrefab;
        [SerializeField]
        private Transform cardParent;
        [SerializeField]
        private CardSpritesScriptableObject cardSprites;

        public event EventHandler onCardsCleared;

        private List<CardController> inGamecards = new List<CardController>();
        private List<CardController> clearedCards;
        #endregion


        #region Public methods
        public List<CardController> InitializeCards( List<BasicSuitData> _suits,
                                                    short _amountOfCardsPerSuit ) {
            Debug.Log( $"inGamecards: {inGamecards.Count}" );
            InstantiateCards( _suits, _amountOfCardsPerSuit );
            Debug.Log($"inGamecards: {inGamecards.Count}");
            ShuffleCards();
            Debug.Log($"inGamecards: {inGamecards.Count}");
            Rendersorting();
            Debug.Log($"inGamecards: {inGamecards.Count}");

            return inGamecards;
        }
        #endregion


        #region PrivateMethods
        private List<CardController> InstantiateCards( List<BasicSuitData> _suits,
                                                    short _amountOfCardsPerSuit ) {
            List<Sprite> suitSprites;

            // Iterating each suit
            foreach (BasicSuitData auxSuitKey in _suits) {
                Debug.Log( $"Instantiating suit {auxSuitKey}." );
                suitSprites = cardSprites.GetSuitCardsSprites( auxSuitKey );

                // For each amount amount suit
                for (short suitAmountCouter = 0; suitAmountCouter < _amountOfCardsPerSuit;
                                                                        suitAmountCouter++) {

                    // Instantiating for each card sprite
                    for (int spriteIndex = 0; spriteIndex < suitSprites.Count; spriteIndex++) {

                        CardData auxCardData = new CardData( (short) (spriteIndex + 1),
                                                            auxSuitKey.suitName,
                                                            auxSuitKey.color );

                        CardController auxCardController = InstantiateCard();
                        auxCardController.SetCardData(auxCardData);
                        auxCardController.SetBackSprite(cardSprites.backSprite);
                        auxCardController.SetFrontSprite(
                                        cardSprites.GetSuitCardsSprites(auxSuitKey)[spriteIndex] );

                        inGamecards.Add(auxCardController);
                    }
                }
            }

            return inGamecards;
        }
 

        private CardController InstantiateCard() {
            if( !cardParent )
                throw new Exception( "Cards' parent Transform has not been asigned." );

            CardController cardInstance = Instantiate( cardPrefab, cardParent );


            return cardInstance;
        }
        
        
        private void ShuffleCards() {
            List<CardController> auxShuffledCardList = new List<CardController>();
            int cardsAmount = inGamecards.Count;
            System.Random random = new System.Random();
            int randomCardIndex;

            for ( int i = 0; i < cardsAmount; i++ ) {
                randomCardIndex = random.Next( 0, inGamecards.Count );

                auxShuffledCardList.Add( inGamecards[randomCardIndex] );
                inGamecards.RemoveAt( randomCardIndex );
            }

            inGamecards = auxShuffledCardList;
        }
        
        
        private void Rendersorting() {
            foreach( CardController auxCardController in inGamecards ) {
                auxCardController.gameObject.transform.SetParent( transform );
                auxCardController.gameObject.transform.SetParent( cardParent );
            }
        }
        #endregion
    }
}
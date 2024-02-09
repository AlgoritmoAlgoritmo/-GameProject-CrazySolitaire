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
        private CardFacade cardPrefab;
        [SerializeField]
        private Transform cardParent;
        [SerializeField]
        private CardSpritesScriptableObject cardSprites;

        public event EventHandler onCardsCleared;

        private List<CardFacade> inGamecards = new List<CardFacade>();
        #endregion


        #region Public methods
        public List<CardFacade> InitializeCards( List<BasicSuitData> _suits,
                                                    short _amountOfCardsPerSuit ) {

            InstantiateCards( _suits, _amountOfCardsPerSuit );
            ShuffleCards();
            RenderSorting();

            return inGamecards;
        }


        public void SubscribeToDragStartEvent( Action<CardFacade> action ) {
            foreach( CardFacade auxCard in inGamecards ) {
                auxCard.SubscribeToOnStartDragging( action );
            }
        }


        public void SubscribeToCardPlacedEventWithCollision( Action<CardFacade,
                                                            GameObject> action ) {
            foreach( CardFacade auxCard in inGamecards ) {
                auxCard.SubscribeToOnCardPlacedWithCollisions( action );
            }
        }


        public void SubscribeToCardPlacedEventWithoutCollision(Action<CardFacade> action) {
            foreach (CardFacade auxCard in inGamecards) {
                auxCard.SubscribeToOnCardPlacedWithoutCollisions( action );
            }
        }
        
        
        
        public void RemoveCardsFromGame( List<CardFacade> _cards ) {
            Debug.Log( $"Current cards amount is {inGamecards.Count}." );

            foreach( CardFacade auxCard in _cards ) {
                inGamecards.Remove( auxCard );
            }

            if( inGamecards.Count == 0 ) {
                onCardsCleared.Invoke( this, EventArgs.Empty );
            }

            Debug.Log($"Amount of cards after column completition is "
                                                    + $"{inGamecards.Count}.");
        }
        #endregion


        #region PrivateMethods
        private List<CardFacade> InstantiateCards( List<BasicSuitData> _suits,
                                                    short _amountOfCardsPerSuit ) {
            List<Sprite> suitSprites;

            // Iterating each suit
            foreach( BasicSuitData auxSuitKey in _suits ) {
                suitSprites = cardSprites.GetSuitCardsSprites( auxSuitKey );
                int generatedID = 0;

                // For each amount amount suit
                for( short suitAmountCouter = 0; suitAmountCouter < _amountOfCardsPerSuit;
                                                                    suitAmountCouter++) {

                    // Instantiating for each card sprite
                    for (int spriteIndex = 0; spriteIndex < suitSprites.Count; spriteIndex++) {

                        CardData auxCardData = new CardData( (short) (spriteIndex + 1),

                                                            auxSuitKey.suitName,
                                                            auxSuitKey.color,
                                                            generatedID.ToString() );

                        CardFacade auxCardFacade = InstantiateCard();
                        auxCardFacade.SetCardData(auxCardData);
                        auxCardFacade.SetBackSprite(cardSprites.backSprite);
                        auxCardFacade.SetFrontSprite(
                                        cardSprites.GetSuitCardsSprites(auxSuitKey)[spriteIndex] );

                        inGamecards.Add(auxCardFacade);
                        generatedID++;
                    }
                }
            }

            return inGamecards;
        }
 

        private CardFacade InstantiateCard() {
            if( !cardParent )
                throw new Exception( "Cards' parent Transform has not been asigned." );

            CardFacade cardInstance = Instantiate( cardPrefab, cardParent );


            return cardInstance;
        }
        
        
        private void ShuffleCards() {
            List<CardFacade> auxShuffledCardList = new List<CardFacade>();
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
        
        
        private void RenderSorting() {
            foreach( CardFacade auxCardFacade in inGamecards ) {
                auxCardFacade.gameObject.transform.SetParent( transform );
                auxCardFacade.gameObject.transform.SetParent( cardParent );
            }
        }
        #endregion
    }
}
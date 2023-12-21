/*
* Author:	Iris Bermudez
* Date:		08/12/2023
*/



using System;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Cards;



namespace Solitaire.Gameplay.Spider {
    public class SpiderGameMode : AbstractGameMode {
        #region Variables

        #endregion


        #region Public methods
        public override void Initialize() {
            cards = deckController.InitializeCards(suits, amountOfEachSuit);

            List<CardFacade> auxCards = new List<CardFacade>();

            foreach (CardFacade auxCard in cards) {
                auxCards.Add(auxCard);
            }

            foreach (AbstractCardContainer auxCardContainer in cardContainers) {
                auxCards = auxCardContainer.Initialize(auxCards);
            }

            deckController.SubscribeToDragStartEvent( ValidateCardDragging );
            deckController.SubscribeToCardPlacedEventWithCollision( 
                                                ValidateCardPlacementWithCollison );
            deckController.SubscribeToCardPlacedEventWithoutCollision( 
                                                ValidateCardPlacementWithoutCollison );
        }


        public override void SubscribeToOnGameClearedEvent(
                                                    EventHandler eventHandler) {
            deckController.onCardsCleared += eventHandler;
        }


        public void ValidateCardDragging( CardFacade cardFacade ) {
            if( cardFacade.ChildCard == null ) {
                cardFacade.SetCanBeDragged( true );

            } else {
                cardFacade.SetCanBeDragged( false );

                /*
                if( cardFacade.GetCardNumber() == 1
                        ||  cardFacade.ChildCard.GetCardNumber() !=  cardFacade.GetCardNumber() - 1 ) {
                    cardFacade.SetCanBeDragged( false );
                }*/
            }
        }


        private void ValidateCardPlacementWithCollison( CardFacade placedCard,
                                                    CardFacade detectedGameObject ) {
            Debug.Log( "Collision detected sith collision." );

            // Write logic to move card from one container to another
            
        }


        private void ValidateCardPlacementWithoutCollison( CardFacade _card ) {
            // Write logic to make card return to previous position
            foreach( AbstractCardContainer auxCardContainer in cardContainers ) {
                if( auxCardContainer.ContainsCard( _card ) ) {
                    _card.transform.position = auxCardContainer.GetCardPosition( _card );
                }
            }
        }
        #endregion
    }
}
/*
* Author:	Iris Bermudez
* Date:		08/03/2024
*/



using System.Collections.Generic;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Common;
using Solitaire.GameModes.Spider;
using Solitaire.Gameplay.Cards;

namespace Tests.Solitaire.GameModes.Spider {
    public class SpiderGameModeMock : SpiderGameMode {
        #region Variables
        #endregion


        #region Public methods
        public override void Initialize( List<CardFacade> _cards ) {
            if( _cards.Contains( null ) ) {
                throw new System.NullReferenceException( "The list of cards passed for "
                                        + "initialization contains a null element." );

            } else if( _cards.Count < 1 ) {
                throw new System.IndexOutOfRangeException( "The list of cards passed for "
                                                    + "initialization is empty." );
            }

            List<CardFacade> auxCards = new List<CardFacade>();

            foreach( CardFacade auxCard in _cards ) {
                auxCards.Add( auxCard );
                auxCard.SubscribeToOnStartDragging( ValidateCardDragging );
                auxCard.SubscribeToCardEvent( ManageCardEvent );
            }

            foreach( AbstractCardContainer auxCardContainer in cardContainers ) {
                auxCards = auxCardContainer.Initialize( auxCards );
            }
        }


        public int GetAmountOfDistributedCards() {
            int amountOfCards = 0;

            foreach( var auxContainer in cardContainers ) {
                amountOfCards += auxContainer.GetCards().Count;
            }

            return amountOfCards;
        }

        public void SetCardContainers( List<AbstractCardContainer> _containersList ) {
            cardContainers = _containersList;
        }

        public void SetSuits(List<BasicSuitData> _suits) {
            suits = _suits;
        }

        public void SetAmountPerSuit(short _amountPerSuit ) {
            amountOfEachSuit = _amountPerSuit;
        }
        #endregion


        #region Mocked SpiderGameMode private methods

        #endregion
    }
}
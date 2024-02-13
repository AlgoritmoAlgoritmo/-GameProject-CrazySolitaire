/*
* Author:	Iris Bermudez
* Date:		11/12/2023
*/



using Solitaire.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace Solitaire.Gameplay.Spider {
    public class SpiderCardContainer : AbstractCardContainer {
        #region Public methods
        public override List<CardFacade> Initialize( List<CardFacade> _cards ) {
            if( _cards == null     ||     _cards.Count == 0 ) {
                throw new System.Exception( "Cards list is empty." );

            } else if( _cards.Count < initialCardsAmount ) {
                throw new System.Exception( "There aren't enough cards to initialize CardContainer." );
            }


            return AddInitializationCards( _cards );
        }


        public override void AddCard( CardFacade _card ) {
            cards.Add( _card );
            _card.transform.position = GetCardPosition( _card );

            CheckFacingUpCards();
        }


        public override void RemoveCard( CardFacade _card ) {
            Debug.Log("Removing card from SpiderCardContainer");
            cards.Remove( _card );
            UpdateCards();
        }


        public override bool AddCards( List<CardFacade> _cards ) {
            throw new System.NotImplementedException();
        }


        public override void RemoveCards( List<CardFacade> _cards ) {
            for( int i = _cards.Count - 1; i >= 0; i-- ) {
                cards.Remove( _cards[i] );
            }

            UpdateCards();
        }


        public override bool CandAddCards() {
            return canAddCards;
        }
        #endregion


        #region Protected methods
        protected override void SetUpStarterCards() {
            for( int i = 0; i <= cards.Count - 1; i++ ) {
                if( i != cards.Count - 1) { 
                    cards[i].FlipCard( false );
                    cards[i].SetCanBeDragged( false );
                    cards[i].ActivatePhysics( false );
                    cards[i].SetCollisionsActive( false );

                    cards[i].SetChildCard( cards[i + 1] );
                    cards[i + 1].SetParentCard( cards[i] );

                } else { 
                    cards[i].FlipCard( true );
                    cards[i].SetCanBeDragged( true );
                    cards[i].ActivatePhysics( true );
                    cards[i].SetCollisionsActive( true );
                }
            }
        }
        #endregion


        #region Private methods
        private void UpdateCards() {
            if( GetTopCard() ) {
                if( !GetTopCard().IsFacingUp() ) {
                    Debug.Log("Top card is not facing up");
                    CheckAndFlipUpperCard();
                } else {
                    Debug.Log("Top card is facing up");
                    CheckFacingUpCards();
                }
            }
        }


        private void CheckFacingUpCards() {
            Debug.Log("CheckFacingUpCards");
            if( cards.Count >= 2 ) {
                GetTopCard().SetCanBeDragged(true);

                Debug.Log("cards.Count >= 2");
                for( int i = cards.Count - 2; i >= 0; i-- ) {
                    Debug.Log( "Is facing up: " + cards[i].IsFacingUp() );
                    Debug.Log( $"Numbers: {cards[i].GetCardNumber()} " +
                                                    $"{cards[i+1].GetCardNumber()}" );
                    Debug.Log( $"Suits: {cards[i].GetSuit()} {cards[i+1].GetSuit()}" );


                    if ( cards[i].IsFacingUp()  &&  cards[i].GetSuit().Equals( cards[i+1] )
                                        &&  cards[i].GetCardNumber() - 1  == cards[i+1]
                                                                     .GetCardNumber() ) {
                        Debug.Log( "Setting card draggable." );
                        cards[i].SetCanBeDragged( true );
                    }
                }
            }

            CheckAndFlipUpperCard();
        }


        private void CheckAndFlipUpperCard() {
            if( GetTopCard() ) {
                GetTopCard().FlipCard( true );
                GetTopCard().SetCollisionsActive( true );
                GetTopCard().SetCanBeDragged( true );
                GetTopCard().ActivatePhysics( true );
            }
        }
        #endregion
    }
}
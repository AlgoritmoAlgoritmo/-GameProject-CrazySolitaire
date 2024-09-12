/*
* Author:	Iris Bermudez
* Date:		08/12/2023
*/



using System;
using System.Collections.Generic;
using UnityEngine;
using Solitaire.Gameplay.Cards;
using Solitaire.Gameplay.Common;
using Solitaire.Gameplay.CardContainers;



namespace Solitaire.Gameplay {
    public abstract class AbstractGameMode : MonoBehaviour {
        #region Variables
        [SerializeField]
        protected GameModeSuitsDataScriptableObject suitsData;

        public List<BasicSuitData> Suits {
            get => suitsData.Suits;
        }

        public short AmountOfEachSuit {
            get => suitsData.AmountOfEachSuit;
        }

    public CardsEvent OnCardsCleared = new CardsEvent();

        protected List<AbstractCardContainer> cardContainers = new List<AbstractCardContainer>();
        #endregion


        #region Public Methods
        public virtual void Initialize( List<CardFacade> _cards ) {
            if( _cards.Contains( null ) ) {
                throw new NullReferenceException( "The list of cards passed for "
                                        + "initialization contains a null element." );

            } else if( _cards.Count < 1 ) {
                throw new IndexOutOfRangeException( "The list of cards passed for "
                                                    + "initialization is empty." );
            }


            if( cardContainers is null || cardContainers.Count == 0 ) {
                cardContainers = new List<AbstractCardContainer>( FindObjectsOfType<AbstractCardContainer>() );
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
        #endregion


        #region Public abstract methods
        public abstract void ValidateCardDragging(CardFacade _card);
        #endregion


        #region Protected abstract methods
        protected abstract void ManageCardEvent( CardFacade _placedCard,
                                                        GameObject _detectedGameObject );

        protected abstract bool CanBeChildOf( CardFacade _card,
                                                            CardFacade _potentialParent );

        protected abstract bool CanCardBeDragged( CardFacade _card );
        #endregion


        #region Protected methods
        protected AbstractCardContainer GetCardContainer( CardFacade _card ) {
            foreach( AbstractCardContainer auxCardContainer in cardContainers) {
                if( auxCardContainer.ContainsCard(_card) ) {
                    return auxCardContainer;
                }
            }

            throw new Exception("Card doesn't belong to any Card Container.");
        }
        #endregion
    }
}
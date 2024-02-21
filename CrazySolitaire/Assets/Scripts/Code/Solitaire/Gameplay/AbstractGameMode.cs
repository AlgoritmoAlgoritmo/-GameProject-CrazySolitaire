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
        protected List<AbstractCardContainer> cardContainers;
        [SerializeField]
        protected List<BasicSuitData> suits;
        public List<BasicSuitData> Suits {
            get => suits;
        }

        [SerializeField]
        protected short amountOfEachSuit;
        public short AmountOfEachSuit {
            get => amountOfEachSuit;
        }


        [SerializeField]
        protected string cardsLayer = "CARD";
        [SerializeField]
        protected string cardContainersLayer = "CARD_CONTAINER";

        public CardsEvent OnCardsCleared = new CardsEvent();
        #endregion


        #region Public abstract methods
        public abstract void Initialize( List<CardFacade> _cards );
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
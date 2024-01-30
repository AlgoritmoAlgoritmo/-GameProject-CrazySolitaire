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
        protected List<BasicSuitData> suits;
        [SerializeField]
        protected short amountOfEachSuit;

        [SerializeField]
        protected string cardsLayer = "CARD";
        [SerializeField]
        protected string cardContainersLayer = "CARD_CONTAINER";


        protected List<CardFacade> cards;
        #endregion


        #region Public abstract methods
        public abstract void Initialize();

        public abstract void SubscribeToOnGameClearedEvent( EventHandler _eventHandler );
        #endregion


        #region Protected abstract methods
        protected abstract void ValidateCardPlacementWithCollison( CardFacade _placedCard,
                                                            GameObject _detectedGameObject );

        protected abstract void ValidateCardPlacementWithoutCollison( CardFacade _card );

        protected abstract bool CanBeChildOf( CardFacade _card, CardFacade _potentialParent );

        protected abstract bool CanBeDragged( CardFacade _card );
        #endregion


        #region Protected methods
        protected Vector3 GetCardOriginalPositionInContainer(CardFacade _card) {
            return GetCardContainer(_card).GetCardPosition(_card);
        }


        protected AbstractCardContainer GetCardContainer(CardFacade _card) {            
            foreach( AbstractCardContainer auxCardContainer in cardContainers) {
                if (auxCardContainer.ContainsCard(_card)) {
                    return auxCardContainer;
                }
            }

            throw new Exception("Card doesn't belong to any Card Container.");
        }
        #endregion
    }
}
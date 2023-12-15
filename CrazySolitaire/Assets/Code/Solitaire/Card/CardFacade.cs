/*
* Author:	Iris Bermudez
* Date:		06/12/2023
*/



using System;
using UnityEngine;



namespace Solitaire.Cards {
    public class CardFacade : MonoBehaviour {
        #region Variables
        [SerializeField]
        private CardData cardData;
        [SerializeField]
        private CardView cardView;


        private CardFacade childCard;
        public CardFacade ChildCard {
            get => childCard;
            set => childCard = value;
        }

        private CardFacade parentCard;
        public CardFacade ParentCard {
            get => parentCard;
            set => parentCard = value;
        }


        public event Action<CardFacade, GameObject> onEndDragging;
        #endregion



        #region Public methods
        public void SetCardData( CardData _cardData ) {
            cardData = _cardData;
        }


        public void SetFrontSprite( Sprite frontSprite ) {
            cardView?.SetFrontSprite( frontSprite );
        }


        public void SetBackSprite( Sprite backtSprite ) {
            cardView?.SetBackSprite( backtSprite );
        }


        public void FlipCard( bool _facingUp ) {
            cardView.FlipCard( _facingUp );
        }
        #endregion
    }
}
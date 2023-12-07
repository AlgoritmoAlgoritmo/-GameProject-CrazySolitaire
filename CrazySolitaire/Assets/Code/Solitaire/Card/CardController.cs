/*
* Author:	Iris Bermudez
* Date:		06/12/2023
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Solitaire.Cards {

    public class CardController : MonoBehaviour {
        #region Variables
        [SerializeField]
        private CardData cardData;
        [SerializeField]
        private CardView cardView;

        private CardController childCard;
        private CardController ChildCard {
            get => childCard;
            set => childCard = value;
        }

        private CardController parentCard;
        private CardController ParentCard {
            get => parentCard;
            set => parentCard = value;
        }
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
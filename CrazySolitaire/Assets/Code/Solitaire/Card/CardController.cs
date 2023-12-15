/*
* Author:	Iris Bermudez
* Date:		06/12/2023
*/



using UnityEngine;
using UnityEngine.EventSystems;

namespace Solitaire.Cards {
    public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler {
        #region Variables
        [SerializeField]
        private CardData cardData;
        [SerializeField]
        private CardView cardView;

        private CardController childCard;
        public CardController ChildCard {
            get => childCard;
            set => childCard = value;
        }

        private CardController parentCard;
        public CardController ParentCard {
            get => parentCard;
            set => parentCard = value;
        }

        private Vector3 offset = Vector2.zero;
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

        public void OnBeginDrag( PointerEventData eventData ) {
            Debug.Log( "Started dragging." );
            cardView.RenderOnTop( transform );
        }

        public void OnDrag( PointerEventData eventData ) {
            Debug.Log( "Dragging." );
            transform.position += (Vector3)eventData.delta;
        }
        #endregion
    }
}
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
        [SerializeField]
        private CardPhysics cardPhysics;


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


        public event Action<CardFacade> OnStartDrag;
        public event Action<CardFacade> OnCardPlacedWithoutCollisions;
        public event Action<CardFacade, CardFacade> OnCardPlacedWithCollisions;
        #endregion


        #region MonoBehaviour Methods
        private void Start() {
            cardPhysics.OnStartDragging += InvokeOnStartDragEvent;
            cardPhysics.OnCardPlacedWithCollisions += InvokeOnCardPlacedWithCollisions;
            cardPhysics.OnCardPlacedWithoutCollisions += InvokeOnCardPlacedWithoutCollisions;
            cardPhysics.OnDragging += MoveToPosition;
        }
        #endregion


        #region Public methods
        public short GetCardNumber() {
            return cardData.number;
        }


        public string GetSuit() {
            return cardData.suit;
        }


        public string GetColor() {
            return cardData.color;
        }


        public string GetID() {
            return cardData.id;
        }


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


        public void SubscribeToOnStartDragging( Action<CardFacade> action ) {
            OnStartDrag += action;
        }


        public void SubscribeToOnCardPlacedWithCollisions( Action<CardFacade, CardFacade> action ) {
            OnCardPlacedWithCollisions += action;
        }


        public void SubscribeToOnCardPlacedWithoutCollisions( Action<CardFacade> action ) {
            OnCardPlacedWithoutCollisions += action;
        }


        public void InvokeOnStartDragEvent() {
            RenderOnTop();

            OnStartDrag( this );
        }


        public void InvokeOnCardPlacedWithCollisions( GameObject _detectedGameObject ) {
            OnCardPlacedWithCollisions( this, _detectedGameObject.GetComponent<CardFacade>() );
        }


        public void InvokeOnCardPlacedWithoutCollisions() {
            OnCardPlacedWithoutCollisions( this );
        }


        public void SetCanBeDragged( bool _canBeDragged ) {
            cardPhysics.SetCanBeDragged( _canBeDragged );
            cardView.SetInteractable( _canBeDragged );
        }


        public void SetCollisionsActive( bool _active ) {
            cardPhysics.ActivateCollisions( _active );
        }


        public void MoveToPosition( Vector3 _newPositionOffset ) {
            if( ChildCard ) {
                ChildCard.MoveToPosition( _newPositionOffset );
            }

            transform.position += _newPositionOffset;
        }

        public void RenderOnTop() {
            cardView.RenderOnTop(transform);

            if (ChildCard) {
                ChildCard.RenderOnTop();
            }
        }
        #endregion
    }
}
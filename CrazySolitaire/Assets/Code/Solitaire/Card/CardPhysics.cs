/*
* Author:	Iris Bermudez
* Date:		15/12/2023
*/



using System;
using UnityEngine;
using UnityEngine.EventSystems;



namespace Solitaire.Cards {
    public class CardPhysics : MonoBehaviour, IBeginDragHandler,
                                        IDragHandler, IEndDragHandler {
        #region Variables
        public event Action OnStartDragging;
        public event Action OnCardPlacedWithoutCollisions;
        public event Action<GameObject> OnCardPlacedWithCollisions;

        private bool canBeDragged;
        private Collider2D attachedCollider2D;
        private Collider2D detectedCollider;
        #endregion



        #region Inherited/Interface methods
        private void Awake() {
            attachedCollider2D = GetComponent<Collider2D>();
        }


        public void OnBeginDrag( PointerEventData eventData ) {
            ActivateCollisions( true );
            OnStartDragging();
        }


        public void OnDrag( PointerEventData eventData ) {
            if( canBeDragged ) {
                transform.position += (Vector3)eventData.delta;
            }
        }


        public void OnEndDrag( PointerEventData eventData ) {
            if( canBeDragged ) {
                InvokeOnCardPlacedAction();
                ActivateCollisions( false );
                // Invoke( "InvokeOnCardPlacedAction", Time.deltaTime * 2 );
            }
        }


        private void OnTriggerEnter2D ( Collider2D collision) {
            detectedCollider = collision;
        }


        private void OnTriggerExit2D( Collider2D collision ) {
            
        }
        #endregion



        #region Public Methods
        public void SetCanBeDragged( bool canBeDragged ) {
            this.canBeDragged = canBeDragged;
        }


        public void ActivateCollisions( bool activate ) {
            attachedCollider2D.isTrigger = activate;
        }
        #endregion



        #region Private methods
        private void InvokeOnCardPlacedAction() {
            if( detectedCollider == null ) {
                OnCardPlacedWithoutCollisions();

            } else {
                OnCardPlacedWithCollisions( detectedCollider.gameObject );
            }
            
            ActivateCollisions( false );
            detectedCollider = null;
        }
        #endregion
    }
}
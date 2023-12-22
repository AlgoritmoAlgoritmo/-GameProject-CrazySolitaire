/*
* Author:	Iris Bermudez
* Date:		15/12/2023
*/



using System;
using System.Collections.Generic;
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
        private List<Collider2D> detectedColliders = new List<Collider2D>();
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
                // ActivateCollisions( true );
                // Invoke( "InvokeOnCardPlacedAction", Time.deltaTime * 2 );
            }
        }


        private void OnTriggerEnter2D ( Collider2D collision) {
            Debug.Log( "OnTriggerEnter2D" );

            detectedColliders.Add( collision );
        }


        private void OnTriggerExit2D( Collider2D collision ) {
            Debug.Log( "OnTriggerExit2D" );
            detectedColliders.Remove( collision );
        }
        #endregion



        #region Public Methods
        public void SetCanBeDragged( bool canBeDragged ) {
            this.canBeDragged = canBeDragged;
        }


        public void ActivateCollisions( bool activate ) {
            // attachedCollider2D.enabled = activate;
            attachedCollider2D.isTrigger = activate;
        }
        #endregion



        #region Private methods
        private void InvokeOnCardPlacedAction() {
            if( detectedColliders.Count == 0 ) {
                OnCardPlacedWithoutCollisions();

            } else {
                // Pass closest collider object through Event
                OnCardPlacedWithCollisions( GetClosestCollidingGameObject() );
            }
            
            ActivateCollisions( false );
            detectedColliders.Clear();
        }


        private GameObject GetClosestCollidingGameObject() {
            if( detectedColliders.Count == 1 ) {
                return detectedColliders[0].gameObject;

            } else {
                GameObject closestObject = detectedColliders[0].gameObject;
                float closestDistance = Vector3.Distance( transform.position,
                                            closestObject.transform.position );

                for( int i = 1; i < detectedColliders.Count; i++ ) {
                    if( Vector3.Distance( transform.position,  
                            detectedColliders[i].transform.position) <  closestDistance ) {

                        closestObject = detectedColliders[i].gameObject;
                        closestDistance = Vector3.Distance(transform.position,
                                                    closestObject.transform.position);
                    }
                }

                return closestObject;
            }
        }
        #endregion
    }
}
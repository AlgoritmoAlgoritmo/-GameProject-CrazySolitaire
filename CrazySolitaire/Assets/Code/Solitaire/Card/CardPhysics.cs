/*
* Author:	Iris Bermudez
* Date:		15/12/2023
*/



using UnityEngine;
using UnityEngine.EventSystems;



namespace Solitaire.Cards {
    public class CardPhysics : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        #region Variables
        private GameObject currentColidingObject;
        #endregion



        #region Inherited methods
        public void OnBeginDrag( PointerEventData eventData ) {
            throw new System.NotImplementedException();
            // cardView.RenderOnTop(transform);
        }


        public void OnDrag( PointerEventData eventData ) {
            transform.position += (Vector3)eventData.delta;
        }


        public void OnEndDrag( PointerEventData eventData ) {
            throw new System.NotImplementedException();
        }


        private void OnCollisionStay(Collision collision) {
            throw new System.NotImplementedException();
        }
        #endregion


        #region Private methods

        #endregion
    }
}
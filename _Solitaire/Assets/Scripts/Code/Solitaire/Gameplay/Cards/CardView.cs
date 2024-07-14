/*
* Author:	Iris Bermudez
* Date:		06/12/2023
*/



using UnityEngine;
using UnityEngine.UI;



namespace Solitaire.Gameplay.Cards {

    [System.Serializable]
    public class CardView {
        #region Variables
        [SerializeField]
        private Image frontImage;
        [SerializeField]
        private Image backImage;

        private bool isFacingUp;
        public bool IsFacingUp {
            get => isFacingUp;
        }
        #endregion


        #region Constructors
        public CardView() {}
        #endregion



        #region Public methods
        public void SetFrontSprite( Sprite _frontSprite ) {
            if( frontImage )
                frontImage.sprite = _frontSprite;

            else
                throw new System.Exception("frontImage reference is missing.");
        }


        public void SetBackSprite( Sprite _backSprite ) {
            if( backImage )
                backImage.sprite = _backSprite;

            else
                throw new System.Exception( "backImage reference is missing." );
        }


        public void FlipCard( bool _facingUp ) {
            if( frontImage  &&  backImage ) {
                frontImage.gameObject.SetActive( _facingUp );
                backImage.gameObject.SetActive( !_facingUp );
                isFacingUp = _facingUp;

            } else {
                throw new System.Exception("Either frontImage or backImage reference is missing.");
            }
        }


        public void RenderOnTop( Transform _transform ) {
            Transform auxParent = _transform.parent;

            if (_transform.parent && _transform.parent.parent) {
                _transform?.SetParent(_transform.parent.parent);
                _transform?.SetParent(auxParent);
            }
        }


        public void SetInteractable( bool _interactable) {
            frontImage.raycastTarget = _interactable;
            backImage.raycastTarget = _interactable;
        }
        #endregion
    }
}
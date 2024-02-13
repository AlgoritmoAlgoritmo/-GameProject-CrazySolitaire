/*
* Author:	Iris Bermudez
* Date:		06/12/2023
*/



using System.Collections.Generic;
using UnityEngine;



namespace Solitaire.Common {

    [CreateAssetMenu(fileName = "New CardSpritesScriptableObject",
                                menuName = "Solitaire/Card Sprites")]
    public class CardSpritesScriptableObject : ScriptableObject {
        #region Variables
        [SerializeField]
        private Sprite backSprite;
        public Sprite BackSprite {
            get => backSprite;
        }

        [SerializeField]
        private List<CompleteSuitData> suits = new List<CompleteSuitData>();        
        #endregion


        #region Public methods
        public void SetNewSprites( CardSpritesScriptableObject _newSprites ) {
            backSprite = _newSprites.backSprite;
            suits = _newSprites.suits;
        }


        public List<Sprite> GetSuitCardsSprites( BasicSuitData _suitData ) {
            foreach (CompleteSuitData auxSuit in suits) {
                if( auxSuit.suitName.Equals( _suitData.suitName ) )
                    return auxSuit.sprites;
            }

            throw new System.Exception( $"Suit key {_suitData} not found." );
        }
        #endregion
    }
}
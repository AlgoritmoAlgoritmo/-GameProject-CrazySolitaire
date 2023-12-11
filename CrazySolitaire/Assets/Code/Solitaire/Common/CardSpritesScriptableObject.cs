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
        public Sprite backSprite;

        [SerializeField]
        public List<SuitData> suits = new List<SuitData>();        
        #endregion


        #region Public methods
        public void SetNewSprites( CardSpritesScriptableObject _newSprites ) {
            backSprite = _newSprites.backSprite;
            suits = _newSprites.suits;
        }


        public List<Sprite> GetSuitCardsSprites( SuitData _suitData ) {
            foreach (SuitData auxSuit in suits) {
                if( auxSuit == _suitData )
                    return auxSuit.sprites;
            }

            throw new System.Exception( $"Suit key {_suitData} not found." );
        }
        #endregion
    }
}
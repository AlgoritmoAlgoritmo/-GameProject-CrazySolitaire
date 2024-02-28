/*
* Author:	Iris Bermudez
* Date:		30/01/2024
*/



using System.Collections.Generic;
using UnityEngine;
using EffectsSystem.Utils;



namespace EffectsSystem {
    [System.Serializable]
    public class EffectsManager : MonoBehaviour, IEffect {
        #region Variables
        [SerializeField]
        private List<BasicEffect> effects = new List<BasicEffect>();
        #endregion



        #region Public methods
        public void Play() {
            if( effects == null   ||   effects.Count < 1 )
                throw new System.NullReferenceException("No effect has been added.");

            foreach( IEffect auxEffect in effects ) {
                auxEffect.Play();
            }
        }


        public void AddEffect( BasicEffect _effect ) {
            effects.Add( _effect );
        }


        public int GetEffectsAmount() {
            return effects.Count;
        }
        #endregion
    }
}
/*
* Author:	Iris Bermudez
* Date:		30/01/2024
*/



using UnityEngine;
using EffectsSystem.Interfaces;
using System.Collections.Generic;



namespace EffectsSystem {
    [System.Serializable]
    public class EffectsManager : MonoBehaviour, IEffect {
        #region Variables
        [SerializeField]
        private List<IEffect> effects = new List<IEffect>();
        #endregion


        #region MonoBehaviour methods
        #endregion


        #region Public methods
        public void Play() {
            if( effects == null   &&   effects.Count < 1 )
                throw new System.NullReferenceException("No effect has been added.");

            foreach( IEffect auxEffect in effects ) {
                auxEffect.Play();
            }
        }


        public void AddEffect( IEffect _effect ) {
            effects.Add( _effect );
        }


        public int GetEffectsAmount() {
            return effects.Count;
        }
        #endregion


        #region Private methods

        #endregion
    }
}
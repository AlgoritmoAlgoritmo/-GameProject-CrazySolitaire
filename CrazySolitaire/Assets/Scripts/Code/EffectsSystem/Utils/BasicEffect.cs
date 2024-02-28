/*
* Author:	Iris Bermudez
* Date:		26/02/2024
*/



using System;

namespace EffectsSystem.Utils {
    [System.Serializable]
    public class BasicEffect : IEffect {
        #region Variables
        public readonly string MenuName;
        #endregion


        #region Inherited methods
        public virtual void Play() {
            throw new System.NotImplementedException();
        }

        public static explicit operator BasicEffect(Type v) {
            throw new NotImplementedException();
        }
        #endregion
    }
}
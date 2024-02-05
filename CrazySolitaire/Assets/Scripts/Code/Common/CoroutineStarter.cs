/*
* Author:	Iris Bermudez
* Date:		02/02/2024
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Common {
    public class CoroutineStarter : MonoBehaviour {
        #region Variables
        private static CoroutineStarter instance;
        public static CoroutineStarter Instance {
            get {
                if( !instance ) {
                    instance = Instantiate(new GameObject()).
                                        AddComponent(typeof(CoroutineStarter))
                                        as CoroutineStarter;
                }

                return instance;
            }
        }
        #endregion


        #region Public methods
        public void ExcecuteCoroutine( IEnumerator _routine ) {
            StartCoroutine( _routine );
        }
        #endregion
    }
}
/*
* Author:	Iris Bermudez
* Date:		02/02/2024
*/



using System;
using System.Collections;
using UnityEngine;
using EffectsSystem.Interfaces;
using Common;



namespace Tests.EffectsSystem.Effects.CameraEffects {
    public class CameraShakeEffects : IEffect {
        #region Variables
        private Camera camera;
        private float durationInSeconds = .3f;
        #endregion



        #region Public methods
        public void Play() {
            if ( !camera )
                throw new NullReferenceException( "No camera has been assigned." );

            CoroutineStarter.Instance.StartCoroutine( MoveCamera() );
        }


        public void SetCamera( Camera _camera ) {
            camera = _camera;
        }


        public void SetDuration( float _durationInSeconds ) {
            durationInSeconds = _durationInSeconds;
        }
        #endregion

        
        #region Task methods
        private IEnumerator MoveCamera() {
            float elapsedTime = 0f;

            while( elapsedTime <= durationInSeconds ) {
                elapsedTime += Time.deltaTime;
                camera.transform.position += new Vector3(
                                                UnityEngine.Random.Range( 1f, -1f ),
                                                UnityEngine.Random.Range( 1f, -1f ),
                                                UnityEngine.Random.Range( 1f, -1f )
                                            );
                yield return null;
            }
        }
        #endregion
    }
}
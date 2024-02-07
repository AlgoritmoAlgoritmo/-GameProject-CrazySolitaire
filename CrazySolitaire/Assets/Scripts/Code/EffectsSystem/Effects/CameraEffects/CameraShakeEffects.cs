/*
* Author:	Iris Bermudez
* Date:		02/02/2024
*/



using System.Collections;
using UnityEngine;
using EffectsSystem.Interfaces;
using Common;



namespace EffectsSystem.Effects.CameraEffects {
    [System.Serializable]
    public class CameraShakeEffects : IEffect {
        #region Variables
        [SerializeField]
        private Camera camera;
        [SerializeField]
        private float durationInSeconds = .3f;
        [SerializeField]
        private float amplitude = .5f;

        private bool isPlaying = false;
        public bool IsPlaying {
            get;
        }

        #endregion



        #region Public methods
        public void Play() {
            if ( !camera )
                throw new System.NullReferenceException( "No camera has been assigned." );

            if( !isPlaying ) { 
                CoroutineStarter.Instance.StartCoroutine( MoveCamera() );
            }
        }


        public void SetCamera( Camera _camera ) {
            camera = _camera;
        }


        public void SetDuration( float _durationInSeconds ) {
            durationInSeconds = _durationInSeconds;
        }


        public void Stop() {
            isPlaying = false;
        }
        #endregion

        
        #region Task methods
        private IEnumerator MoveCamera() {
            float elapsedTime = 0f;
            Vector3 originalLocalPosition = camera.transform.localPosition;
            isPlaying = true;

            while( elapsedTime <= durationInSeconds    &&    isPlaying ) {
                elapsedTime += Time.deltaTime;
                camera.transform.localPosition += new Vector3(
                                Random.Range( amplitude, -amplitude ),
                                Random.Range( amplitude, -amplitude ),
                                Random.Range( amplitude, -amplitude )
                            );

                yield return null;
            }

            camera.transform.localPosition = originalLocalPosition;
            isPlaying = false;
        }
        #endregion
    }
}
/*
* Author:	Iris Bermudez
* Date:		31/01/2024
*/


using System;
using UnityEngine;
using UnityEngine.Audio;
using EffectsSystem.Interfaces;





namespace EffectsSystem.Effects.AudioEffects {
    [System.Serializable]
    public class SoundEffect : IEffect {
        #region Variables
        [SerializeField]
        private AudioClip audioClip;
        [SerializeField]
        private AudioMixerGroup audioMixer;

        private AudioSource audioSource;
        #endregion

        #region Constructors
        public SoundEffect() {
            audioSource = GameObject.Instantiate(new GameObject()).
                                        AddComponent(typeof(AudioSource)) as AudioSource;
        }
        #endregion




        #region Public methods
        public void Play() {
            if( !audioClip )
                throw new NullReferenceException("AudioClip reference is null.");

            audioSource.clip = audioClip;
            audioSource.outputAudioMixerGroup = audioMixer;
            audioSource.Play();
        }


        public AudioClip GetAudioClip() {
            return audioClip;
        }

        public void SetAudioClip( AudioClip _audioClip ) {
            audioClip = _audioClip;                        
            audioSource.clip = audioClip;
        }

        public AudioSource GetAudioSource() {
            return audioSource;
        }

        public void SetAudioMixerGroup( AudioMixerGroup _audioMixer ) {
            audioMixer = _audioMixer;

            audioSource.outputAudioMixerGroup = audioMixer;
        }
        #endregion


        #region Private methods

        #endregion
    }
}
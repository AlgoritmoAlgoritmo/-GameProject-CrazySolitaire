/*
* Author:	Iris Bermudez
* Date:		01/02/2024
*/


using System;
using NUnit.Framework;
using UnityEngine;
using EffectsSystem.Interfaces;
using EffectsSystem.Effects.AudioEffects;
using Tests;
using System.Collections;
using UnityEngine.TestTools;

namespace Tests.EffectsSystem.Effects.AudioEffects {
    public class SoundEffectTest {
        
        private const string AUDIO_FILE_PATH = "Assets/Scripts/Tests/" +
                        "EffectsSystemTests/Effects/AudioEffects/Cityscapes.wav";
        private SoundEffect soundEffect;



        [SetUp]
        public void Setup() {
            soundEffect = new SoundEffect();
        }


        /*
         *      La convención para nombres de funciones de testing es:
         *      MethodName_WhenThisConditions_DoesWhat
         */
        [Test]
        public void SoundEffectClass_IsInstanceOfsIEffect() {
            Assert.IsInstanceOf(typeof(IEffect), soundEffect);       
        }



        [Test]
        public void Play_MethodCallWithNoAudioClip_ThrowsNullReferenceException() {
            soundEffect = new SoundEffect();

            Assert.Throws<NullReferenceException>(() => soundEffect.Play());
        }



        [Test]
        public void SetAudioClip_AudioClipSetup_IsTheSameAsAudioSourceAudioClip() {
            AudioClip audioClip = (AudioClip)UnityEditor.AssetDatabase.
                            LoadAssetAtPath(AUDIO_FILE_PATH, typeof(AudioClip));
            SoundEffect soundEffect = new SoundEffect();
            soundEffect.SetAudioClip(audioClip);


            soundEffect.Play();

            
            Assert.IsNotNull( soundEffect.GetAudioSource() );
            Assert.IsNotNull( soundEffect.GetAudioSource().clip );
            Assert.AreSame( audioClip, soundEffect.GetAudioSource().clip );
        }


        [UnityTest]
        public IEnumerator Play_MethodCallWithAudioClip_AudioClipPlays() {
            // Arrange
            AudioClip audioClip  = (AudioClip) UnityEditor.AssetDatabase.
                            LoadAssetAtPath( AUDIO_FILE_PATH, typeof(AudioClip) );
            SoundEffect soundEffect = new SoundEffect();
            soundEffect.SetAudioClip( audioClip );


            soundEffect.Play();
            yield return null;
            yield return null;

                        
            Assert.AreSame( audioClip, soundEffect.GetAudioSource().clip );
            Assert.True( soundEffect.GetAudioSource().isPlaying );
        }
    }
}

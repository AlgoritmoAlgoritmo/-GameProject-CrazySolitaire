/*
* Author:	Iris Bermudez
* Date:		01/02/2024
*/



using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;
using EffectsSystem.Interfaces;
using EffectsSystem.Effects.AudioEffects;
using System.Collections;




namespace Tests.EffectsSystem.Effects.AudioEffects {
    public class SoundEffectTest {
        
        private const string AUDIO_FILE_PATH = "Assets/Scripts/Tests/" +
                        "EffectsSystemTests/Effects/AudioEffects/Cityscapes.wav";
        private const string AUDIO_MIXER_PATH = "Assets/Scripts/Tests/" +
                        "EffectsSystemTests/Effects/AudioEffects/MixerForTest.mixer";
        private SoundEffect soundEffect;



        [SetUp]
        public void Setup() {
            soundEffect = new SoundEffect();
        }


        

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


        [UnityTest]
        public IEnumerator SetAudioMixerGroup_MixerGroupOverriding_IsTheSameAudioSourceMixerGroup() {
            AudioMixer audioMixer = UnityEditor.AssetDatabase.LoadAssetAtPath(
                                AUDIO_MIXER_PATH, typeof(AudioMixer) ) as AudioMixer;


            soundEffect.SetAudioMixerGroup( audioMixer.outputAudioMixerGroup );
            yield return null;
            yield return null;


            Assert.IsNotNull( audioMixer );
            Assert.AreSame( audioMixer.outputAudioMixerGroup,
                                        soundEffect.GetAudioSource().outputAudioMixerGroup );
        }
    }
}

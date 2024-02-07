/*
* Author:	Iris Bermudez
* Date:		02/02/2024
*/



using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using EffectsSystem.Interfaces;
using EffectsSystem.Effects.CameraEffects;



namespace Tests.EffectsSystem.Effects.CameraEffects {
    public class CameraShakeEffectTest {
        CameraShakeEffects cameraShakeEffects;



        [SetUp]
        public void Setup () {
            cameraShakeEffects = new CameraShakeEffects();
        }



        [Test]
        public void CameraShakeEffectInstantiation_ImplementsIEffect() {
            Assert.IsInstanceOf( typeof(IEffect), cameraShakeEffects );
        }


        [Test]
        public void Play_NoCameraAssigned_NullReferenceException () {
            Assert.Throws<NullReferenceException> ( () => cameraShakeEffects.Play() );
        }


        [UnityTest]
        public IEnumerator Play_CameraAssigned_PositionChanges() {
            Vector3 originalPosition = Camera.main.transform.position;
            cameraShakeEffects.SetCamera( Camera.main );


            cameraShakeEffects.Play();
            yield return null;
            yield return null;


            Assert.AreNotEqual(originalPosition, Camera.main.transform.position);
        }
    }
}

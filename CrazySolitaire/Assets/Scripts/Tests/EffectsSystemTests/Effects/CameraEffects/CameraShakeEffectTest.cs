/*
* Author:	Iris Bermudez
* Date:		02/02/2024
*/



using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using EffectsSystem.Interfaces;
using Tests.EffectsSystem.Effects.CameraEffects;
using System;



namespace Tests.EffectsSystem {
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
        public IEnumerator Play_CameraAssigned_IsMovedFromOriginalPosition() {
            Vector3 originalPosition = Camera.main.transform.position;
            cameraShakeEffects.SetCamera( Camera.main );


            cameraShakeEffects.Play();
            yield return null;
            yield return null;


            Assert.AreNotEqual(originalPosition, Camera.main.transform.position);
        }



        
        [UnityTest]
        public IEnumerator Play_FixedTime_MovingToDiferentPositionsOverTime() {
            Vector3 previousPosition = Camera.main.transform.position;
            float effectDuration = .4f;
            cameraShakeEffects.SetCamera( Camera.main );
            cameraShakeEffects.SetDuration( effectDuration );

            float elapsedTimeSincePlayWasCalled = 0f;
            cameraShakeEffects.Play();


            // Wait for movement to start
            while( Camera.main.transform.position == previousPosition ) {
                yield return null;
            }


            // Register how much time does the camera keep moving
            do  {
                elapsedTimeSincePlayWasCalled += Time.deltaTime;
                previousPosition = Camera.main.transform.position;

                yield return null;
            } while ( Camera.main.transform.position != previousPosition );


            // compare elapse time with duration
            Assert.AreEqual( effectDuration, elapsedTimeSincePlayWasCalled );
        }
        


        /*
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CameraShakeTestWithEnumeratorPasses() {
            
            yield return null;
        }
        */
    }
}

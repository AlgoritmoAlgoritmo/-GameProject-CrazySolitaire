/*
* Author:	Iris Bermudez
* Date:		30/01/2024
*/


using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using EffectsSystem;
using EffectsSystem.Interfaces;



namespace Tests.EffectsSystem {
    public class EffectsManagerTest {
        private GameObject effectManagerContainer;
        private EffectsManager effectsManager;


        [SetUp]
        public void SetUp() {
            effectManagerContainer = GameObject.Instantiate( new GameObject() );
            effectsManager = effectManagerContainer.AddComponent(
                                    typeof(EffectsManager)) as EffectsManager;
        } 


        
        /*
         *      La convención para nombres de funciones de testing es:
         *      MethodName_WhenThisConditions_DoesWhat
         */
        [Test]
        public void EffectsManager_IsInstanceOfIEffect_IsNotNull() {
            Assert.IsTrue( effectsManager is IEffect );
            Assert.IsNotNull( effectsManager );
        }


        [Test]
        public void Play_CallingMethod_DoesNotThrowException () {
            Action playAction = () => { effectsManager.Play(); };

            // Assert.Catch<Exception>( playAction );
        }
    }
}

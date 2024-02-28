/*
* Author:	Iris Bermudez
* Date:		30/01/2024
*/



using System;
using NUnit.Framework;
using UnityEngine;
using EffectsSystem;
using EffectsSystem.Utils;



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


        [Test]
        public void EffectsManager_IsInstanceOfIEffect_IsNotNull() {
            Assert.IsTrue( effectsManager is IEffect );
            Assert.IsNotNull( effectsManager );
        }


        [Test]
        public void Play_NoEffectsAdded_ThrowsNullReferenceException() {
            Assert.Throws<NullReferenceException>(() => effectsManager.Play());
        }


        [Test]
        public void Play_AddEffects_EffectsAreSuccessfullyAdded() {
            BasicEffect effect = new EffectImplementationForTesting();

            effectsManager.AddEffect( effect );

            Assert.AreEqual( effectsManager.GetEffectsAmount(), 1 );
            effectsManager.Play();
        }
    }
}

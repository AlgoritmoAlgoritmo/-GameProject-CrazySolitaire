/*
* Author:	Iris Bermudez
* Date:		07/02/2024
*/



using System.Collections;
using EffectsSystem.Utils;
using NUnit.Framework;
using UnityEngine.TestTools;



namespace Tests.EffectsSystem.Utils {

    public class BasicEffectTest {
        private BasicEffect basicEffect;

        [SetUp]
        public void Setup() {
            basicEffect = new BasicEffect();
        }


        [Test]
        public void BasicEffect_IsIEffect() {
            Assert.IsInstanceOf( typeof(IEffect), basicEffect );
        }

        [Test]
        public void WhenPlay_ThrowNotImplementedException() {
            Assert.Throws<System.NotImplementedException>( () => basicEffect.Play() );
        }
    }
}
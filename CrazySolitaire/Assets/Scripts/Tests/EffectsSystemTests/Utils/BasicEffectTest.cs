/*
* Author:	Iris Bermudez
* Date:		07/02/2024
*/



using EffectsSystem.Utils;
using NUnit.Framework;



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
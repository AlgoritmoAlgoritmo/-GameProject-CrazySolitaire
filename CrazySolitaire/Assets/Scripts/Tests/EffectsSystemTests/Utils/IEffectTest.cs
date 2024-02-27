/*
* Author:	Iris Bermudez
* Date:		30/01/2024
*/


using NUnit.Framework;
using EffectsSystem.Utils;



namespace Tests.EffectsSystem.Utils {
    public partial class IEffectTest {
        private ImplementationOfIEffectForTesting auxEffectImplementation;


        [SetUp]
        public void Setup() {
            auxEffectImplementation = new ImplementationOfIEffectForTesting();
        }



        [Test]
        public void Play_Instance_IsIEffect() {
            Assert.IsTrue(auxEffectImplementation is IEffect);
        }

        
        [Test]
        public void Play_CallingMethod_SwitchesBooleanFromFalseToTrue() {
            Assert.IsFalse( auxEffectImplementation.hasPlayMethodBeenExcecuted );

            auxEffectImplementation.Play();

            Assert.IsTrue( auxEffectImplementation.hasPlayMethodBeenExcecuted );
        }
    }
}

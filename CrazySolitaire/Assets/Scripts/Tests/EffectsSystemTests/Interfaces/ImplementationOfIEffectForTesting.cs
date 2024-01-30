/*
* Author:	Iris Bermudez
* Date:		30/01/2024
*/


using EffectsSystem.Interfaces;



namespace Tests.EffectsSystem.Interfaces {


    public partial class IEffectTest {
        private class ImplementationOfIEffectForTesting : IEffect {
            public bool hasPlayMethodBeenExcecuted;

            public void Play() {
                hasPlayMethodBeenExcecuted = true;
            }
        }

    }
}

/*
* Author:	Iris Bermudez
* Date:		30/01/2024
*/



using EffectsSystem.Utils;



namespace Tests.EffectsSystem.Utils {
    public partial class IEffectTest {
        private class ImplementationOfIEffectForTesting : IEffect {
            public bool hasPlayMethodBeenExcecuted;

            public void Play() {
                hasPlayMethodBeenExcecuted = true;
            }
        }
    }
}

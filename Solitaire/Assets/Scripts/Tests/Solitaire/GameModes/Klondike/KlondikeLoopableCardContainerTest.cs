/*
* Author:	Iris Bermudez
* Date:		24/07/2024
*/



using System;
using NUnit.Framework;
using UnityEngine;
using Solitaire.GameModes.Klondike;
using UnityEditor;
using Solitaire.Gameplay.CardContainers;



namespace Tests.Solitaire.GameModes.Klondike {
    public class KlondikeLoopableCardContainerTest {
        #region Variables
        private GameObject klondikeLoopableCardGameObject;
        private KlondikeLoopableCardContainer klondikeLoopableCardContainer;
        #endregion


        #region Setup
        [SetUp]
        public void SetUp() {
            klondikeLoopableCardGameObject = GameObject.Instantiate( AssetDatabase
                                                        .LoadAssetAtPath<GameObject>(
                                                            Test.TestConstants
                                                                .KLONDIKELOOPABLECONTAINER_PREFAB_PATH ) );

            if( !klondikeLoopableCardGameObject ) {
                throw new NullReferenceException( "GameObject at "
                        + $"{Test.TestConstants.KLONDIKELOOPABLECONTAINER_PREFAB_PATH} "
                        + "could not be loaded." );
            }

            klondikeLoopableCardContainer = klondikeLoopableCardGameObject
                                                    .GetComponent<KlondikeLoopableCardContainer>();

            if( !klondikeLoopableCardContainer ) {
                throw new NullReferenceException( "GameObject at "
                        + $"{Test.TestConstants.KLONDIKELOOPABLECONTAINER_PREFAB_PATH} "
                        + "does not contain a SpiderCardContainer component." );
            }
        }
        #endregion


        #region Tests
        [Test]
        public void KlondikeLoopableCardContainer_IsAbstractCardContainer() {
            Assert.IsInstanceOf( typeof( AbstractCardContainer ), klondikeLoopableCardContainer,
                        "KlondikeLoopableCardContainer does not inherit from AbstractCardContainer." );
        }

        #endregion
    }
}

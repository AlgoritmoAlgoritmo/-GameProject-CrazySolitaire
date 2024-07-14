/*
* Author:	Iris Bermudez
* Date:		26/06/2024
*/



using System;
using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Test;
using Solitaire.GameModes.Klondike;
using Solitaire.Gameplay;

namespace Tests.Solitaire.GameModes.Klondike {
    public class KlondikeGameModeTest {
        #region Variables
        private GameObject klondikeGameModeMockGameObject;
        private KlondikeGameModeMock klondikeGameModeMock;
        #endregion


        #region Tests setup
        [SetUp]
        public void SetUp() {
            klondikeGameModeMockGameObject = GameObject.Instantiate( AssetDatabase
                    .LoadAssetAtPath<GameObject>( TestConstants.KLONDIKEGAMEMODEMOCK_PREFAB_PATH ) );

            if( !klondikeGameModeMockGameObject )
                throw new NullReferenceException( "GameObject at "
                        + $"{TestConstants.KLONDIKEGAMEMODEMOCK_PREFAB_PATH} could not be loaded." );

            klondikeGameModeMock = klondikeGameModeMockGameObject.GetComponent<KlondikeGameModeMock>();

            if( !klondikeGameModeMock )
                throw new NullReferenceException( "GameObject at "
                        + $"{TestConstants.SPIDERGAMEMODEMOCK_PREFAB_PATH} does not contain "
                        + "a SpiderGameMode component." );
        }
        #endregion


        #region Tests
        [Test]
        public void KlondikeGameModeMock_Is_KlondikeGameMode() {
            Assert.IsInstanceOf(typeof( KlondikeGameMode ), klondikeGameModeMock );
        }

        [Test]
        public void KlondikeGameModeMock_Is_AbstractGameMode() {
            Assert.IsInstanceOf( typeof( AbstractGameMode ), klondikeGameModeMock );
        }

        [Test]
        public void WhenInitializing
        #endregion
    }
}
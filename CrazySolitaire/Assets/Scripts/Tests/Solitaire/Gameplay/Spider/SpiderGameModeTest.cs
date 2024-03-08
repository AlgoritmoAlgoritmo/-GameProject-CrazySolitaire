/*
* Author:	Iris Bermudez
* Date:		08/03/2024
*/



using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Solitaire.Gameplay.Spider;
using Solitaire.Gameplay;



namespace Tests.Solitaire.Gameplay.Spider {
    public class SpiderGameModeTest {
        #region Variables
        private GameObject spiderGameModeGameObject;
        private SpiderGameMode spiderGameMode;

        private const string SPIDERGAMEMODE_PREFAB_PATH = "Assets/Prefabs/Gameplay/GameMode.prefab";
        #endregion


        #region Tests set up
        [SetUp]
        public void Setup() {
            spiderGameModeGameObject = GameObject.Instantiate( AssetDatabase.LoadAssetAtPath<GameObject>(
                                                                            SPIDERGAMEMODE_PREFAB_PATH));
            if (!spiderGameModeGameObject) {
                throw new NullReferenceException($"GameObject at {SPIDERGAMEMODE_PREFAB_PATH} "
                                                    + "could not be loaded.");
            }

            spiderGameMode = spiderGameModeGameObject.GetComponent<SpiderGameMode>();
            if (!spiderGameMode) {
                throw new NullReferenceException($"GameObject at {SPIDERGAMEMODE_PREFAB_PATH} "
                                                    + "does not contain a SpiderGameMode component.");
            }
        }
        #endregion


        #region Tests
        [Test]
        public void SpiderGameMode_IsAbstractGameMode() {
            Assert.IsInstanceOf(typeof(AbstractGameMode), spiderGameMode,
                                "SpiderGameMode must inherit from AbstractGameMode.");
        }
        #endregion
    }
}

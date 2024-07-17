/*
* Author:	Iris Bermudez
* Date:		17/07/2024
*/



using System;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using Solitaire.GameModes.Klondike;
using Solitaire.Gameplay.CardContainers;
using Test;



namespace Tests.Solitaire.GameModes.Klondike {
    public class KlondikeCompletedColumnContainerTest {
        #region Variables
        private GameObject klondikeCompletedColumnContainerGameObject;
        private KlondikeCompletedColumnContainer klondikeCompletedColumnContainer;
        #endregion


        #region Setup
        [SetUp]
        public void SetUp() {
            klondikeCompletedColumnContainerGameObject = GameObject.Instantiate( 
                            AssetDatabase.LoadAssetAtPath<GameObject>(
                                TestConstants.KLONDIKECOMPLETEDCOLUMNCONTAINER_PREFAB_PATH ) );

            if( !klondikeCompletedColumnContainerGameObject ) {
                throw new NullReferenceException( "GameObject at KLONDIKECOMPLETEDCOLUMNCONTAINER_PREFAB_PATH "
                        + "could not be loaded." );
            }


            klondikeCompletedColumnContainer = klondikeCompletedColumnContainerGameObject
                                                .GetComponent<KlondikeCompletedColumnContainer>();

            if( !klondikeCompletedColumnContainer ) {
                throw new NullReferenceException( "Prefab at "
                                + $"{TestConstants.KLONDIKECOMPLETEDCOLUMNCONTAINER_PREFAB_PATH} "
                                + "does not contain a KlondikeCompletedColumnContainer component." );
            }
        }
        #endregion


        #region Tests
        [Test]
        public void KlondikeCompletedColumnContainer_Is_AbstractCardContainer() {
            Assert.IsInstanceOf( typeof(AbstractCardContainer), 
                                klondikeCompletedColumnContainer,
                                "KlondikeCompletedColumnContainer does not inherit from"
                                        + " AbstractCardContainer." );
        }
        
        
        //[Test]

        #endregion
    }
}

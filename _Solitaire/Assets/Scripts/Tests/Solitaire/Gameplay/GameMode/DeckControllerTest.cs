/*
* Author:	Iris Bermudez
* Date:		06/03/2024
*/



using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Solitaire.Gameplay.Cards;
using Solitaire.Gameplay.Common;
using Solitaire.Gameplay.GameMode;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;



namespace Tests.Solitaire.Gameplay.GameMode {
    public class DeckControllerTest {
        #region Variables
        private const string DECKCONTROLLER_PREFAB_PATH = "Assets/Prefabs/Gameplay"
                                                            + "/DeckController.prefab";
        private GameObject deckControllerGameObject;
        private DeckController deckController;
        #endregion

        #region Tests setup
        [SetUp]
        public void Setup() {
            deckControllerGameObject = GameObject.Instantiate( 
                            AssetDatabase.LoadAssetAtPath<GameObject>( DECKCONTROLLER_PREFAB_PATH ) );

            if( !deckControllerGameObject ) { 
                throw new NullReferenceException( $"The file {DECKCONTROLLER_PREFAB_PATH} "
                                                    + "couldn't be found.");
            }

            deckController = deckControllerGameObject.GetComponent<DeckController>();

            if( !deckController ) {
                throw new NullReferenceException("The object istantiated doesn't contain "
                                                    + "a DeckController component.");
            }
        }
        #endregion


        #region Tests
        [Test]
        public void WhenInitializingCards_ThenReturnPropperCardInstances() {
            // Create list of basic suit data and and set the amount of cards per suit
            short amountOfSuitPerType = 1;
            List<BasicSuitData> suits = new List<BasicSuitData>() {
                new BasicSuitData( "HEARTS", "red" ),
                new BasicSuitData( "SPADES", "black" )
            };

            // Initialize deck controller and save cards
            List<CardFacade> instantiatedCards = deckController
                                                .InitializeCards( suits, amountOfSuitPerType);


            // Assert the amount of cards instantiated is correct
            Assert.AreEqual( 13 * suits.Count, instantiatedCards.Count,
                            $"The amount of instantiated cards should be "
                                    + $"26 but it's "
                                    + $"{instantiatedCards.Count} instead." );
        }
        #endregion
    }
}

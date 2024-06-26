/*
* Author:	Iris Bermudez
* Date:		26/06/2024
*/



using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Solitaire.GameModes.Klondike;
using Solitaire.Gameplay.CardContainers;
using UnityEditor;
using System;
using Solitaire.Gameplay.Cards;



namespace Tests.Solitaire.GameModes.Klondike {
    public class KlondikeCardContainerTest {
        #region Variables
        private GameObject klondikeCardContainerGameObject;
        private KlondikeCardContainer klondikeCardContainer;

        private const string CARD_PREFAB_PATH = Test.TestConstants.CARD_PREFAB_PATH;
        private const string KLONDIKECARDCONTAINER_PREFAB_PATH = Test.TestConstants
                                                            .KLONDIKECARDCONTAINER_PREFAB_PATH;
        #endregion


        #region Setup
        [SetUp]
        public void Setup() {
            klondikeCardContainerGameObject = GameObject.Instantiate( AssetDatabase
                                                        .LoadAssetAtPath<GameObject>(
                                                            KLONDIKECARDCONTAINER_PREFAB_PATH
                                                    ) );

            if( !klondikeCardContainerGameObject ) {
                throw new NullReferenceException( "GameObject at KLONDIKECARDCONTAINER_PREFAB_PATH "
                        + "could not be loaded." );
            }

            klondikeCardContainer = klondikeCardContainerGameObject.AddComponent<KlondikeCardContainer>();

            if( !klondikeCardContainer ) {
                throw new NullReferenceException( "GameObject at SPIDERCARDCONTAINER_PREFAB_PATH "
                        + "does not contain a SpiderCardContainer component." );
            }
        }
        #endregion


        #region Tests
        [Test]
        public void KlondikeCardContainer_IsAbstractCardContainer() {
            Assert.IsInstanceOf( typeof( AbstractCardContainer ), klondikeCardContainer,
                            "KlondikeCardContainer does not inherit from AbstractCardContainer." );
        }


        [Test]
        public void WhenAddingCards_ThenAmoutOfCardsFromCardContainerChangesAccordingly() {
            //  Generate random amount of cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range( 0, 50 );
            GameObject cardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>( CARD_PREFAB_PATH );

            // Assert CardPrefab was loaded successfully
            Assert.IsNotNull( cardPrefab, "Card prefab could not be loaded." );

            // Check to avoid false positive
            Assert.Zero( klondikeCardContainer.GetCards().Count,
                            "klondikeCardContainer shouldn't contain any cards" );

            //  Add cards to spiderCardContainer
            for( int i = 0; i < amountOfCardsToSpawn; i++ ) {
                klondikeCardContainer.AddCard( GameObject.Instantiate( cardPrefab )
                                                .GetComponent<CardFacade>() );
            }

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.True( amountOfCardsToSpawn == klondikeCardContainer.GetCards().Count,
                            $"klondikeCardContainer cards has {klondikeCardContainer.GetCards().Count} "
                            + $"when it should have {amountOfCardsToSpawn} cards" );
        }


        [Test]
        public void WhenAddingNullObjectToKlondikeCardContainer_ThenThrowNullReferenceExceptionAndCardsAmountRemainsTheSame() {
            // Test to avoid false positive
            Assert.Zero( klondikeCardContainer.GetCards().Count,
                        "There was an alement in klondikeCardContainer before the null object was added." );
            Assert.Throws<NullReferenceException>( () => klondikeCardContainer.AddCard( null ) );
            Assert.Zero( klondikeCardContainer.GetCards().Count,
                        "Null object was added when it shouldn't." );
        }


        [Test]
        public void WhenAddingMultipleCards_ThenThrowsNotImplementedException() {
            // Instantiate cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range( 0, 100 );
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>( CARD_PREFAB_PATH );

            List<CardFacade> cardsToAdd = new List<CardFacade>();
            for( int i = 0; i < amountOfCardsToSpawn; i++ ) {
                cardsToAdd.Add( GameObject.Instantiate( cardFacadePrefab ).GetComponent<CardFacade>() );
            }

            // Check to avoid false positive
            Assert.Zero( klondikeCardContainer.GetCards().Count,
                        "klondikeCardContainer shouldn't contain any cards" );

            // Asserts NotImplementedException is thrown
            Assert.Throws<NotImplementedException>( () => klondikeCardContainer.AddCards( cardsToAdd ) );

            // Assert the amount of cards is still 0
            Assert.Zero( klondikeCardContainer.GetCards().Count,
                        "klondikeCardContainer shouldn't contain any cards" );
        }

        [Test]
        public void WhenInitializing_ThenAddOnlyTheRightAmountOfCards() {
            // Create list of cards to add
            short defaultAmountOfCards = (short)UnityEngine.Random.Range( 1, 50 );
            int amountOfCardsToAdd = UnityEngine.Random.Range( defaultAmountOfCards, 50 );
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>( CARD_PREFAB_PATH );

            klondikeCardContainer.SetDefaultAmountOfCards( defaultAmountOfCards );
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>();
            for( int i = 0; i < amountOfCardsToAdd; i++ ) {
                listOfCardsToAdd.Add( GameObject.Instantiate( cardFacadePrefab ).GetComponent<CardFacade>() );
            }

            // Check there aren't any cards already to avoid false positive
            Assert.Zero( klondikeCardContainer.GetCards().Count,
                        "klondikeCardContainer shouldn't contain any cards but it does." );

            // Initialize spiderCardContainer
            List<CardFacade> remainingCards = klondikeCardContainer.Initialize( listOfCardsToAdd );

            // Check cards have been added successfully
            Assert.AreEqual( defaultAmountOfCards, klondikeCardContainer.GetCards().Count,
                            $"klondikeCardContainer should contain {amountOfCardsToAdd} "
                                    + $"but it has {klondikeCardContainer.GetCards().Count} instead" );
            Assert.AreEqual( ( amountOfCardsToAdd - klondikeCardContainer.GetCards().Count ),
                                remainingCards.Count,
                                $"The remaining cards should be " +
                                $"{amountOfCardsToAdd - klondikeCardContainer.GetCards().Count} "
                                + $"but there are {remainingCards.Count} instead." );
        }
        #endregion
    }
}

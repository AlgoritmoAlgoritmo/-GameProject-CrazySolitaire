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
using Solitaire.Gameplay.Cards;
using System.Collections.Generic;



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
        
        
        [Test]
        public void WhenAddingCard_ThenGetCorrectAmoutOfCardsFromCardContainer() {
            //  Generate random amount of cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range( 0, 50 );
            GameObject cardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>( TestConstants.CARD_PREFAB_PATH );

            // Assert CardPrefab was loaded successfully
            Assert.IsNotNull( cardPrefab, "Card prefab could not be loaded." );

            // Check to avoid false positive
            Assert.Zero( klondikeCompletedColumnContainer.GetCards().Count,
                            "spiderCardContainer shouldn't contain any cards" );

            //  Add cards to spiderCardContainer
            for( int i = 0; i < amountOfCardsToSpawn; i++ ) {
                klondikeCompletedColumnContainer.AddCard( GameObject.Instantiate( cardPrefab )
                                                .GetComponent<CardFacade>() );
            }

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.True( amountOfCardsToSpawn == klondikeCompletedColumnContainer.GetCards().Count,
                        $"spiderCardContainer cards has {klondikeCompletedColumnContainer.GetCards().Count} "
                                + $"when it should have {amountOfCardsToSpawn} cards" );
        }
        
        
        [Test]
        public void WhenAddingNullObjectToCardContainer_ThenThrowNullReferenceExceptionAndDontAddIt() {
            // Test to avoid false positive
            Assert.Zero( klondikeCompletedColumnContainer.GetCards().Count,
                        "There was an alement in klondikeCompletedColumnContainer before the null object was added." );
            Assert.Throws<NullReferenceException>( () => klondikeCompletedColumnContainer.AddCard( null ) );
            Assert.Zero( klondikeCompletedColumnContainer.GetCards().Count,
                        "Null object was added when it shouldn't." );
        }


        [Test]
        public void WhenAddingMultipleCards_ThenThrowsNotImplementedException() {
            // Instantiate cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range( 0, 100 );
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>( TestConstants.CARD_PREFAB_PATH );

            List<CardFacade> cardsToAdd = new List<CardFacade>();
            for( int i = 0; i < amountOfCardsToSpawn; i++ ) {
                cardsToAdd.Add( GameObject.Instantiate( cardFacadePrefab ).GetComponent<CardFacade>() );
            }

            // Check the amount of cards is zero to avoid false positive
            Assert.Zero( klondikeCompletedColumnContainer.GetCards().Count,
                        "klondikeCompletedColumnContainer shouldn't contain any cards" );

            // Assert multiple card addition throws exception
            Assert.Throws<NotImplementedException>( () => klondikeCompletedColumnContainer.AddCards( cardsToAdd ) );

            // Assert the amount of cards added is still zero
            Assert.Zero( klondikeCompletedColumnContainer.GetCards().Count,
                        $"klondikeCompletedColumnContainer cards has "
                                + $"{klondikeCompletedColumnContainer.GetCards().Count} "
                                + $"when it should have 0 cards" );
        }


        [Test]
        public void WhenInitializing_ThenDontAddAnyCard() {
            // Create list of cards to add
            short defaultAmountOfCards = (short)UnityEngine.Random.Range( 1, 50 );
            int amountOfCardsToAdd = UnityEngine.Random.Range( defaultAmountOfCards, 50 );
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>( Test.TestConstants.CARD_PREFAB_PATH );

            klondikeCompletedColumnContainer.SetDefaultAmountOfCards( defaultAmountOfCards );
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>();
            for( int i = 0; i < amountOfCardsToAdd; i++ ) {
                listOfCardsToAdd.Add( GameObject.Instantiate( cardFacadePrefab ).GetComponent<CardFacade>() );
            }

            // Check there aren't any cards already to avoid false positive
            Assert.Zero( klondikeCompletedColumnContainer.GetCards().Count,
                        "spiderCardContainer shouldn't contain any cards but it does." );

            // Initialize spiderCardContainer
            List<CardFacade> remainingCards = klondikeCompletedColumnContainer.Initialize( listOfCardsToAdd );

            // Check cards have been added successfully
            Assert.Zero( klondikeCompletedColumnContainer.GetCards().Count,
                            $"klondikeCompletedColumnContainer shouldn't contain any card "
                                    + $"but it has {klondikeCompletedColumnContainer.GetCards().Count} instead" );
            Assert.AreEqual( amountOfCardsToAdd, remainingCards.Count,
                            $"The remaining cards should be {amountOfCardsToAdd} "
                                    + $"but there are {remainingCards.Count} instead." );
        }

        #endregion
    }
}

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
using Solitaire.Gameplay.CardContainers;
using System.Collections.Generic;
using Solitaire.Gameplay.Cards;

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
            Assert.IsInstanceOf(typeof( KlondikeGameMode ), klondikeGameModeMock,
                               "Make sure KlondikeGameModeMock inherits from KlondikeGameMode." );
        }

        [Test]
        public void KlondikeGameModeMock_Is_AbstractGameMode() {
            Assert.IsInstanceOf( typeof( AbstractGameMode ), klondikeGameModeMock,
                               "Make sure KlondikeGameMode inherits from AbstractGameMode." );
        }

        [Test]
        [TestCase( 2, 4 )]
        [TestCase( 3, 3 )]
        [TestCase( 4, 6 )]
        public void WhenInitializingKlondikeGameMode_ThenDistributesProperAmountOfCardsToEverySingleContainer(
                                                                        short _amountOfContainersToSpawn,
                                                                         short _amountOfCardsPerContainer) {
            // Instantiate Card containers and add them to KlondikeGameModeMock
            List<AbstractCardContainer> cardContainerList = SpawnTheFollowingAmountOfAbstractCardContainers(
                                                                        _amountOfContainersToSpawn );
            foreach( var auxContainer in cardContainerList ) {
                auxContainer.SetDefaultAmountOfCards( _amountOfCardsPerContainer );
            }
            klondikeGameModeMock.SetCardContainers( cardContainerList );

            // Instantiate cards
            List<CardFacade> cardFacadeList = SpawnTheFollowingAmountOfCards( 
                                                    _amountOfCardsPerContainer * _amountOfContainersToSpawn );

            // Check amount of cards before initialization is zero
            Assert.Zero( klondikeGameModeMock.GetAmountOfDistributedCards(),
                        "klondikeGameModeMock shouldn't have any card." );

            // Initialize GameModeMock
            klondikeGameModeMock.Initialize( cardFacadeList );

            // Assert every single container has the correct amount of cards
            Assert.AreEqual( _amountOfCardsPerContainer * _amountOfContainersToSpawn,
                            klondikeGameModeMock.GetAmountOfDistributedCards(),
                            $"klondikeGameModeMock should contain {_amountOfCardsPerContainer * _amountOfContainersToSpawn} "
                                    + $"instead of {klondikeGameModeMock.GetAmountOfDistributedCards()}." );
        }
        #endregion


        #region Private methods
        private List<AbstractCardContainer> SpawnTheFollowingAmountOfAbstractCardContainers( int _amount ) {
            List<AbstractCardContainer> listOfCardContainersToAdd = new List<AbstractCardContainer>();

            GameObject klondikeCardContaierPrefabInstance = GameObject.Instantiate(
                            AssetDatabase.LoadAssetAtPath<GameObject>( TestConstants
                                                                    .KLONDIKECARDCONTAINER_PREFAB_PATH ) );
            listOfCardContainersToAdd.Add( klondikeCardContaierPrefabInstance.GetComponent<AbstractCardContainer>() );

            if( !klondikeCardContaierPrefabInstance )
                throw new NullReferenceException("Couldn't load prefab at "
                                                    + $"{TestConstants.KLONDIKECARDCONTAINER_PREFAB_PATH}.");

            for( int i = 0; i < _amount-1; i++ ) {
                listOfCardContainersToAdd.Add( GameObject.Instantiate( klondikeCardContaierPrefabInstance )
                                                                .GetComponent<AbstractCardContainer>() );
            }

            return listOfCardContainersToAdd;
        }

        private List<CardFacade> SpawnTheFollowingAmountOfCards( int _amountOfCards ) {
            GameObject cardFacadePrefabGameObject = GameObject.Instantiate( AssetDatabase
                                                        .LoadAssetAtPath<GameObject>( 
                                                                TestConstants.CARD_PREFAB_PATH ) );
            CardFacade cardFacadePrefab = cardFacadePrefabGameObject.GetComponent<CardFacade>();
            List<CardFacade> listOfSpawnedCards = new List<CardFacade>();
            for( int i = 0; i < _amountOfCards; i++ ) {
                listOfSpawnedCards.Add( GameObject.Instantiate( cardFacadePrefabGameObject )
                                                                .GetComponent<CardFacade>() );
            }

            return listOfSpawnedCards;
        }
        #endregion
    }
}
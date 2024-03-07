/*
* Author:	Iris Bermudez
* Date:		07/03/2024
*/



using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Solitaire.Gameplay.Spider;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using UnityEditor;
using System.Collections.Generic;

namespace Tests.Solitaire.Gameplay.Spider {
    public class SpiderCardContainerTest {
        #region Variables
        private GameObject spiderCardContainerGameObject;
        private SpiderCardContainer spiderCardContainer;

        private const string CARD_PREFAB_PATH = "Assets/Prefabs/Gameplay/Card Prefab.prefab";
        private const string SPIDERCARDCONTAINER_PREFAB_PATH = "Assets/Prefabs/Gameplay/Spider"
                                                + "/SpiderCardContainer 6Cards.prefab";
        #endregion


        #region Setup
        [SetUp]
        public void Setup() {
            spiderCardContainerGameObject = GameObject.Instantiate( AssetDatabase
                                                        .LoadAssetAtPath<GameObject>(
                                                            SPIDERCARDCONTAINER_PREFAB_PATH
                                                    ));

            if( !spiderCardContainerGameObject ) {
                throw new NullReferenceException("GameObject at SPIDERCARDCONTAINER_PREFAB_PATH "
                        + "could not be loaded.");
            }

            spiderCardContainer = spiderCardContainerGameObject.AddComponent<SpiderCardContainer>();

            if( !spiderCardContainer ) {
                throw new NullReferenceException("GameObject at SPIDERCARDCONTAINER_PREFAB_PATH "
                        + "does not contain a SpiderCardContainer component.");
            }
        }
        #endregion


        #region Tests
        [Test]
        public void SpiderCardContainer_IsAbstractCardContainer() {
            Assert.IsInstanceOf(typeof(AbstractCardContainer), spiderCardContainer,
                        "SpiderCardContainer does not inherit from AbstractCardContainer.");
        }


        [Test]
        public void WhenAddingCard_ThenGetCorrectAmoutOfCardsFromCardContainer() {
            //  Generate random amount of cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range(0, 50);
            GameObject cardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);

            // Assert CardPrefab was loaded successfully
            Assert.IsNotNull(cardPrefab, "Card prefab could not be loaded.");

            // Check to avoid false positive
            Assert.Zero(spiderCardContainer.GetCards().Count,
                            "spiderCardContainer shouldn't contain any cards");

            //  Add cards to spiderCardContainer
            for (int i = 0; i < amountOfCardsToSpawn; i++) {
                spiderCardContainer.AddCard( GameObject.Instantiate(cardPrefab)
                                                .GetComponent<CardFacade>()
                                            );
            }

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.True(amountOfCardsToSpawn == spiderCardContainer.GetCards().Count,
                            $"spiderCardContainer cards has {spiderCardContainer.GetCards().Count} "
                            + $"when it should have {amountOfCardsToSpawn} cards");
        }


        [Test]
        public void WhenAddingNullObjectToSpiderCardContainer_ThenThrowNullReferenceExceptionAndDontAddIt() {
            // Test to avoid false positive
            Assert.Zero(spiderCardContainer.GetCards().Count,
                        "There was an alement in spiderCardContainer before the null object was added.");
            Assert.Throws<NullReferenceException>(() => spiderCardContainer.AddCard(null));
            Assert.Zero(spiderCardContainer.GetCards().Count,
                        "Null object was added when it shouldn't.");
        }


        [Test]
        public void WhenAddingMultipleCards_ThenGetCorrectAmoutOfCardsFromCardContainer() {
            // Instantiate cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range(0, 100);
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);

            List<CardFacade> cardsToAdd = new List<CardFacade>();
            for (int i = 0; i < amountOfCardsToSpawn; i++) {
                cardsToAdd.Add( GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>() );
            }

            // Check to avoid false positive
            Assert.Zero( spiderCardContainer.GetCards().Count,
                        "spiderCardContainer shouldn't contain any cards");

            // Add cards to spiderCardContainer
            spiderCardContainer.AddCards(cardsToAdd);

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.True(amountOfCardsToSpawn == spiderCardContainer.GetCards().Count,
                        $"spiderCardContainer cards has {spiderCardContainer.GetCards().Count} "
                            + $"when it should have {amountOfCardsToSpawn} cards");
        }


        [Test]
        public void WhenAddingCardListWithNullObjectToSpiderCardContainer_ThenThrowNullReferenceExceptionAndDontAddIt() {
            //  Create list of cards
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>() {
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    null,
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>()
                                };

            //  Check spiderCardContainer has 0 cards
            Assert.Zero(spiderCardContainer.GetCards().Count,
                            "spiderCardContainer shouldn't contain any cards but it does.");

            //  Assert list contains null object
            Assert.True(listOfCardsToAdd.Contains(null),
                        "listOfCardsToAdd should contain a null element, but it does not.");

            //  Assert mutltiple card addition error
            Assert.Throws<NullReferenceException>( () => spiderCardContainer.AddCards(listOfCardsToAdd));

            //  Check no card was added
            Assert.Zero(spiderCardContainer.GetCards().Count,
                            "At least one card has been added to spiderCardContainer when it shouldn't.");
        }
        #endregion
    }
}

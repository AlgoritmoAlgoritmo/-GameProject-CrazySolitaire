/*
* Author:	Iris Bermudez
* Date:		07/03/2024
*/



using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using Solitaire.Gameplay.Spider;
using UnityEditor;
using UnityEngine;



namespace Tests.Solitaire.Gameplay.Spider {
    public class SpiderCardContainerForCardDistributorTest {
        #region Variables
        private GameObject spiderCardContainerForCardDistributorGameObject;
        private SpiderCardContainerForCardDistributor spiderCardContainerForCardDistributor;

        private const string CARD_PREFAB_PATH = "Assets/Prefabs/Gameplay/Card Prefab.prefab";
        private const string SPIDER_CARDCONTAINER_FORCARDDISTRIBUTION_PREFAB_PATH = "Assets/Prefabs/Gameplay/Spider"
                                                + "/CardDistributor.prefab";
        #endregion


        #region Tests set up
        [SetUp]
        public void Setup() {
            spiderCardContainerForCardDistributorGameObject = AssetDatabase.LoadAssetAtPath<GameObject>(
                                                SPIDER_CARDCONTAINER_FORCARDDISTRIBUTION_PREFAB_PATH);

            if (!spiderCardContainerForCardDistributorGameObject) {
                throw new NullReferenceException("GameObject at spiderCardContainerForCardDistributor_PREFAB_PATH "
                        + "could not be loaded.");
            }

            spiderCardContainerForCardDistributor = spiderCardContainerForCardDistributorGameObject
                                                    .AddComponent<SpiderCardContainerForCardDistributor>();

            if( !spiderCardContainerForCardDistributor ) {
                throw new NullReferenceException( $"GameObject at "
                                + $"{SPIDER_CARDCONTAINER_FORCARDDISTRIBUTION_PREFAB_PATH} "
                                + "does not contain a spiderCardContainerForCardDistributor component.");
            }
        }
        #endregion


        #region Tests
        [Test]
        public void SpiderCardContainerForCardDistributor_IsAbstractCardContainer() {
            Assert.IsInstanceOf( typeof(AbstractCardContainer),
                                spiderCardContainerForCardDistributor,
                                "SpiderCardContainerForCardDistributor must inherit from "
                                    + "AbstractCardContainer");
        }


        [Test]
        public void WhenAddingACard_ThenThrowsNotImplementedException() {
            //  Instantiate cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range(0, 50);
            GameObject cardGameObject = GameObject.Instantiate(new GameObject());

            // Check to avoid false positive
            Assert.Zero(spiderCardContainerForCardDistributor.GetCards().Count,
                            "spiderCardContainerForCardDistributor shouldn't contain any cards");

            //  Assert addition of card
            for (int i = 0; i < amountOfCardsToSpawn; i++) {
                Assert.Throws<System.NotImplementedException>( 
                                                () => spiderCardContainerForCardDistributor
                                                    .AddCard(cardGameObject.AddComponent<CardFacade>()));
            }

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.Zero( spiderCardContainerForCardDistributor.GetCards().Count,
                        $"spiderCardContainerForCardDistributor cards has "
                            + $"{spiderCardContainerForCardDistributor.GetCards().Count} "
                            + $"when it should have 0 cards");
        }


        [Test]
        public void WhenAddingMultipleCards_ThenThrowsNotImplementedException() {
            // Instantiate cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range(0, 100);
            GameObject cardGameObject = GameObject.Instantiate(new GameObject());
            List<CardFacade> cardsToAdd = new List<CardFacade>();
            for (int i = 0; i < amountOfCardsToSpawn; i++) {
                cardsToAdd.Add(cardGameObject.AddComponent<CardFacade>());
            }

            // Check to avoid false positive
            Assert.Zero(spiderCardContainerForCardDistributor.GetCards().Count,
                            "spiderCardContainerForCardDistributor shouldn't contain any cards");

            // Assert addition of multiple cards
            Assert.Throws<NotImplementedException>( () => 
                                                    spiderCardContainerForCardDistributor.AddCards(cardsToAdd),
                                                    "he addition of multiple cards should throw a "
                                                        + "NotImplementedException.");

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.Zero( spiderCardContainerForCardDistributor.GetCards().Count,
                        $"spiderCardContainerForCardDistributor cards has "
                                + $"{spiderCardContainerForCardDistributor.GetCards().Count} "
                                + $"when it should have 0 cards");
        }


        [Test]
        public void WhenInitializing_ThenAddOnlyTheRightAmountOfCards() {
            // Create list of cards to add
            short defaultAmountOfCards = (short)UnityEngine.Random.Range(1, 50);
            int amountOfCardsToAdd = UnityEngine.Random.Range(defaultAmountOfCards, 50);
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);

            spiderCardContainerForCardDistributor.SetDefaultAmountOfCards(defaultAmountOfCards);
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>();
            for (int i = 0; i < amountOfCardsToAdd; i++) {
                listOfCardsToAdd.Add(GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>());
            }

            // Check there aren't any cards already to avoid false positive
            Assert.Zero(spiderCardContainerForCardDistributor.GetCards().Count,
                        "spiderCardContainerForCardDistributor shouldn't contain any cards but it does.");

            // Initialize spiderCardContainer
            List<CardFacade> remainingCards = spiderCardContainerForCardDistributor.Initialize(listOfCardsToAdd);

            // Check cards have been added successfully
            Assert.AreEqual(defaultAmountOfCards, spiderCardContainerForCardDistributor.GetCards().Count,
                            $"spiderCardContainerForCardDistributor should contain {amountOfCardsToAdd} "
                                    + $"but it has {spiderCardContainerForCardDistributor.GetCards().Count} instead");
            Assert.AreEqual((amountOfCardsToAdd - spiderCardContainerForCardDistributor.GetCards().Count),
                                remainingCards.Count,
                                $"The remaining cards should be " +
                                $"{amountOfCardsToAdd - spiderCardContainerForCardDistributor.GetCards().Count} "
                                + $"but there are {remainingCards.Count} instead.");
        }


        [Test]
        public void WhenCallingRemoveCardMethod_ThenEliminateItFromspiderCardContainerCards() {
            //  Instantiate cards to add
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>() {
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>()
                                };
            int amountOfCardsToAdd = listOfCardsToAdd.Count;
            spiderCardContainerForCardDistributor.SetDefaultAmountOfCards((short)amountOfCardsToAdd);
            int indexOfTheCardToBeRemoved = UnityEngine.Random.Range(0, listOfCardsToAdd.Count - 1);
            CardFacade cardToRemove = listOfCardsToAdd[indexOfTheCardToBeRemoved];

            //  Check spiderCardContainer doesn't have cards already
            Assert.Zero(spiderCardContainerForCardDistributor.GetCards().Count,
                        "casicCardContainer shouldn't contain any card but it does.");

            //  Initialize spiderCardContainer
            spiderCardContainerForCardDistributor.Initialize( listOfCardsToAdd );

            //  Remove card
            spiderCardContainerForCardDistributor.RemoveCard(cardToRemove);

            //  Assert container doesn't have that card anymore and that the amount of cards is correct
            Assert.False(spiderCardContainerForCardDistributor.GetCards().Contains(cardToRemove),
                        "spiderCardContainer still  has the card that should have been removed.");

            Assert.False(spiderCardContainerForCardDistributor.GetCards().Contains(null),
                            "spiderCardContainerForCardDistributor contains a null element in "
                                    + "its list of cards.");

            Assert.AreEqual( amountOfCardsToAdd -1,
                            spiderCardContainerForCardDistributor.GetCards().Count,
                            "The amount of cards in spiderCardContainerForCardDistributor should be "
                                    + $"{amountOfCardsToAdd - 1} instead of "
                                    + $"{spiderCardContainerForCardDistributor.GetCards().Count} ");


        }
        #endregion
    }
}

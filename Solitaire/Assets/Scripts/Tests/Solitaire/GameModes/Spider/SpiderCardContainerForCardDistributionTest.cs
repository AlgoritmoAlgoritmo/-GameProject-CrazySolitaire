/*
* Author:	Iris Bermudez
* Date:		07/03/2024
*/



using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using Solitaire.GameModes.Spider;



namespace Tests.Solitaire.GameModes.Spider {
    public class SpiderCardContainerForCardDistributionTest {
        #region Variables
        private GameObject spiderCardContainerForCardDistributorGameObject;
        private SpiderCardContainerForCardDistribution spiderCardContainerForCardDistribution;

        private const string CARD_PREFAB_PATH = "Assets/Prefabs/Gameplay/Card Prefab.prefab";
        private const string SPIDER_CARDCONTAINER_FORCARDDISTRIBUTION_PREFAB_PATH = "Assets/Prefabs/Gameplay/"
                                                    + "Spider/CardDistributor.prefab";
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

            spiderCardContainerForCardDistribution = spiderCardContainerForCardDistributorGameObject
                                                    .AddComponent<SpiderCardContainerForCardDistribution>();

            if( !spiderCardContainerForCardDistribution) {
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
                                spiderCardContainerForCardDistribution,
                                "SpiderCardContainerForCardDistributor must inherit from "
                                    + "AbstractCardContainer");
        }


        [Test]
        public void WhenAddingACard_ThenThrowsNotImplementedException() {
            //  Instantiate cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range(0, 50);
            GameObject cardGameObject = GameObject.Instantiate(new GameObject());

            // Check to avoid false positive
            Assert.Zero(spiderCardContainerForCardDistribution.GetCards().Count,
                            "spiderCardContainerForCardDistributor shouldn't contain any cards");

            //  Assert addition of card
            for (int i = 0; i < amountOfCardsToSpawn; i++) {
                Assert.Throws<System.NotImplementedException>( 
                                                () => spiderCardContainerForCardDistribution
                                                    .AddCard(cardGameObject.AddComponent<CardFacade>()));
            }

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.Zero(spiderCardContainerForCardDistribution.GetCards().Count,
                        $"spiderCardContainerForCardDistributor cards has "
                            + $"{spiderCardContainerForCardDistribution.GetCards().Count} "
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
            Assert.Zero(spiderCardContainerForCardDistribution.GetCards().Count,
                            "spiderCardContainerForCardDistributor shouldn't contain any cards");

            // Assert addition of multiple cards
            Assert.Throws<NotImplementedException>( () =>
                                                    spiderCardContainerForCardDistribution.AddCards(cardsToAdd),
                                                    "he addition of multiple cards should throw a "
                                                        + "NotImplementedException.");

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.Zero(spiderCardContainerForCardDistribution.GetCards().Count,
                        $"spiderCardContainerForCardDistributor cards has "
                                + $"{spiderCardContainerForCardDistribution.GetCards().Count} "
                                + $"when it should have 0 cards");
        }


        [Test]
        public void WhenInitializing_ThenAddOnlyTheRightAmountOfCards() {
            // Create list of cards to add
            short defaultAmountOfCards = (short)UnityEngine.Random.Range(1, 50);
            int amountOfCardsToAdd = UnityEngine.Random.Range(defaultAmountOfCards, 50);
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);

            spiderCardContainerForCardDistribution.SetDefaultAmountOfCards(defaultAmountOfCards);
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>();
            for (int i = 0; i < amountOfCardsToAdd; i++) {
                listOfCardsToAdd.Add(GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>());
            }

            // Check there aren't any cards already to avoid false positive
            Assert.Zero(spiderCardContainerForCardDistribution.GetCards().Count,
                        "spiderCardContainerForCardDistributor shouldn't contain any cards but it does.");

            // Initialize spiderCardContainer
            List<CardFacade> remainingCards = spiderCardContainerForCardDistribution.Initialize(listOfCardsToAdd);

            // Check cards have been added successfully
            Assert.AreEqual(defaultAmountOfCards, spiderCardContainerForCardDistribution.GetCards().Count,
                            $"spiderCardContainerForCardDistributor should contain {amountOfCardsToAdd} "
                                    + $"but it has {spiderCardContainerForCardDistribution.GetCards().Count} instead");
            Assert.AreEqual((amountOfCardsToAdd - spiderCardContainerForCardDistribution.GetCards().Count),
                                remainingCards.Count,
                                $"The remaining cards should be " +
                                $"{amountOfCardsToAdd - spiderCardContainerForCardDistribution.GetCards().Count} "
                                + $"but there are {remainingCards.Count} instead.");
        }


        [Test]
        public void WhenTriesToInitializeAListWithANullElement_ThenThorwNullReferenceExceptionBeforeAddingAnyCard() {
            // Create list of cards
            GameObject cardsGameObject = GameObject.Instantiate(new GameObject());
            List<CardFacade> listForInitialization = new List<CardFacade>() {
                                                        cardsGameObject.AddComponent<CardFacade>(),
                                                        null,
                                                        cardsGameObject.AddComponent<CardFacade>()
                                                    };
            int amountOfCardsToAddForInitialization = listForInitialization.Count;

            // Check basic card container doesn't have any cards to avoid false positive
            Assert.Zero(spiderCardContainerForCardDistribution.GetCards().Count,
                        "casicCardContainer shouldn't contain any card but it does.");

            // Assert initialization
            Assert.Throws<System.NullReferenceException>(() =>
                                                        spiderCardContainerForCardDistribution.Initialize(
                                                                listForInitialization),
                                                    "spiderCardContainerForCardDistributor should have thrown a "
                                                            + "NullReferenceException error since at least one of "
                                                            + "the elements of the list is null.");

            // Check no card was added
            Assert.Zero(spiderCardContainerForCardDistribution.GetCards().Count,
                        "Cards have been added to spiderCardContainerForCardDistributor when they shouldn't.");
            Assert.AreEqual(amountOfCardsToAddForInitialization,
                            listForInitialization.Count,
                            "listForInitialization amount of elements should be "
                            + $"{amountOfCardsToAddForInitialization} but it's "
                            + $"{listForInitialization.Count} instead."
                        );
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
            spiderCardContainerForCardDistribution.SetDefaultAmountOfCards((short)amountOfCardsToAdd);
            int indexOfTheCardToBeRemoved = UnityEngine.Random.Range(0, listOfCardsToAdd.Count - 1);
            CardFacade cardToRemove = listOfCardsToAdd[indexOfTheCardToBeRemoved];

            //  Check spiderCardContainer doesn't have cards already
            Assert.Zero(spiderCardContainerForCardDistribution.GetCards().Count,
                        "casicCardContainer shouldn't contain any card but it does.");

            //  Initialize spiderCardContainer
            spiderCardContainerForCardDistribution.Initialize( listOfCardsToAdd );

            //  Remove card
            spiderCardContainerForCardDistribution.RemoveCard(cardToRemove);

            //  Assert container doesn't have that card anymore and that the amount of cards is correct
            Assert.False(spiderCardContainerForCardDistribution.GetCards().Contains(cardToRemove),
                        "spiderCardContainer still  has the card that should have been removed.");

            Assert.False(spiderCardContainerForCardDistribution.GetCards().Contains(null),
                            "spiderCardContainerForCardDistributor contains a null element in "
                                    + "its list of cards.");

            Assert.AreEqual( amountOfCardsToAdd -1,
                            spiderCardContainerForCardDistribution.GetCards().Count,
                            "The amount of cards in spiderCardContainerForCardDistributor should be "
                                    + $"{amountOfCardsToAdd - 1} instead of "
                                    + $"{spiderCardContainerForCardDistribution.GetCards().Count} ");


        }


        [Test]
        public void WhenPassingNullToRemoveCardMethod_ThenThrowNullReferenceException() {
            // Assert null reference
            Assert.Throws<NullReferenceException>(() => spiderCardContainerForCardDistribution
                                                                                .RemoveCard(null));
        }


        [Test]
        public void WhenCallMethodForMultipleCardRemoval_ThenThrowNotImplementedException() {
            //  Instantiate cards to add
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>() {
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>()
                                };
            int amountOfCardsToAdd = listOfCardsToAdd.Count;
            spiderCardContainerForCardDistribution.SetDefaultAmountOfCards((short)amountOfCardsToAdd);

            //  Create list of cards to remove
            List<CardFacade> listOfCardsToRemove = new List<CardFacade>() {
                listOfCardsToAdd[1],
                listOfCardsToAdd[3],
                listOfCardsToAdd[5]
            };

            //  Add cards to containner
            spiderCardContainerForCardDistribution.Initialize(listOfCardsToAdd);

            //  Save amount of cards
            int amountOfCardsAfterAddition = spiderCardContainerForCardDistribution.GetCards().Count;

            //  Assert ramoval of multiple cards
            Assert.Throws<NotImplementedException>( () =>
                                    spiderCardContainerForCardDistribution.RemoveCards(listOfCardsToRemove),
                                    "spiderCardContainerForCardDistributor.RemoveCards must throw a "
                                            + "NotImplementedException."
                                );

            //  Assert the remaining amount of cards in container is correct
            Assert.AreEqual(amountOfCardsAfterAddition,
                            spiderCardContainerForCardDistribution.GetCards().Count,
                            $"The container has {spiderCardContainerForCardDistribution.GetCards().Count} "
                                    + $"when it was expected it to have {amountOfCardsToAdd}.");
        }
        #endregion
    }
}

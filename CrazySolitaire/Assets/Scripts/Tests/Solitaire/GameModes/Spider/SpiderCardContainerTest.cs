/*
* Author:	Iris Bermudez
* Date:		07/03/2024
*/



using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using Solitaire.GameModes.Spider;



namespace Tests.Solitaire.GameModes.Spider {
    public class SpiderCardContainerTest {
        #region Variables
        private GameObject spiderCardContainerGameObject;
        private SpiderCardContainer spiderCardContainer;

        private const string CARD_PREFAB_PATH = Test.TestConstants.CARD_PREFAB_PATH;
        private const string SPIDERCARDCONTAINER_PREFAB_PATH = Test.TestConstants
                                                    .SPIDERCARDCONTAINER_PREFAB_PATH;
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


        [Test]
        public void WhenInitializing_ThenAddOnlyTheRightAmountOfCards() {
            // Create list of cards to add
            short defaultAmountOfCards = (short)UnityEngine.Random.Range(1, 50);
            int amountOfCardsToAdd = UnityEngine.Random.Range(defaultAmountOfCards, 50);
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);

            spiderCardContainer.SetDefaultAmountOfCards(defaultAmountOfCards);
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>();
            for (int i = 0; i < amountOfCardsToAdd; i++) {
                listOfCardsToAdd.Add( GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>());
            }

            // Check there aren't any cards already to avoid false positive
            Assert.Zero( spiderCardContainer.GetCards().Count,
                        "spiderCardContainer shouldn't contain any cards but it does.");

            // Initialize spiderCardContainer
            List<CardFacade> remainingCards = spiderCardContainer.Initialize(listOfCardsToAdd);

            // Check cards have been added successfully
            Assert.AreEqual(defaultAmountOfCards, spiderCardContainer.GetCards().Count,
                            $"spiderCardContainer should contain {amountOfCardsToAdd} "
                                    + $"but it has {spiderCardContainer.GetCards().Count} instead");
            Assert.AreEqual((amountOfCardsToAdd - spiderCardContainer.GetCards().Count),
                                remainingCards.Count,
                                $"The remaining cards should be " +
                                $"{amountOfCardsToAdd - spiderCardContainer.GetCards().Count} "
                                + $"but there are {remainingCards.Count} instead.");
        }


        [Test]
        public void WhenTriesToInitializeAListWithANullElement_ThenThorwNullReferenceExceptionBeforeAddingAnyCard() {
            // Create list of cards
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);
            List<CardFacade> listForInitialization = new List<CardFacade>() {
                                                GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                                                null,
                                                GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>()
                                            };
            int amountOfCardsToAddForInitialization = listForInitialization.Count;

            // Check basic card container doesn't have any cards to avoid false positive
            Assert.Zero(spiderCardContainer.GetCards().Count,
                        "casicCardContainer shouldn't contain any card but it does.");

            // Assert initialization
            Assert.Throws<System.NullReferenceException>(() =>
                                                   spiderCardContainer.Initialize(listForInitialization),
                                                    "spiderCardContainer should have thrown a "
                                                    + "NullReferenceException error since at least one of "
                                                    + "the elements of the list is null.");

            // Check no card was added
            Assert.Zero(spiderCardContainer.GetCards().Count,
                        "Cards have been added to spiderCardContainer when they shouldn't.");
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

            //  Check spiderCardContainer doesn't have cards already
            Assert.Zero(spiderCardContainer.GetCards().Count,
                        "casicCardContainer shouldn't contain any card but it does.");

            //  Add cards  to spiderCardContainer
            spiderCardContainer.AddCards(listOfCardsToAdd);

            //  Remove card
            int indexOfTheCardToBeRemoved = UnityEngine.Random.Range(0, listOfCardsToAdd.Count - 1);
            spiderCardContainer.RemoveCard(listOfCardsToAdd[indexOfTheCardToBeRemoved]);

            //  Assert container doesn't have that card anymore and that the amount of cards is correct
            Assert.False(spiderCardContainer.GetCards().Contains(listOfCardsToAdd[indexOfTheCardToBeRemoved]),
                        "spiderCardContainer still  has the card that should have been removed.");
            Assert.AreEqual(listOfCardsToAdd.Count - 1,
                            spiderCardContainer.GetCards().Count,
                            "The amount of cards in spiderCardContainer should be "
                            + $"{listOfCardsToAdd.Count - 1} instead of {spiderCardContainer.GetCards().Count} ");
        }


        [Test]
        public void WhenPassingNullToRemoveCardMethod_ThenThrowNullReferenceException() {
            // Assert null reference
            Assert.Throws<NullReferenceException>(() => spiderCardContainer.RemoveCard(null));
        }


        [Test]
        public void WhenCallMethodForMultipleCardRemoval_ThenRemoveOnlyTheRightAmountOfCards() {
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

            //  Add cards to containner
            spiderCardContainer.AddCards(listOfCardsToAdd);

            //  Save amount of cards
            int amountOfCardsAfterAddition = spiderCardContainer.GetCards().Count;

            //  Create list of cards to remove
            List<CardFacade> listOfCardsToRemove = new List<CardFacade>() {
                listOfCardsToAdd[1],
                listOfCardsToAdd[3],
                listOfCardsToAdd[5]
            };

            //  Remove multiple cards
            spiderCardContainer.RemoveCards(listOfCardsToRemove);

            //  Assert the remaining amount of cards in container is correct
            Assert.AreEqual(amountOfCardsAfterAddition - listOfCardsToRemove.Count,
                            spiderCardContainer.GetCards().Count,
                            $"The container has {spiderCardContainer.GetCards().Count} "
                                    + "when it was expected it to have "
                                    + $"{amountOfCardsAfterAddition - listOfCardsToRemove.Count}.");

            //  Assert cards for removal are not referenced by container
            foreach (var auxCard in listOfCardsToRemove) {
                Assert.False(spiderCardContainer.GetCards().Contains(auxCard),
                            "At least one of the cards from listOfCardsToRemove "
                                    + "wasn't successfully removed."
                    );
            }
        }


        [Test]
        public void WhenPassingListWithNullElementToRemoveCardMethod_ThenThrowNullReferenceException() {
            //  Create list of cards
            GameObject cardFacadePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>() {
                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>(),
                    GameObject.Instantiate(cardFacadePrefab).GetComponent<CardFacade>()
            };

            //  Add cards to container
            spiderCardContainer.AddCards(listOfCardsToAdd);

            //  Create list of cards to remove with a null element in it
            List<CardFacade> listOfCardsToRemove = new List<CardFacade>() {
                listOfCardsToAdd[1],
                null,
                listOfCardsToAdd[5]
            };

            //  Assert remove method
            Assert.Throws<NullReferenceException>(() => spiderCardContainer.RemoveCards(
                                                                    listOfCardsToRemove));
        }
        #endregion
    }
}

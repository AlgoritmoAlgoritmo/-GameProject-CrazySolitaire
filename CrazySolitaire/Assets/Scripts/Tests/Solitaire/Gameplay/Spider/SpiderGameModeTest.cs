/*
* Author:	Iris Bermudez
* Date:		08/03/2024
*/



using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Solitaire.Gameplay.Spider;
using Solitaire.Gameplay;
using Solitaire.Gameplay.Cards;
using Solitaire.Gameplay.CardContainers;



namespace Tests.Solitaire.Gameplay.Spider {
    public class SpiderGameModeTest {
        #region Variables
        private GameObject spiderGameModeMockGameObject;
        private SpiderGameModeMock spiderGameModeMock;

        private const string SPIDERGAMEMODEMOCK_PREFAB_PATH = "Assets/Scripts/Tests/Solitaire/"
                                                + "Gameplay/Spider/SpiderGameModeMock Prefab.prefab";
        private const string CARD_PREFAB_PATH = "Assets/Prefabs/Gameplay/Card Prefab.prefab";
        private const string SPIDERCARDCONTAINERM_PREFAB_PATH = "Assets/Prefabs/Gameplay/Spider"
                                                + "/SpiderCardContainer 6Cards.prefab";
        #endregion


        #region Tests set up
        [SetUp]
        public void Setup() {
            spiderGameModeMockGameObject = GameObject.Instantiate( AssetDatabase
                                                            .LoadAssetAtPath<GameObject>(
                                                                SPIDERGAMEMODEMOCK_PREFAB_PATH));
            if (!spiderGameModeMockGameObject) {
                throw new NullReferenceException($"GameObject at {SPIDERGAMEMODEMOCK_PREFAB_PATH} "
                                                    + "could not be loaded.");
            }

            spiderGameModeMock = spiderGameModeMockGameObject.GetComponent<SpiderGameModeMock>();
            if (!spiderGameModeMock) {
                throw new NullReferenceException($"GameObject at {SPIDERGAMEMODEMOCK_PREFAB_PATH} "
                                                    + "does not contain a SpiderGameMode component.");
            }
        }
        #endregion


        #region Tests
        [Test]
        public void SpiderGameModeMock_InheritsFromSpiderGameMode() {
            Assert.IsInstanceOf(typeof(SpiderGameMode), spiderGameModeMock,
                                "SpiderGameModeMock must inherit from SpiderGameMode instance.");
        }

        [Test]
        public void SpiderGameMode_InheritsFromAbstractGameMode() {
            // Instantiate SpiderGameMode object
            SpiderGameMode spiderGameMode = GameObject.Instantiate(new GameObject())
                                                            .AddComponent<SpiderGameMode>();

            // Assert SpiderGameMode inherits from AbstractGameMode
            Assert.IsInstanceOf(typeof(AbstractGameMode), spiderGameMode,
                                "spiderGameMode must be a AbstractGameMode instance.");
        }

        [Test]
        public void WhenInitializing_ThenDistributeCardsToContainersPropperly() {
            int amountOfAbstractCardContainersToSpawn = 2;
            int amountOfCardsToSpawn = 26;

            // Instantiate CardContainers and add them to spiderGameModeMock
            List<AbstractCardContainer> listOfCardContainersToAdd = SpawnTheFollowingAmountOfAbstractCardContainers(
                                                                                amountOfAbstractCardContainersToSpawn);
            // Set up amount of cards to spawn
            spiderGameModeMock.SetAmountPerSuit((short)amountOfAbstractCardContainersToSpawn);
            foreach( var auxContainer in listOfCardContainersToAdd ) {
                auxContainer.SetDefaultAmountOfCards((short) (amountOfCardsToSpawn / listOfCardContainersToAdd.Count));
            }
            spiderGameModeMock.SetCardContainers(listOfCardContainersToAdd);

            // Create list of cards for SpiderGameMode initialization
            List<CardFacade> listOfCardsToAdd = SpawnTheFollowingamountOfCards(amountOfCardsToSpawn);

            // Check amount of cards before initialization
            Assert.Zero( spiderGameModeMock.GetAmountOfDistributedCards(),
                        "spiderGameModeMock shouldn't have any card.");

            // Initialize SpiderGameMode
            spiderGameModeMock.Initialize(listOfCardsToAdd);

            // Check amount of distributed cards after initialization
            Assert.AreEqual(amountOfCardsToSpawn, spiderGameModeMock.GetAmountOfDistributedCards(),
                            $"spiderGameModeMock should contain {amountOfCardsToSpawn} "
                                    + $"instead of {spiderGameModeMock.GetAmountOfDistributedCards()}.");
        }

        [Test]
        public void WhenInitializingEmptyListOfCards_ThenThrowIndexOutOfRangeException() {
            List<CardFacade> listOfCards = new List<CardFacade>();
            int currentAmountOfCardsBeforeInitialization = spiderGameModeMock.GetAmountOfDistributedCards();


            Assert.Throws<IndexOutOfRangeException>(() => spiderGameModeMock.Initialize(
                                listOfCards),
                        "spiderGameModeMock didn't throw IndexOutOfRangeException as expected. "
                                + "Check if the list passed is empty."
            );

            // Assert the amount of cards from spiderGameModeMock didn't change
            Assert.AreEqual(currentAmountOfCardsBeforeInitialization,
                            spiderGameModeMock.GetAmountOfDistributedCards(),
                            "spiderGameModeMock amount of cards changed: it should have "
                                    + $"{currentAmountOfCardsBeforeInitialization} instead of "
                                    + $"{spiderGameModeMock.GetAmountOfDistributedCards()}." );
        }

        [Test]
        public void WhenInitializingListWithNullElement_ThenThrowNullReferenceException() {
            // Create list of cards
            List<CardFacade> listOfCards = SpawnTheFollowingamountOfCards(10);
            int currentAmountOfCardsBeforeInitialization = spiderGameModeMock.GetAmountOfDistributedCards();


            // Add null element to list
            listOfCards[UnityEngine.Random.Range(0, listOfCards.Count - 1)] = null;


            // Assert addition of list of cards with null element
            Assert.Throws<NullReferenceException>( () => spiderGameModeMock.Initialize(listOfCards),
                            "spiderGameModeMock didn't throw NullReferenceException as expected. "
                                    + "Check if the list contains a null element."
            );


            // Assert the amount of cards from spiderGameModeMock didn't change
            Assert.AreEqual(currentAmountOfCardsBeforeInitialization,
                            spiderGameModeMock.GetAmountOfDistributedCards(),
                            "spiderGameModeMock amount of cards changed: it should have "
                                    + $"{currentAmountOfCardsBeforeInitialization} instead of "
                                    + $"{spiderGameModeMock.GetAmountOfDistributedCards()}.");
        }

        [Test]
        public void WhenInitializingWithNullObject_ThenThrowNullReferenceException() {
            int currentAmountOfCardsBeforeInitialization = spiderGameModeMock.GetAmountOfDistributedCards();

            // Assert addition of list of cards with null element
            Assert.Throws<NullReferenceException>(() => spiderGameModeMock.Initialize(null),
                            "spiderGameModeMock didn't throw NullReferenceException as expected. "
                                    + "Check if the object is actually null."
            );


            // Assert the amount of cards from spiderGameModeMock didn't change
            Assert.AreEqual(currentAmountOfCardsBeforeInitialization,
                            spiderGameModeMock.GetAmountOfDistributedCards(),
                            "spiderGameModeMock amount of cards changed: it should have "
                                    + $"{currentAmountOfCardsBeforeInitialization} instead of "
                                    + $"{spiderGameModeMock.GetAmountOfDistributedCards()}.");
        }

        [Test]
        public void WhenCardIsFacingDown_ThenDeactivateDragging() {
            // Create facing down card
            CardFacade card = SpawnNewCard();

            // Set card facing down
            card.FlipCard(false);

            // Set card can bedragging to true
            card.SetCanBeDragged(true);

            // Validate card dragging
            spiderGameModeMock.ValidateCardDragging(card);

            // Assert cannot be dragged
            Assert.IsFalse(card.gameObject.GetComponent<CardPhysics>().CanBeDragged,
                            "card shouldn't be draggable." );
        }


        [Test]
        public void WhenCardIsFacingUpWithNoChild_ThenActivateDragging() {
            // Create card to validate
            CardFacade card = SpawnNewCard();

            // Set card facing up without child
            card.FlipCard(true);
            card.SetChildCard( null );

            // Set card can bedragging to false
            card.SetCanBeDragged(false);

            // Validate card dragging
            spiderGameModeMock.ValidateCardDragging(card);

            // Assert cannot be dragged
            Assert.IsTrue( card.gameObject.GetComponent<CardPhysics>().CanBeDragged,
                            "card should be draggable.");
        }


        [Test]
        public void WhenCardIsFacingUpWithValidChild_ThenActivateDragging() {
            // Create card to validate
            CardFacade cardToValidate = SpawnNewCard();
            cardToValidate.SetCardData(new CardData(5, "SPADES", "BLACK", "PARENT"));

            // Create calid child card
            CardFacade childCard = SpawnNewCard();
            childCard.SetCardData(new CardData(4, "SPADES", "BLACK", "CHILD"));

            // Set prent facing up and child card
            cardToValidate.FlipCard(true);
            cardToValidate.SetChildCard(childCard);

            // Set card can bedragging to false
            cardToValidate.SetCanBeDragged(false);

            // Validate card dragging
            spiderGameModeMock.ValidateCardDragging(cardToValidate);

            // Assert cannot be dragged
            Assert.IsTrue(cardToValidate.gameObject.GetComponent<CardPhysics>().CanBeDragged,
                            "card should be draggable. Check its child card is valid.");
        }


        [Test]
        public void WhenCardIsFacingUpWithAChildOfAnotherSuit_ThenDeactivateDragging() {
            // Create card to validate
            CardFacade cardToValidate = SpawnNewCard();
            cardToValidate.SetCardData(new CardData(5, "SPADES", "BLACK", "PARENT"));

            // Create calid child card
            CardFacade childCard = SpawnNewCard();
            childCard.SetCardData(new CardData(4, "CLOVER", "BLACK", "CHILD"));

            // Set prent facing up and child card
            cardToValidate.FlipCard(true);
            cardToValidate.SetChildCard(childCard);

            // Set card can bedragging to false
            cardToValidate.SetCanBeDragged(true);

            // Validate card dragging
            spiderGameModeMock.ValidateCardDragging(cardToValidate);

            // Assert cannot be dragged
            Assert.IsFalse(cardToValidate.gameObject.GetComponent<CardPhysics>().CanBeDragged,
                            "card should not be draggable. Check its child card is invalid.");
        }


        [Test]
        public void WhenCardIsFacingUpWithAChildWithAnInvalidNumber_ThenDeactivateDragging() {
            // Create card to validate
            CardFacade cardToValidate = SpawnNewCard();
            cardToValidate.SetCardData(new CardData(5, "HEARTS", "RED", "PARENT"));

            // Create calid child card
            CardFacade childCard = SpawnNewCard();
            childCard.SetCardData(new CardData(6, "HEARTS", "RED", "CHILD"));

            // Set prent facing up and child card
            cardToValidate.FlipCard(true);
            cardToValidate.SetChildCard(childCard);

            // Set card can bedragging to false
            cardToValidate.SetCanBeDragged(true);

            // Validate card dragging
            spiderGameModeMock.ValidateCardDragging(cardToValidate);

            // Assert cannot be dragged
            Assert.IsFalse(cardToValidate.gameObject.GetComponent<CardPhysics>().CanBeDragged,
                            "card should not be draggable. Check its child card is invalid.");
        }
        #endregion


        #region Private methods 
        private List<AbstractCardContainer> SpawnTheFollowingAmountOfAbstractCardContainers( int _amount) {
            GameObject spiderCardContaierPrefabInstance = GameObject.Instantiate(
                            AssetDatabase.LoadAssetAtPath<GameObject>(SPIDERCARDCONTAINERM_PREFAB_PATH));
            List<AbstractCardContainer> listOfCardContainersToAdd = new List<AbstractCardContainer>();

            for (int i = 0; i < _amount; i++) {
                listOfCardContainersToAdd.Add(GameObject.Instantiate(spiderCardContaierPrefabInstance)
                                                                .GetComponent<AbstractCardContainer>());
            }

            return listOfCardContainersToAdd;
        }

        private List<CardFacade> SpawnTheFollowingamountOfCards( int _amountOfCards ) {
            GameObject cardFacadePrefabGameObject = GameObject.Instantiate(AssetDatabase
                                                        .LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH));
            CardFacade cardFacadePrefab = cardFacadePrefabGameObject.GetComponent<CardFacade>();
            List<CardFacade> listOfSpawnedCards = new List<CardFacade>();
            for (int i = 0; i < _amountOfCards; i++) {
                listOfSpawnedCards.Add(GameObject.Instantiate(cardFacadePrefabGameObject)
                                                                .GetComponent<CardFacade>());
            }

            return listOfSpawnedCards;
        }        

        private CardFacade SpawnNewCard() {
            GameObject cardFacadePrefabGameObject = GameObject.Instantiate(AssetDatabase
                                                        .LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH));
            CardFacade cardFacadePrefab = cardFacadePrefabGameObject.GetComponent<CardFacade>();
            
            return GameObject.Instantiate(cardFacadePrefabGameObject ).GetComponent<CardFacade>();
        }
        #endregion
    }
}

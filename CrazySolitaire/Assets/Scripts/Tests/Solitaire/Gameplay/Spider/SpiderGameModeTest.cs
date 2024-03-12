/*
* Author:	Iris Bermudez
* Date:		08/03/2024
*/



using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Solitaire.Gameplay;
using Solitaire.Gameplay.Spider;
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


        [TestCase(12, "HEARTS", "RED")]
        [TestCase(1, "SPADES", "BLACK")]
        [TestCase(7, "DIAMONDS", "RED")]
        [TestCase(4, "CLOVER", "BLACK")]
        [Test]
        public void WhenCardIsFacingUpWithValidChild_ThenActivateDragging(
                        short _cardNumber, string _cardSuit, string _cardColor ) {
            // Create card to validate
            CardFacade cardToValidate = SpawnNewCard();
            cardToValidate.SetCardData(new CardData(_cardNumber, _cardSuit, _cardColor, "PARENT"));

            // Create calid child card
            CardFacade childCard = SpawnNewCard();
            childCard.SetCardData(new CardData((short)(_cardNumber - 1), _cardSuit, _cardColor,
                                                                                        "CHILD"));

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


        [TestCase(6, "SPADES", "BLACK", "CLOVER", "BLACK" )]
        [TestCase(8, "HEARTS", "RED", "DIAMONDS", "RED")]
        [TestCase(1, "CLOVER", "BLACK", "HEARTS", "RED")]
        [TestCase(13, "DIAMONDS", "RED", "SPADES", "BLACK")]
        [Test]
        public void WhenCardIsFacingUpWithAChildOfAnotherSuit_ThenDeactivateDragging(
                                    short _parentCardNumber, string _parentCardSuit, string _parentCardColor,
                                    string _childCardSuit, string _childCardColor ) {
            // Create card to validate
            CardFacade cardToValidate = SpawnNewCard();
            cardToValidate.SetCardData(new CardData(_parentCardNumber, _parentCardSuit, _parentCardColor, "PARENT"));

            // Create calid child card
            CardFacade childCard = SpawnNewCard();
            childCard.SetCardData(new CardData((short)(_parentCardNumber -1), _childCardSuit, _childCardColor, "CHILD"));

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

        
        [TestCase( 1, 2, "CLOVER", "BLACK" ) ]
        [TestCase(12, 12, "HEARTS", "RED")]
        [TestCase(5, 3, "SPADES", "BLACK")]
        [Test]
        public void WhenCardIsFacingUpWithAChildWithAnInvalidNumber_ThenDeactivateDragging(
                                    short _parentCardNumber, short _childCardNumber, string _cardSuit,
                                    string _cardColor ) {
            // Create card to validate
            CardFacade cardToValidate = SpawnNewCard();
            cardToValidate.SetCardData(new CardData(_parentCardNumber, _cardSuit, _cardColor, "PARENT"));

            // Create calid child card
            CardFacade childCard = SpawnNewCard();
            childCard.SetCardData(new CardData(_childCardNumber, _cardSuit, _cardColor, "CHILD"));

            // Set prent facing up and child card
            cardToValidate.FlipCard(true);
            cardToValidate.SetChildCard(childCard);

            // Set card can bedragging to false
            cardToValidate.SetCanBeDragged(true);

            // Validate card dragging
            spiderGameModeMock.ValidateCardDragging(cardToValidate);

            // Assert cannot be dragged
            Assert.IsFalse( cardToValidate.gameObject.GetComponent<CardPhysics>().CanBeDragged,
                            "card should not be draggable. Check its child card is invalid.");
        }


        [TestCase(12)]
        [TestCase(2)]
        [TestCase(5)]
        [Test]
        public void WhenDistributesCardsBetweenCardContainers_ThenAddOneCardToEachContainer(
                                    int _amountOfCardsAndContainersToSpawn ) {
            // Create list of cards to distribute
            List<CardFacade> cardsToDistribute = SpawnTheFollowingamountOfCards( _amountOfCardsAndContainersToSpawn );

            // Create card container for distribution
            AbstractCardContainer cardContainerForDistribution = SpawnTheFollowingAmountOfAbstractCardContainers(1)[0];

            // Add cards to card container
            cardContainerForDistribution.AddCards(cardsToDistribute);

            // Create card containers for SpiderGameModeMock
            List<AbstractCardContainer> cardContainersForSpiderGameModeMock
                            = SpawnTheFollowingAmountOfAbstractCardContainers( _amountOfCardsAndContainersToSpawn );

            // Add card containers to SpiderGameModeMock
            spiderGameModeMock.SetCardContainers(cardContainersForSpiderGameModeMock);

            // Check amount of cards per container before distribution
            List<int> amountOfCardsPerContainer = new List<int>();
            foreach( AbstractCardContainer auxContainer in cardContainersForSpiderGameModeMock) {
                amountOfCardsPerContainer.Add(auxContainer.GetCards().Count);
            }

            // Distribute cards
            spiderGameModeMock.DistributeCardsBetweenCardContainers(cardContainerForDistribution);

            // Check amount of cards per container after distribution
            for( int i = 0; i < cardContainersForSpiderGameModeMock.Count; i++ ) {
                Assert.AreEqual( amountOfCardsPerContainer[i] + 1,
                                cardContainersForSpiderGameModeMock[i].GetCards().Count,
                                "At least one of the containers should have doesn't have the correct "
                                    + $"amount of cards. (Itshould have {amountOfCardsPerContainer[i] + 1} "
                                    + $"instead of {cardContainersForSpiderGameModeMock[i].GetCards().Count}.");

                Assert.IsTrue(cardsToDistribute[i].IsFacingUp(),
                                "Card should be facing up." );
            }
        }


        [Test]
        public void WhenReceivesNullObjectForCardDistribution_ThenThrowNullReferenceException() {
            int amountOfCardContainers = 3;

            // Create card containers for SpiderGameModeMock
            List<AbstractCardContainer> cardContainersForSpiderGameModeMock
                            = SpawnTheFollowingAmountOfAbstractCardContainers(amountOfCardContainers);

            // Add card containers to SpiderGameModeMock
            spiderGameModeMock.SetCardContainers( cardContainersForSpiderGameModeMock );

            // Distribute cards
            Assert.Throws<NullReferenceException>(() =>
                            spiderGameModeMock.DistributeCardsBetweenCardContainers(null),
                            "Check the passed object was null.");
        }


        [Test]
        public void WhenReceivesCardContainerForCardDistributionWithANullElementInCardList_ThenThrowNullReferenceExceptionWithoutChangingContainersCardsAmount() {
            int amountOfCardsToSpawn = 5;

            // Create list of cards
            List<CardFacade> cardsForDistribution = SpawnTheFollowingamountOfCards(amountOfCardsToSpawn);

            // Randomly add null element to list of cards
            cardsForDistribution[UnityEngine.Random.Range(0, cardsForDistribution.Count - 1)] = null;

            // Create CardContainer for distribution
            GameObject cardContainerforDistributionGameObject = GameObject.Instantiate(new GameObject());
            CardContainerMock cardContaineForDistribution = cardContainerforDistributionGameObject
                                                                    .AddComponent<CardContainerMock>();

            // Add list of cards to card container for distribution
            cardContaineForDistribution.AddCards(cardsForDistribution);

            // Create list of card containers for SpiderGameMode
            List<AbstractCardContainer> cardContainersForSpiderGameModeMock =
                                        SpawnTheFollowingAmountOfAbstractCardContainers(amountOfCardsToSpawn);

            // Add list of card containers to SpiderGameMode
            spiderGameModeMock.SetCardContainers(cardContainersForSpiderGameModeMock);

            // Save amount of cards before card distribution
            List<int> amountOfCardsPerContainerBeforeDistributionAssertion = new List<int>();
            foreach( AbstractCardContainer auxCardContainer in cardContainersForSpiderGameModeMock ) {
                amountOfCardsPerContainerBeforeDistributionAssertion.Add( auxCardContainer.GetCards().Count );
            }

            // Assert card distribution exception
            Assert.Throws<NullReferenceException>( () => spiderGameModeMock.DistributeCardsBetweenCardContainers(
                                                        cardContaineForDistribution),
                                                "Check cardsForDistribution List containes a null element.");

            // Assert containers amount of cards didn't change
            for( int i = 0; i < amountOfCardsPerContainerBeforeDistributionAssertion.Count; i++ ) {
                Assert.AreEqual( amountOfCardsPerContainerBeforeDistributionAssertion[i],
                                cardContainersForSpiderGameModeMock[i].GetCards().Count,
                                "The amount of cards changed in one of the containers." );
            }
        }
        #endregion


        #region Private methods 
        private List<AbstractCardContainer> SpawnTheFollowingAmountOfAbstractCardContainers( int _amount ) {
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

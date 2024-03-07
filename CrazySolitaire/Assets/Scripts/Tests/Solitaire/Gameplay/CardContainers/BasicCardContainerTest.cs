/*
* Author:	Iris Bermudez
* Date:		05/03/2024
*/



using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using UnityEngine;
using UnityEngine.TestTools;



namespace Tests.Solitaire.Gameplay.CardContainers {
    public class BasicCardContainerTest {
        #region Variables
        private GameObject basicCardContainerGameObject;
        private BasicCardContainer basicCardContainer;
        #endregion

        #region Tests setup
        [SetUp]
        public void Setup() {
            basicCardContainerGameObject = GameObject.Instantiate(new GameObject());
            basicCardContainer = basicCardContainerGameObject.AddComponent<BasicCardContainer>();
        }
        #endregion


        #region Tests
        [Test]
        public void BasicCardContainer_IsAbstractCardContainer () {
            Assert.IsInstanceOf( typeof(AbstractCardContainer), basicCardContainer );
        }

                
        [Test]
        public void WhenAddingCard_ThenGetCorrectAmoutOfCardsFromCardContainer() {
            //  Instantiate cards
            int amountOfCardsToSpawn = Random.Range(0, 100);
            GameObject cardGameObject = GameObject.Instantiate( new GameObject() );

            // Check to avoid false positive
            Assert.Zero ( basicCardContainer.GetCards().Count,
                            "basicCardContainer shouldn't contain any cards");

            //  Add cards to BasicCardContainer
            for( int i = 0; i < amountOfCardsToSpawn; i++ ) {
                basicCardContainer.AddCard( cardGameObject.AddComponent<CardFacade>() );
            }

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.True(  amountOfCardsToSpawn == basicCardContainer.GetCards().Count,
                            $"basicCardContainer cards has {basicCardContainer.GetCards().Count} "
                            + $"when it should have {amountOfCardsToSpawn} cards" );
        }


        [Test]
        public void WhenAddingNullObjectToBasicCardContainer_ThenThrowNullReferenceExceptionAndDontAddIt() {
            // Test to avoid false positive
            Assert.Zero( basicCardContainer.GetCards().Count,
                        "There was an alement in basicCardContainer before the null object was added.");
            Assert.Throws<System.NullReferenceException>( () => basicCardContainer.AddCard( null ) );
            Assert.Zero( basicCardContainer.GetCards().Count,
                        "Null object was added when it shouldn't.");
        }
        
        
        [Test]
        public void WhenAddingMultipleCards_ThenGetCorrectAmoutOfCardsFromCardContainer() {
            // Instantiate cards
            int amountOfCardsToSpawn = Random.Range(0, 100);
            GameObject cardGameObject = GameObject.Instantiate(new GameObject());
            List<CardFacade> cardsToAdd = new List<CardFacade>();
            for (int i = 0; i < amountOfCardsToSpawn; i++) {
                cardsToAdd.Add( cardGameObject.AddComponent<CardFacade>() );
            }

            // Check to avoid false positive
            Assert.Zero( basicCardContainer.GetCards().Count,
                            "basicCardContainer shouldn't contain any cards");

            // Add cards to BasicCardContainer
            basicCardContainer.AddCards( cardsToAdd );

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.True( amountOfCardsToSpawn == basicCardContainer.GetCards().Count,
                            $"basicCardContainer cards has {basicCardContainer.GetCards().Count} "
                            + $"when it should have {amountOfCardsToSpawn} cards" );
        }


        [Test]
        public void WhenAddingCardListWithNullObjectToBasicCardContainer_ThenThrowNullReferenceExceptionAndDontAddIt () {
            //  Create list of cards
            GameObject cardsGameObject = GameObject.Instantiate(new GameObject());
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>() {
                                                cardsGameObject.AddComponent<CardFacade>(),
                                                cardsGameObject.AddComponent<CardFacade>(),
                                                null,
                                                cardsGameObject.AddComponent<CardFacade>()
                                            };

            //  Check basicCardcontainer has 0 cards
            Assert.Zero( basicCardContainer.GetCards().Count,
                            "basicCardContainer shouldn't contain any cards but it does." );

            //  Assert list contains null object
            Assert.True( listOfCardsToAdd.Contains(null),
                        "listOfCardsToAdd should contain a null element, but it does not.");

            //  Assert mutltiple card addition error
            Assert.Throws<System.NullReferenceException>(
                                                    () => basicCardContainer.AddCards(listOfCardsToAdd));

            //  Check no card was added
            Assert.Zero( basicCardContainer.GetCards().Count,
                            "At least one card has been added to basicCardContainer when it shouldn't.");
        }
        

        [Test]
        public void WhenInitializing_ThenAddOnlyTheRightAmountOfCards() {
            // Create list of cards to add
            short defaultAmountOfCards = (short) Random.Range(1, 50);
            int amountOfCardsToAdd = Random.Range(defaultAmountOfCards, 50);
            basicCardContainer.SetDefaultAmountOfCards(defaultAmountOfCards);
            GameObject cardsGameObject = GameObject.Instantiate( new GameObject() );
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>();
            for( int i = 0; i < amountOfCardsToAdd; i++ ) {
                listOfCardsToAdd.Add( cardsGameObject.AddComponent<CardFacade>() );
            }


            // Check there aren't any cards already to avoid false positive
            Assert.Zero( basicCardContainer.GetCards().Count,
                        "basicCardContainer shouldn't contain any cards but it does.");

            // Initialize basicCardContainer
            List<CardFacade> remainingCards = basicCardContainer.Initialize(listOfCardsToAdd);

            // Check cards have been added successfully
            Assert.AreEqual( defaultAmountOfCards, basicCardContainer.GetCards().Count,
                            $"basicCardContainer should contain {amountOfCardsToAdd} "
                                    + $"but it has {basicCardContainer.GetCards().Count} instead");
            Assert.AreEqual( (amountOfCardsToAdd - basicCardContainer.GetCards().Count),
                                remainingCards.Count,
                                $"The remaining cards should be " +
                                $"{amountOfCardsToAdd - basicCardContainer.GetCards().Count} "
                                + $"but there are {remainingCards.Count} instead.");

        }

        [Test]
        public void WhenTriesToInitializeAListWithANullElement_ThenThorwNullReferenceExceptionBeforeAddingAnyCard() {
            // Create list of cards
            GameObject cardsGameObject = GameObject.Instantiate( new GameObject() );
            List<CardFacade> listForInitialization = new List<CardFacade>() {
                                                        cardsGameObject.AddComponent<CardFacade>(),
                                                        null,
                                                        cardsGameObject.AddComponent<CardFacade>()
                                                    };
            int amountOfCardsToAddForInitialization = listForInitialization.Count;

            // Check basic card container doesn't have any cards to avoid false positive
            Assert.Zero( basicCardContainer.GetCards().Count,
                        "casicCardContainer shouldn't contain any card but it does.");

            // Assert initialization
            Assert.Throws<System.NullReferenceException>( () => 
                                                    basicCardContainer.Initialize(listForInitialization),
                                                    "basicCardContainer should have thrown a "
                                                    + "NullReferenceException error since at least one of "
                                                    + "the elements of the list is null.");

            // Check no card was added
            Assert.Zero( basicCardContainer.GetCards().Count,
                        "Cards have been added to basicCardContainer when they shouldn't.");
            Assert.AreEqual( amountOfCardsToAddForInitialization,
                            listForInitialization.Count,
                            "listForInitialization amount of elements should be "
                            + $"{amountOfCardsToAddForInitialization} but it's "
                            + $"{listForInitialization.Count} instead."
                        );
        }
                
        [Test]
        public void WhenCallingRemoveCardMethod_ThenEliminateItFromBasicCardContainerCards() {
            //  Instantiate cards to add
            GameObject cardsGameObject = GameObject.Instantiate(new GameObject());
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>() {
                                                        cardsGameObject.AddComponent<CardFacade>(),
                                                        cardsGameObject.AddComponent<CardFacade>(),
                                                        cardsGameObject.AddComponent<CardFacade>(),
                                                        cardsGameObject.AddComponent<CardFacade>()
                                                    };

            //  Check BasicCardContainer doesn't have cards already
            Assert.Zero(basicCardContainer.GetCards().Count,
                        "casicCardContainer shouldn't contain any card but it does.");

            //  Add cards  to BasicCardContainer
            basicCardContainer.AddCards(listOfCardsToAdd);

            //  Remove card
            int indexOfTheCardToBeRemoved = Random.Range(0, listOfCardsToAdd.Count - 1);
            basicCardContainer.RemoveCard(listOfCardsToAdd[indexOfTheCardToBeRemoved]);

            //  Assert container doesn't have that card anymore and that the amount of cards is correct
            Assert.False( basicCardContainer.GetCards().Contains(listOfCardsToAdd[indexOfTheCardToBeRemoved]),
                        "basicCardContainer still  has the card that should have been removed.");
            Assert.AreEqual( listOfCardsToAdd.Count - 1,
                            basicCardContainer.GetCards().Count,
                            "The amount of cards in basicCardContainer should be "
                            + $"{listOfCardsToAdd.Count - 1} instead of {basicCardContainer.GetCards().Count} ");
        }

        [Test]
        public void WhenPassingNullToRemoveCardMethod_ThenThrowNullReferenceException() {
            // Assert null reference
            Assert.Throws<System.NullReferenceException>( () => basicCardContainer.RemoveCard(null) );
        }

        [Test]
        public void WhenCallMethodForMultipleCardRemoval_ThenRemoveOnlyTheRightAmountOfCards() {
            //  Instantiate cards to add
            GameObject cardsObject = GameObject.Instantiate( new GameObject() );
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>() {
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>()
            };

            //  Add cards to containner
            basicCardContainer.AddCards( listOfCardsToAdd );

            //  Save amount of cards
            int amountOfCardsAfterAddition = basicCardContainer.GetCards().Count;

            //  Create list of cards to remove
            List<CardFacade> listOfCardsToRemove = new List<CardFacade>() {
                listOfCardsToAdd[1],
                listOfCardsToAdd[3],
                listOfCardsToAdd[5]
            };

            //  Remove multiple cards
            basicCardContainer.RemoveCards(listOfCardsToRemove);

            //  Assert the remaining amount of cards in container is correct
            Assert.AreEqual(amountOfCardsAfterAddition - listOfCardsToRemove.Count,
                            basicCardContainer.GetCards().Count,
                            $"The container has {basicCardContainer.GetCards().Count} "
                                    + "when it was expected it to have "
                                    + $"{amountOfCardsAfterAddition - listOfCardsToRemove.Count}.");

            //  Assert cards for removal are not referenced by container
            foreach(var auxCard in listOfCardsToRemove ) {
                Assert.False( basicCardContainer.GetCards().Contains( auxCard ),
                            "At least one of the cards from listOfCardsToRemove "
                                    + "wasn't successfully removed."
                    );
            }
        }

        [Test]
        public void WhenPassingListWithNullElementToRemoveCardMethod_ThenThrowNullReferenceException () {
            //  Create list of cards
            GameObject cardsObject = GameObject.Instantiate(new GameObject());
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>() {
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>(),
                    cardsObject.AddComponent<CardFacade>()
            };

            //  Add cards to container
            basicCardContainer.AddCards(listOfCardsToAdd);

            //  Create list of cards to remove with a null element in it
            List<CardFacade> listOfCardsToRemove = new List<CardFacade>() {
                listOfCardsToAdd[1],
                null,
                listOfCardsToAdd[5]
            };

            //  Assert remove method
            Assert.Throws<System.NullReferenceException>(
                                () => basicCardContainer.RemoveCards(listOfCardsToRemove)
                            );

        }
        #endregion
    }
}

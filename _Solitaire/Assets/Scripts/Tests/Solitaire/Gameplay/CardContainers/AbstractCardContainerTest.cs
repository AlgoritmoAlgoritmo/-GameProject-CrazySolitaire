/*
* Author:	Iris Bermudez
* Date:		29/02/2024
*/



using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using System.Collections.Generic;



namespace Tests.Solitaire.Gameplay.CardContainers {
    public class AbstractCardContainerTest {
        #region Variables
        private GameObject abstractCardContainerObject;
        private AbstractCardContainerMock abstractCardContainerMock;
        #endregion


        #region Tests set up
        [SetUp]
        public void Setup() {
            abstractCardContainerObject = GameObject.Instantiate(new GameObject());
            abstractCardContainerMock = abstractCardContainerObject.AddComponent<AbstractCardContainerMock>();
        }
        #endregion


        #region Tests
        [Test]
        public void AbstractCardContainerMock_IsAbstractCardContainer() {
            Assert.IsNotNull(abstractCardContainerObject, "abstractCardContainer is Null");
            Assert.IsNotNull(abstractCardContainerMock, "abstractCardContainerMock is Null");
            Assert.IsInstanceOf(typeof(AbstractCardContainer), abstractCardContainerMock,
                    "AbstractCardContainerMock does not inherit from AbstractCardContainer.");
        }

        
        [TestCase( 2, 5 )]
        [TestCase( 4, 7 )]
        [TestCase( 3, 3 )]
        [TestCase( 10, 11 )]
        [Test]
        public void WhenInitialize_ThenReturnNoAddedCards( int _initialCardsAmount,
                                                            int _amountOfCardsToInstantiate) {
            // Instantiate cards
            GameObject cardGameObject = GameObject.Instantiate(new GameObject());
            List<CardFacade> cardsList = new List<CardFacade>();
            for (int i = 0; i <= _amountOfCardsToInstantiate; i++) {
                cardsList.Add(cardGameObject.AddComponent<CardFacade>());
            }

            // Set up
            abstractCardContainerMock.SetInitialCardsAmount((short)_initialCardsAmount);

            // Check there aren't previous cards
            Assert.IsTrue(abstractCardContainerMock.GetCards().Count < 1);

            // Pass array of cards for AbstractCardContainerInitialization
            cardsList = abstractCardContainerMock.Initialize(cardsList);

            // Assertions
            Assert.IsTrue(abstractCardContainerMock.GetCards().Count == _initialCardsAmount);
        }


        [Test]
        public void WhenCheckingIfContainsACard_ThenReturnsIfContainsItOrNot() {
            // Initializing
            GameObject cardContainerObject = GameObject.Instantiate(new GameObject());
            CardFacade card0 = cardContainerObject.AddComponent<CardFacade>();
            CardFacade card1 = cardContainerObject.AddComponent<CardFacade>();

            // Test against false positive
            Assert.IsTrue(abstractCardContainerMock.GetCards().Count == 0,
                            "abstractCardContainerMock shouldn't containe any card.");

            // Add card
            abstractCardContainerMock.AddCard(card0);

            // Do actual test
            Assert.IsTrue( abstractCardContainerMock.ContainsCard(card0),
                            "card0 wasn't added to abstractCardContainerMock");
            Assert.IsFalse( abstractCardContainerMock.ContainsCard(card1),
                            "card1 wasn't added to abstractCardContainerMock");
        }


        [Test]
        public void WhenGettingTopCard_ThenReturnLastAddedCard() {
            // Initialization
            GameObject cardContainerObject = GameObject.Instantiate(new GameObject());
            CardFacade card0 = cardContainerObject.AddComponent<CardFacade>();
            CardFacade card1 = cardContainerObject.AddComponent<CardFacade>();

            // Add cards
            abstractCardContainerMock.AddCard(card0);
            abstractCardContainerMock.AddCard(card1);

            // Check last card is last added card
            Assert.AreSame(card1, abstractCardContainerMock.GetTopCard());
        }


        [TestCase(0f, 0f, 0f)]
        [TestCase(-10f, 5f, 3f)]
        [TestCase(200f, -80f, -67f )]
        [Test]
        public void WhenRefreshingAbstractCardContainer_ThenChangeCardPositionToCorresponding(
                                                        float _xOffset, float _yOffset, float _zOffset ) {
            // Set up
            abstractCardContainerObject.transform.position = new Vector3(   Random.Range(-100, 100),
                                                                            Random.Range(-100, 100),
                                                                            Random.Range(-100, 100) );
            Vector3 expectedPosition = abstractCardContainerObject.transform.position;
            expectedPosition.z = 0;  // Must always be 0
            GameObject cardContainerObject = GameObject.Instantiate(new GameObject());
            CardFacade card0 = cardContainerObject.AddComponent<CardFacade>();

            abstractCardContainerMock.SetOffset( new Vector3( _xOffset, _yOffset, _zOffset ));
            abstractCardContainerMock.AddCard(card0);
            cardContainerObject.transform.position = abstractCardContainerMock
                                                                        .GetCardPosition_MockPublicAccess(0)
                                                    + new Vector3(1, 1, 1);

            // Check position to avoid false positive
            Assert.AreNotEqual(expectedPosition, cardContainerObject.transform.position);

            // Call refresh function
            abstractCardContainerMock.Refresh();

            // Assert position
            Assert.AreEqual(expectedPosition, cardContainerObject.transform.position);
        }
        #endregion
    }
}

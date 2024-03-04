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
        [UnityTest]
        public IEnumerator AbstractCardContainerMock_IsAbstractCardContainer() {
            yield return null;
            yield return null;

            Assert.IsNotNull( abstractCardContainerObject, "abstractCardContainer is Null");
            Assert.IsNotNull( abstractCardContainerMock, "abstractCardContainerMock is Null");
            Assert.IsInstanceOf( typeof(AbstractCardContainer), abstractCardContainerMock,
                    "AbstractCardContainerMock does not inherit from AbstractCardContainer.");
        }


        [UnityTest]
        public IEnumerator WhenCheckingIfContainsACard_ReturnsIfContainsItOrNot() {
            // Initializing
            GameObject cardContainerObject = GameObject.Instantiate( new GameObject() );
            CardFacade card0 = cardContainerObject.AddComponent<CardFacade>();
            CardFacade card1 = cardContainerObject.AddComponent<CardFacade>();

            // Test against false positive
            Assert.IsTrue( abstractCardContainerMock.GetCards().Count == 0,
                            "abstractCardContainerMock shouldn't containe any card.");

            // Add card
            abstractCardContainerMock.AddCard( card0 );

            // Do actual test
            Assert.IsTrue( abstractCardContainerMock.ContainsCard( card0 ),
                            "card0 wasn't added to abstractCardContainerMock" );
            Assert.IsFalse( abstractCardContainerMock.ContainsCard( card1 ),
                            "card1 wasn't added to abstractCardContainerMock" );

            yield return null;
        }


        [UnityTest]
        public IEnumerator WhenGettingTopCard_ReturnLastAddedCard() {
            // Initialization
            GameObject cardContainerObject = GameObject.Instantiate(new GameObject());
            CardFacade card0 = cardContainerObject.AddComponent<CardFacade>();
            CardFacade card1 = cardContainerObject.AddComponent<CardFacade>();

            // Add cards
            abstractCardContainerMock.AddCard( card0 );
            abstractCardContainerMock.AddCard( card1 );

            // Check last card is last added card
            Assert.AreSame( card1, abstractCardContainerMock.GetTopCard() );

            yield return null;
        }
        #endregion


        #region Private methods

        #endregion
    }
}

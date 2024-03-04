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
        public IEnumerator WhenCheckingIfContainsACard_ThenReturnsIfContainsItOrNot() {
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
        public IEnumerator WhenGettingTopCard_ThenReturnLastAddedCard() {
            // Initialization
            GameObject cardContainerObject = GameObject.Instantiate( new GameObject() );
            CardFacade card0 = cardContainerObject.AddComponent<CardFacade>();
            CardFacade card1 = cardContainerObject.AddComponent<CardFacade>();

            // Add cards
            abstractCardContainerMock.AddCard( card0 );
            abstractCardContainerMock.AddCard( card1 );

            // Check last card is last added card
            Assert.AreSame( card1, abstractCardContainerMock.GetTopCard() );

            yield return null;
        }


 
        static Vector3[] offsetPosition = { Vector3.zero,
                                            new Vector3(-10, 5, 3),
                                            new Vector3( 200, -80, -67 ) };
        [UnityTest]
        public IEnumerator WhenRefreshingAbstractCardContainer_ThenChangeCardPositionToCorresponding(
                                            [ValueSource("offsetPosition")] Vector3 _offset ) {
            // Set up
            abstractCardContainerObject.transform.position = new Vector3( Random.Range( -100, 100 ),
                                                                            Random.Range(-100, 100),
                                                                            Random.Range(-100, 100)
                                                                        );
            // Setup
            Vector3 expectedPosition = abstractCardContainerObject.transform.position;
            expectedPosition.z = 0;  // Must always be 0
            GameObject cardContainerObject = GameObject.Instantiate( new GameObject() );
            CardFacade card0 = cardContainerObject.AddComponent<CardFacade>();
            
            abstractCardContainerMock.SetOffset( _offset );
            abstractCardContainerMock.AddCard( card0 );
            cardContainerObject.transform.position = abstractCardContainerMock
                                                                        .GetCardPosition_MockPublicAccess(0)
                                                    + new Vector3( 1, 1, 1 );

            // Check position to avoid false positive
            Assert.AreNotEqual( expectedPosition, cardContainerObject.transform.position );

            // Call refresh function
            abstractCardContainerMock.Refresh();

            // Assert position
            Assert.AreEqual( expectedPosition, cardContainerObject.transform.position );

            yield return null;
        }
        #endregion
    }
}

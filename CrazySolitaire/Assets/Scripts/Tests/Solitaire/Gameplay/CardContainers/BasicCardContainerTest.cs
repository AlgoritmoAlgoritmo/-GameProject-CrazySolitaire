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
        #endregion
    }
}

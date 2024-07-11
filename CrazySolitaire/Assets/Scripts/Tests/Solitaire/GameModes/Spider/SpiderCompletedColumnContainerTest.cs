/*
* Author:	Iris Bermudez
* Date:		10/07/2024
*/


  
using NUnit.Framework;
using UnityEngine;
using Solitaire.GameModes.Spider;
using Solitaire.Gameplay.Cards;
using System.Collections.Generic;
using UnityEditor;

namespace Tests.Solitaire.GameModes.Spider {
    public class SpiderCompletedColumnContainerTest {
        #region Variables
        private GameObject spiderCompletedColumnContainerGameObject;
        private SpiderCompletedColumnContainerMock spiderCompletedColumnContainerMock;

        private const string CARD_PREFAB_PATH = Test.TestConstants.CARD_PREFAB_PATH;
        #endregion


        #region Tests setup
        [SetUp]
        public void Setup() {
            spiderCompletedColumnContainerGameObject = GameObject.Instantiate(
                                                                new GameObject() );
            if( !spiderCompletedColumnContainerGameObject ) {
                throw new System.NullReferenceException( "spiderCompletedColumnContainer" 
                                            + "GameObject could not be instantiated." );
            }

            spiderCompletedColumnContainerMock = spiderCompletedColumnContainerGameObject
                                        .AddComponent<SpiderCompletedColumnContainerMock>();
            if( !spiderCompletedColumnContainerMock ) {
                throw new System.NullReferenceException( "Couldn't add "
                                        + "SpiderCompletedColumnContainerMock component to "
                                        + "spiderCompletedColumnContainerGameObject" );
            }
        }
        #endregion


        #region Tests
        [Test]
        public void SpiderCompletedColumnContainerMock_Is_SpiderCompletedColumnContainer() {
            Assert.IsInstanceOf( typeof( SpiderCompletedColumnContainer ),
                                spiderCompletedColumnContainerMock,
                                "Make sure spiderCompletedColumnContainerMock inherits "
                                                + "from SpiderCompletedColumnContainer" );
        }


        [Test]
        public void WhenAddingCardListWithNullElement_ThenThrowsNullReferenceException() {
            // Checking spiderCompletedColumnContainerMock doesn't contain any cards
            // to have a reference when checking if the amount of cards changed
            Assert.Zero( spiderCompletedColumnContainerMock.GetCardsAmount(),
                        "spiderCompletedColumnContainerMock shuldn't contain any card." );

            Assert.Throws<System.NullReferenceException>( 
                                    () => spiderCompletedColumnContainerMock.AddCards(
                                                    new List<CardFacade>() { null } ),
                                    "Check the addition of List<CardFacade> validates "
                                                                    + "null elements" );

            // Checking the amount of cards hasn't change to avoid adding null elements
            Assert.Zero( spiderCompletedColumnContainerMock.GetCardsAmount(),
                        "spiderCompletedColumnContainerMock shuldn't contain any card." );
        }


        [Test]
        public void WhenAddingNullCardList_ThenThrowsNullReferenceException() {
            // Checking spiderCompletedColumnContainerMock doesn't contain any cards
            // to have a reference when checking if the amount of cards changed
            Assert.Zero( spiderCompletedColumnContainerMock.GetCardsAmount(),
                        "spiderCompletedColumnContainerMock shuldn't contain any card." );

            Assert.Throws<System.NullReferenceException>(
                                () => spiderCompletedColumnContainerMock.AddCards( null ),
                                "Check the addition of List<CardFacade> validates null lists" );

            // Checking the amount of cards hasn't change to avoid adding null elements
            Assert.Zero( spiderCompletedColumnContainerMock.GetCardsAmount(),
                        "spiderCompletedColumnContainerMock shuldn't contain any card." );
        }


        [Test]
        [TestCase( 1 )]
        [TestCase( 3 )]
        [TestCase( 4 )]
        public void WhenAddingValidListOfCards_ThenIncreasesTheAmountOfContainedCardsCorrectly(
                                                                int _amountOfCardsToAdd ) {
            // Instantiate cards to add
            List<CardFacade> listOfCardsToAdd = new List<CardFacade>();
            for( int i = 0; i < _amountOfCardsToAdd; i++ ) {
                listOfCardsToAdd.Add( GetNewCardFacadeInstance() );            
            }

            // Assert there aren't any cards already
            Assert.Zero( spiderCompletedColumnContainerMock.GetCardsAmount(),
                            "spiderCompletedColumnContainerMock shouldn't contain any cards." );

            // Add valid list of cards
            spiderCompletedColumnContainerMock.AddCards( listOfCardsToAdd );

            // Assert the amount of cards increased correctly
            Assert.AreEqual( _amountOfCardsToAdd,
                            spiderCompletedColumnContainerMock.GetCardsAmount(),
                            "spiderCompletedColumnContainerMock should contain " 
                                            +$"{_amountOfCardsToAdd} cards." );

        }
        #endregion


        #region Private methods
        private CardFacade GetNewCardFacadeInstance() {
            return AssetDatabase.LoadAssetAtPath<GameObject>( CARD_PREFAB_PATH )
                                                        .GetComponent<CardFacade>();
        }
        #endregion
    }
}

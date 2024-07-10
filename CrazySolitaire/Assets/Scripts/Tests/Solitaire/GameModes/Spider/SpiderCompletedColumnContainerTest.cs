/*
* Author:	Iris Bermudez
* Date:		10/07/2024
*/


  
using NUnit.Framework;
using UnityEngine;
using Solitaire.GameModes.Spider;
using Solitaire.Gameplay.Cards;
using System.Collections.Generic;

namespace Tests.Solitaire.GameModes.Spider {
    public class SpiderCompletedColumnContainerTest {
        #region Variables
        private GameObject spiderCompletedColumnContainerGameObject;
        private SpiderCompletedColumnContainerMock spiderCompletedColumnContainerMock;
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
        public void WhenAddingCardListWithNullElemetnt_ThenThrowsNullReferenceException() {
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
        #endregion
    }
}

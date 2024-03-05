/*
* Author:	Iris Bermudez
* Date:		05/03/2024
*/



using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Solitaire.Gameplay.CardContainers;
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


            // Check to avoid false positive


            // 



        }

        #endregion
    }
}

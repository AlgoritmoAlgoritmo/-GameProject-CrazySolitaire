/*
* Author:	Iris Bermudez
* Date:		07/03/2024
*/



using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Solitaire.Gameplay.Spider;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using UnityEditor;



namespace Tests.Solitaire.Gameplay.Spider {
    public class SpiderCardContainerTest {
        #region Variables
        private GameObject spiderCardContainerGameObject;
        private SpiderCardContainer spiderCardContainer;

        private const string CARD_PREFAB_PATH = "Assets/Prefabs/Gameplay/Card Prefab.prefab";
        private const string SPIDERCARDCONTAINER_PREFAB_PATH = "Assets/Prefabs/Gameplay/Spider"
                                                + "/SpiderCardContainer 6Cards.prefab";
        #endregion


        #region Setup
        [SetUp]
        public void Setup() {
            spiderCardContainerGameObject = GameObject.Instantiate( AssetDatabase
                                                        .LoadAssetAtPath<GameObject>(
                                                            SPIDERCARDCONTAINER_PREFAB_PATH
                                                    ));

            if( !spiderCardContainerGameObject ) {
                throw new NullReferenceException("GameObject at SPIDERCARDCONTAINER_PREFAB_PATH "
                        + "could not be loaded.");
            }

            spiderCardContainer = spiderCardContainerGameObject.AddComponent<SpiderCardContainer>();

            if( !spiderCardContainer ) {
                throw new NullReferenceException("GameObject at SPIDERCARDCONTAINER_PREFAB_PATH "
                        + "does not contain a SpiderCardContainer component.");
            }
        }
        #endregion


        #region Tests
        [Test]
        public void SpiderCardContainer_IsAbstractCardContainer() {
            Assert.IsInstanceOf(typeof(AbstractCardContainer), spiderCardContainer,
                        "SpiderCardContainer does not inherit from AbstractCardContainer.");
        }


        [Test]
        public void WhenAddingCard_ThenGetCorrectAmoutOfCardsFromCardContainer() {
            //  Generate random amount of cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range(0, 50);
            GameObject cardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(CARD_PREFAB_PATH);

            // Assert CardPrefab was loaded successfully
            Assert.IsNotNull(cardPrefab, "Card prefab could not be loaded.");

            // Check to avoid false positive
            Assert.Zero(spiderCardContainer.GetCards().Count,
                            "spiderCardContainer shouldn't contain any cards");

            //  Add cards to spiderCardContainer
            for (int i = 0; i < amountOfCardsToSpawn; i++) {
                spiderCardContainer.AddCard( GameObject.Instantiate(cardPrefab)
                                                .GetComponent<CardFacade>()
                                            );
            }

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.True(amountOfCardsToSpawn == spiderCardContainer.GetCards().Count,
                            $"spiderCardContainer cards has {spiderCardContainer.GetCards().Count} "
                            + $"when it should have {amountOfCardsToSpawn} cards");
        }
        #endregion
    }
}

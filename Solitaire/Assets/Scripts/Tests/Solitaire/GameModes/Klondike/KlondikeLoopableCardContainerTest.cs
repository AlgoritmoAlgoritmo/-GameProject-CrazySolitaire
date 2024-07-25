/*
* Author:	Iris Bermudez
* Date:		24/07/2024
*/



using System;
using NUnit.Framework;
using UnityEngine;
using Solitaire.GameModes.Klondike;
using UnityEditor;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using System.Collections.Generic;

namespace Tests.Solitaire.GameModes.Klondike {
    public class KlondikeLoopableCardContainerTest {
        #region Variables
        private GameObject klondikeLoopableCardGameObject;
        private KlondikeLoopableCardContainer klondikeLoopableCardContainer;
        #endregion


        #region Setup
        [SetUp]
        public void SetUp() {
            klondikeLoopableCardGameObject = GameObject.Instantiate( AssetDatabase
                                                        .LoadAssetAtPath<GameObject>(
                                                            Test.TestConstants
                                                                .KLONDIKELOOPABLECONTAINER_PREFAB_PATH ) );

            if( !klondikeLoopableCardGameObject ) {
                throw new NullReferenceException( "GameObject at "
                        + $"{Test.TestConstants.KLONDIKELOOPABLECONTAINER_PREFAB_PATH} "
                        + "could not be loaded." );
            }

            klondikeLoopableCardContainer = klondikeLoopableCardGameObject
                                                    .GetComponent<KlondikeLoopableCardContainer>();

            if( !klondikeLoopableCardContainer ) {
                throw new NullReferenceException( "GameObject at "
                        + $"{Test.TestConstants.KLONDIKELOOPABLECONTAINER_PREFAB_PATH} "
                        + "does not contain a SpiderCardContainer component." );
            }
        }
        #endregion


        #region Tests
        [Test]
        public void KlondikeLoopableCardContainer_IsAbstractCardContainer() {
            Assert.IsInstanceOf( typeof( AbstractCardContainer ), klondikeLoopableCardContainer,
                        "KlondikeLoopableCardContainer does not inherit from AbstractCardContainer." );
        }

        [Test]
        public void WhenAddingCard_ThenThrowNotImplementedException() {
            //  Generate random amount of cards
            int amountOfCardsToSpawn = UnityEngine.Random.Range( 0, 50 );
            var card = SpawnFollowingAmountOfCards(1)[0];


            // Assert CardPrefab was loaded successfully
            Assert.IsNotNull( card, "Card prefab could not be loaded." );

            // Check to avoid false positive
            Assert.Zero( klondikeLoopableCardContainer.GetCardCount(),
                            "klondikeLoopableCardContainer shouldn't contain any cards" );     

            // Assert the amount of cards added is the same the BasicaCardContainer's contain
            Assert.Throws<NotImplementedException>( () => klondikeLoopableCardContainer.AddCard( card ) );

            // Assert amount of cards is still 0
            Assert.Zero( klondikeLoopableCardContainer.GetCardCount(),
                            "klondikeLoopableCardContainer shouldn't contain any cards" );
        }


        #endregion


        #region Private methods
        private List<CardFacade> SpawnFollowingAmountOfCards( int _amount ) {
            GameObject cardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>( Test.TestConstants.CARD_PREFAB_PATH );

            if( !cardPrefab ) {
                throw new NullReferenceException( $"Couldn't load card prefab at {Test.TestConstants.CARD_PREFAB_PATH}" );
            }


            List<CardFacade> cardInstances = new List<CardFacade>();

            for( int i = 0; i <= _amount; i++ ) {
                cardInstances.Add( GameObject.Instantiate( cardPrefab ).GetComponent<CardFacade>() );
            }

            return cardInstances;
        }
        #endregion
    }
}

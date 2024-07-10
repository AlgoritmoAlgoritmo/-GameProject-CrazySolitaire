/*
* Author:	Iris Bermudez
* Date:		10/07/2024
*/


  
using NUnit.Framework;
using UnityEngine;
using Solitaire.GameModes.Spider;



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
        #endregion
    }
}

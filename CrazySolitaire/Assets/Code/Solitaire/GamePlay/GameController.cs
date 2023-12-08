﻿/*
* Author:	Iris Bermudez
* Date:		08/12/2023
*/



using UnityEngine;



namespace Solitaire.Gameplay {

    public class GameController : MonoBehaviour {
        #region Variables
        [SerializeField]
        private AbstractGameMode gameMode;
        #endregion


        #region MonoBehaviour methods
        private void Update() {
            if( Input.GetKeyUp( KeyCode.F1 ) ) {
                StartGame();
            }
        }
        #endregion


        #region Public methods
        public void EndClearedGame() {
            Debug.Log( "Game cleared." );
        }
        #endregion


        #region Private methods
        private void StartGame() {
            gameMode.SubscribeToOnGameClearedEvent( EndClearedGame );
            gameMode.Initialize();
        }
        #endregion
    }
}
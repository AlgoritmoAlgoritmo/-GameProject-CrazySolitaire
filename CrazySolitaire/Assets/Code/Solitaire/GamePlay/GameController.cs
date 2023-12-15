/*
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
        private void Start() {
            StartGame();
        }
        #endregion


        #region Public methods
        public void EndClearedGame( object _object, System.EventArgs _args ) {
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
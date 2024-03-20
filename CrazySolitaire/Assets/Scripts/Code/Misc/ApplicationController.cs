/*
* Author:	Iris Bermudez
* Date:		18/03/2024
*/



using UnityEngine;
using UnityEngine.SceneManagement;

namespace Misc {
    public class ApplicationController : MonoBehaviour {
        #region Public methods
        public void QuitApplication() {
            Application.Quit();
        }

        public void ToggleScreenMode() {
            if ( Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
                Screen.fullScreenMode = FullScreenMode.Windowed;
            else
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }

        public void RestartCurrentScene() {
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
        #endregion


        #region Private methods

        #endregion
    }
}
/*
* Author:	Iris Bermudez
* Date:		11/12/2023
*/



using System.Collections.Generic;
using UnityEngine;


namespace Solitaire.Common {
    [System.Serializable]
    public class BasicSuitData {
        #region Variables
        public string suitName;
        public string color;
        #endregion


        #region Constructors
        public BasicSuitData( string _suitName, string _color ) {
            suitName = _suitName;
            color = _color;
        }
        #endregion
    }
}
/*
* Author:	Iris Bermudez
* Date:		11/12/2023
*/



using System.Collections.Generic;
using UnityEngine;


namespace Solitaire.Common {
    [System.Serializable]
    public class SuitData {
        #region Variables
        public string suitName;
        public string color;
        public List<Sprite> sprites;
        #endregion

        #region Constructors
        public SuitData( string _suitName, string _color ) {
            suitName = _suitName;
            color = _color;
        }
        #endregion
    }
}
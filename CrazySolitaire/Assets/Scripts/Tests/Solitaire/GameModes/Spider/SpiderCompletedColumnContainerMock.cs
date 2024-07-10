/*
* Author:	Iris Bermudez
* Date:		10/07/2024
*/



using UnityEngine;
using Solitaire.GameModes.Spider;



namespace Tests.Solitaire.GameModes.Spider {
	public class SpiderCompletedColumnContainerMock : SpiderCompletedColumnContainer {
		#region Variables

		#endregion


		#region Public methods
		public int GetCardsAmount() {
			return cards.Capacity;
		}
		#endregion


		#region Private methods

		#endregion
	}
}
/*
* Author:	Iris Bermudez
* Date:		10/07/2024
*/



using Solitaire.GameModes.Spider;



namespace Tests.Solitaire.GameModes.Spider {
	public class SpiderCompletedColumnContainerMock : SpiderCompletedColumnContainer {
		#region Variables

		#endregion


		#region Public methods
		public int GetCardsAmount() {
			return cards.Count;
		}
		#endregion
	}
}
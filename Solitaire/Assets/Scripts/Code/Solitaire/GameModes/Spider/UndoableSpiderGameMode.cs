/*
* Author:	Iris Bermudez
* Date:		19/09/2024
*/



using System.Collections.Generic;
using Solitaire.GameModes.UndoableCommands;
using Solitaire.Gameplay.CardContainers;
using Solitaire.Gameplay.Cards;
using Solitaire.Gameplay.Common;



namespace Solitaire.GameModes.Spider {
	public class UndoableSpiderGameMode : SpiderGameMode {
		#region Variables
		private UndoLastPlayController undoController;
        #endregion


        #region MonoBehaviour methods
        private void Start() {
			undoController = new UndoLastPlayController();
        }
        #endregion


        #region Public methods
        public void MakePlay() {
			undoController.MakePlay();
		}

		public void Undo() {
			undoController.UndoPlay();
		}
		#endregion


		#region Private methods
		protected override void MoveCardToNewContainer( CardFacade _card,
												AbstractCardContainer _targetCardContainer ) {
			SwitchCardContainerUndoableCommand switchContainerCommand 
							= new SwitchCardContainerUndoableCommand( _card, 
																	GetCardContainer(_card),
																	_targetCardContainer );

			undoController.AddCommand( switchContainerCommand );
			undoController.MakePlay();
		}



		protected override void CheckIfColumnWasCompleted( CardFacade _placedCard ) {
			List<CardFacade> columnOfCards = GetCardColumn( _placedCard );

			if( IsColumnCompleted( columnOfCards ) ) {
				ColumnCompletitionCheckCommand columnCompletedCommand 
										= new ColumnCompletitionCheckCommand( columnOfCards,
											completedColumnContainers[completedColumnContainers.Count - 1],
											GetCardContainer( columnOfCards[0] ) );
				undoController.AddCommand( columnCompletedCommand );
				// MoveColumnToCompletedColumnContainer( columnOfCards );
				completedColumnContainers.RemoveAt( completedColumnContainers.Count - 1 );
				OnCardsCleared.Invoke( columnOfCards );
			}
		}
		#endregion
	}
}

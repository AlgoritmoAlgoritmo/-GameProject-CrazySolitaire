/*
* Author:	Iris Bermudez
* Date:		20/09/2024
*/



using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Misc.Command;
using Solitaire.Gameplay.Cards;
using Solitaire.Gameplay.CardContainers;



namespace Solitaire.GameModes.UndoableCommands {
    public class ColumnCompletitionCheckCommand : IUndoableCommand {
        #region Variables
        private List<CardFacade> columnOfCards;
        private AbstractCardContainer completedColumnContainer;
        private AbstractCardContainer originalCardContainer;
        #endregion


        #region Constructor methods
        public ColumnCompletitionCheckCommand( List<CardFacade> _columnOfCards,
                                            AbstractCardContainer _completedColumnContainer,
                                            AbstractCardContainer _originalCardContainer ) {
            columnOfCards = _columnOfCards;
            completedColumnContainer = _completedColumnContainer;
            originalCardContainer = _originalCardContainer;
        }
        #endregion


        #region Public methods
        public Task Execute() {
            throw new System.NotImplementedException();
        }

        public Task Undo() {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
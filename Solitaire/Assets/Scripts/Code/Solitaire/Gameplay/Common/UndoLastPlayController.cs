/*
* Author:	Iris Bermudez
* Date:		20/09/2024
*/



using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Misc.Command;



namespace Solitaire.Gameplay.Common {
	public class UndoLastPlayController {
        #region Variables
        private readonly List<IUndoableCommand> undoableCommandsToExecute;
        private bool isRunningACommand;

        private short currentCommandIndex;
        #endregion


        #region Constructors
        public UndoLastPlayController() {
            undoableCommandsToExecute = new List<IUndoableCommand>();
            currentCommandIndex = 0;
        }
        #endregion


        #region Public methods
        public void NewPlay() {
            undoableCommandsToExecute.Clear();
            currentCommandIndex = 0;
        }


        public void AddCommand( IUndoableCommand _commandToEnqueue ) {
            undoableCommandsToExecute.Add( _commandToEnqueue );
        }


        public void MakePlay() {
            RunNextCommand().WrapErrors();
        }

        public void UndoPlay() {
        }
        #endregion


        #region Private methods
        private async Task RunNextCommand() {
            if( isRunningACommand  ||  undoableCommandsToExecute.Count == 0 ) {
                return;
            }

            while(  currentCommandIndex < undoableCommandsToExecute.Count ) {
                isRunningACommand = true;
                ICommand commandToExecute = undoableCommandsToExecute[currentCommandIndex];
                currentCommandIndex++;
                await commandToExecute.Execute();
            }

            isRunningACommand = false;
        }



        /*
        private async Task RunNextUndoCommand() {
            if( isRunningACommand ) {
                return;
            }

            while( commandsToExecute.Count > 0 ) {
                isRunningACommand = true;
                ICommand commandToExecute = commandsToExecute.Dequeue();
                await commandToExecute.Execute();
            }

            isRunningACommand = false;
        }*/
        #endregion
    }
}
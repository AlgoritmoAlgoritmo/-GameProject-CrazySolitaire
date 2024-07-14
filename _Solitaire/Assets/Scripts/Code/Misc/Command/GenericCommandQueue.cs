/*
* Author:	Iris Bermudez
* Date:		27/03/2024
*/



using System.Collections.Generic;
using System.Threading.Tasks;



namespace Misc.Command {
	public class GenericCommandQueue {
		#region Variables
		private static GenericCommandQueue instance;
		public static GenericCommandQueue Instance => instance ??
									( instance = new GenericCommandQueue() );

		private readonly Queue<ICommand> commandsToExecute;
		private bool isRunningACommand;
        #endregion


        #region Constructors
        public GenericCommandQueue() {
            commandsToExecute = new Queue<ICommand>();
        }
        #endregion


        #region Public methods
        public void AddCommand( ICommand _commandToEnqueue ) {
            commandsToExecute.Enqueue( _commandToEnqueue );
            RunNextCommand().WrapErrors();
        }
        #endregion


        #region Private methods
        private async Task RunNextCommand() {
            if( isRunningACommand ) {
                return;
            }

            while( commandsToExecute.Count > 0 ) {
                isRunningACommand = true;
                ICommand commandToExecute = commandsToExecute.Dequeue();
                await commandToExecute.Execute();
            }

            isRunningACommand = false;
        }
        #endregion
    }
}
/*
* Author:	Iris Bermudez
* Date:		27/03/2024
*/



using UnityEngine;
using Misc.Command;
using System.Threading.Tasks;
using System.Collections;



namespace Solitaire.Feedbacks {
    public class CardMovementAnimationICommand : ICommand {
        #region Variables
        private Transform transformToMove;
        private Vector3 targetPosition;
        #endregion


        #region Constructors
        public CardMovementAnimationICommand( Transform _transformToMove, 
                                                Vector3 _targetPosition  ) {
            transformToMove = _transformToMove;
            targetPosition = _targetPosition;
        }
        #endregion


        #region Public methods
        public async Task Execute() {
            bool isRunning = true;
            float speed = .01f;
            Vector3 positionDiff = targetPosition - transformToMove.position;


            while (isRunning) {
                transformToMove.position += positionDiff * speed;

                if (transformToMove.position == targetPosition)
                    isRunning = false;

                await Task.Yield();
            }
        }


        public IEnumerator MovementAnimation( Vector3 _targetPosition, Transform _transformToMove ) {
            bool isRunning = true;
            float speed = .01f;
            Vector3 positionDiff = _targetPosition - _transformToMove.position;


            while( isRunning ) {
                _transformToMove.position += positionDiff * speed;

                if (_transformToMove.position == _targetPosition)
                    isRunning = false;

                yield return null;
            }
        }
        #endregion


        #region Private methods

        #endregion

    }
}
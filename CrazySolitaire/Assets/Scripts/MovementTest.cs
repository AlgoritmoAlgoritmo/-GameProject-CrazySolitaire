/*
* Author:	Iris Bermudez
* Date:		28/03/2024
*/



using Misc.Command;
using Solitaire.Feedbacks;
using UnityEngine;



public class MovementTest : MonoBehaviour {
    #region Variables
    public Transform transformToMove;
    public Transform targetPositionTransform;
    private CardMovementAnimationICommand command;
    #endregion


    #region Inherited methods
    private void Awake() {
        command = new CardMovementAnimationICommand( transformToMove, 
                                                    targetPositionTransform.position );
    }


    private void Update() {
        if( Input.GetKeyUp(KeyCode.S) ) {
            Debug.Log("Starting movement.");
            // StartCoroutine( command.MovementAnimation( targetPositionTransform.position,
            //                                            transformToMove) );
            GenericCommandQueue.Instance.AddCommand(command);
        }
    }
    #endregion


    #region Private methods

    #endregion
}

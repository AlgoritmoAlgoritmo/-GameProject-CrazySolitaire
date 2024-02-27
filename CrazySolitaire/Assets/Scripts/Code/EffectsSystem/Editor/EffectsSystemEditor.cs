/*
* Author:	Iris Bermudez
* Date:		23/02/2024
*/



using UnityEngine;
using UnityEditor;



namespace EffectsSystem {

    [CustomEditor(typeof(EffectsManager))]
    public class EffectsSystemEditor : Editor {
        #region Variables
        private SerializedProperty effects;
        #endregion


        #region Inherited methods
        private void OnEnable() {
            effects = serializedObject.FindProperty("effects");
        }


        public override void OnInspectorGUI() {
            EditorGUILayout.PropertyField( effects, new GUIContent("Effects") );
        }
        #endregion


        #region Public methods

        #endregion


        #region Private methods

        #endregion
    }
}
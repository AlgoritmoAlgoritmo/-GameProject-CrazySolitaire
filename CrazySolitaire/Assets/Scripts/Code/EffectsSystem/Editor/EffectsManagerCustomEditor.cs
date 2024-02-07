/*
* Author:	Iris Bermudez
* Date:		05/02/2024
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EffectsSystem.Effects.CameraEffects;

namespace EffectsSystem.Editor {
    [CustomEditor(typeof(EffectsManager))]
    public class EffectsManagerCustomEditor : UnityEditor.Editor {
        #region Variables
        private EffectsManager effectsManager;
        private SerializedObject serializedObject;
        #endregion


        #region Public methods

        #endregion


        #region Private methods
        public override void OnInspectorGUI() {
            effectsManager = (EffectsManager)target;
            serializedObject = new SerializedObject(effectsManager);

            serializedObject.Update();
            
            SerializedProperty property = serializedObject.FindProperty("effects");
            Debug.Log(property);
            EditorGUILayout.PropertyField(property, true);
            


            if (GUILayout.Button("Add effect")) {
                effectsManager.AddEffect( new CameraShakeEffects() );
            }

            if (GUILayout.Button("Play effects")) {
                effectsManager.Play();
            }
        }
        #endregion
    }
}
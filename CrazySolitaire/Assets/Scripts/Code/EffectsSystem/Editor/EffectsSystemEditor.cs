/*
* Author:	Iris Bermudez
* Date:		23/02/2024
*/



using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using EffectsSystem.Utils;



namespace EffectsSystem {
    [CustomEditor(typeof(EffectsManager))]
    public class EffectsSystemEditor : Editor {
        #region Variables
        private SerializedProperty effects;
        private int optionIndex = 0;
        private string basicEfefctsAssemblyName = "EffectSystem.Effects.Basic";

        private Dictionary<string, Type> typesDictionary = new Dictionary<string, Type>();
        #endregion


        #region Inherited methods
        private void OnEnable() {
            effects = serializedObject.FindProperty("effects");
            GetBasicEffectChildClassesTypes( GetAssembly() );

            Debug.Log("-------------");
            Debug.Log("Types found: ");
            foreach ( var auxEffect in typesDictionary.Values ) {
                Debug.Log( auxEffect );
            }
            Debug.Log("-------------");
        }


        public override void OnInspectorGUI() {
            // optionIndex = EditorGUILayout.Popup(optionIndex, options);
            EditorGUILayout.PropertyField( effects, new GUIContent("Effects") );
        }
        #endregion


        #region Public methods

        #endregion


        #region Private methods
        public Assembly GetAssembly() {
            foreach( var auxAssembly in AppDomain.CurrentDomain.GetAssemblies() ) {
                if ( auxAssembly.GetName().Equals( basicEfefctsAssemblyName ) ) {
                    return auxAssembly;
                }
            }

            return null;
        }

        public void GetBasicEffectChildClassesTypes( Assembly _assembly ) {
            if(  _assembly != null) {
                foreach( Type auxType in _assembly.GetTypes() ) {
                    if( auxType.IsSubclassOf( typeof(BasicEffect) ) ) {
                        // Save type to dictionary to
                        typesDictionary.Add( ( (BasicEffect)auxType ).MenuName, auxType );
                    }
                }
            }
        }
        #endregion
    }
}
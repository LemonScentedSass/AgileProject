using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EffectSystem
{
    [CustomEditor(typeof(ProjectileEffect))]
    public class ProjectileEffect_Editor : Editor
    {
        private ProjectileEffect _curPE;

        private void OnEnable()
        {
            _curPE = target as ProjectileEffect;
        }

        public override void OnInspectorGUI()
        {
            _curPE.doesComeBack = EditorGUILayout.Toggle("Does Come back", _curPE.doesComeBack);

            if (_curPE.doesComeBack == true)
            {
                _curPE.comeBackTime = EditorGUILayout.FloatField("Come back after seconds", _curPE.comeBackTime);
            }

            _curPE.speed = EditorGUILayout.FloatField("Speed", _curPE.speed);

            _curPE.objectPool = EditorGUILayout.ObjectField("Object Pool", _curPE.objectPool, typeof(Transform), true) as Transform;

            _curPE.projectileObject = EditorGUILayout.ObjectField("Item", _curPE.projectileObject, typeof(GameObject), true) as GameObject;
            _curPE.direction = (ProjectileEffect.Direction)EditorGUILayout.EnumPopup("Direction", _curPE.direction);

            EditorUtility.SetDirty(_curPE);
        }
    }
}
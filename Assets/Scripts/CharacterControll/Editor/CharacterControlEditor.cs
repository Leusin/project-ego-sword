using UnityEngine;
using UnityEditor;

namespace ProjectEgoSword
{
    [CustomEditor(typeof(CharacterControl), true)]
    public class CharacterControlEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CharacterControl control = (CharacterControl)target;

            if (GUILayout.Button("Setup RagdollParts (BodyParts)"))
            {
                control.SetRagdollParts();
            }
        }
    }
}

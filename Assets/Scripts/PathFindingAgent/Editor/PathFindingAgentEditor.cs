using UnityEngine;
using UnityEditor;

namespace ProjectEgoSword
{
    [CustomEditor(typeof(PathFindingAgent))]
public class PathFindingAgentEditor : Editor
{
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            PathFindingAgent pathFindingAgent = (PathFindingAgent)target;

            if(GUILayout.Button("Go To Target"))
            {
                pathFindingAgent.GoToTarget();
            }
        }
}
}
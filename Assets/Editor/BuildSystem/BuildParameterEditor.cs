using UnityEditor;
using UnityEngine;

namespace BuildSystem
{
    [CustomEditor(typeof(BuildParameter))]
    class BuildParameterEditor :UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Register Active Parameter"))
            {
                ((BuildParameter)target).RegisterActiveParameter();
            }

            if (GUILayout.Button("Build with this parameter"))
            {
                ((BuildParameter)target).Build();
            }
        }
    }
}

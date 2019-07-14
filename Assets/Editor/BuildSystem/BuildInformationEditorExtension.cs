using System.IO;
using UnityEditor;
using UnityEngine;


namespace BuildSystem
{
    public static class BuildInformationEditorExtension
    {
        public static void Save(this BuildInformation info)
        {
            if (!Directory.Exists("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            AssetDatabase.CreateAsset(info, "Assets/Resources/BuildInformation.asset");
        }
    }
}

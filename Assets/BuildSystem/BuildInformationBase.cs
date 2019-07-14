using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BuildSystem
{
    public abstract class BuildInformationBase : ScriptableObject
    {
#if UNITY_EDITOR
        public void Save()
        {
            if (!Directory.Exists("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            AssetDatabase.CreateAsset(this, "Assets/Resources/BuildInformation.asset");
        }
#endif
    }
}

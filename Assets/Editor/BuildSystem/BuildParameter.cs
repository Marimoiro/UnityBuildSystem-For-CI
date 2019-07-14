using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BuildSystem
{
    public class BuildParameter : ScriptableObject
    {

        public string Name;

        public string AppName;
        public string BundleIdentifier;

        public string Environment;




        public const string BuildParameterBase = @"Assets/Editor/BuildSystem";
        public const string DirectoryName = "BuildParameters";
        public static string BuildParameterPath => Path.Combine(BuildParameterBase, DirectoryName);
        public static BuildParameter GetActiveParameter()
        {
            return AssetDatabase.LoadAssetAtPath<BuildParameter>(Path.Combine(BuildParameterPath,"Active.asset"));
        }

        public void RegisterActiveParameter()
        {
            var p = Instantiate(this);
            AssetDatabase.CreateAsset(p, Path.Combine(BuildParameterPath, "Active.asset"));
        }

        public static void RegisterActiveParameter(string name)
        {
            var param = GetParameters().FirstOrDefault(p => p.Name == name);
            if (param == null)
            {
                throw new InvalidOperationException($"${name} parameter is not found");
            }

            param.RegisterActiveParameter();
        }

        [MenuItem("BuildSystem/Create BuildParameter")]
        public static void CreateNew()
        {
            
            if (!Directory.Exists(BuildParameterPath))
            {
                AssetDatabase.CreateFolder(BuildParameterBase, DirectoryName);
            }
            AssetDatabase.CreateAsset(new BuildParameter(), Path.Combine(BuildParameterPath, "NewBuildParameter.asset"));
        }

        public static BuildParameter[] GetParameters()
        {
            return Directory
                .GetFiles(BuildParameterPath, "*.asset")
                .Where( p => Path.GetFileNameWithoutExtension(p) != "Active")
                .Select(AssetDatabase.LoadAssetAtPath<BuildParameter>)
                .Where(a => a != null)
                .ToArray();
        }

        public static BuildParameter GetParameter(string name)
        {
            return GetParameters().First( p => p.Name == name);
        }

        public void Build()
        {
            RegisterActiveParameter();
            BuildProcess.Build();
        }

    }
}

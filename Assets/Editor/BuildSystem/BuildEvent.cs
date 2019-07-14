using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace BuildSystem
{
    class BuildEvent
    {
        public static void SetAndroidParameters()
        {

            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;

            PlayerSettings.Android.keystoreName = "sample.keystore";
            PlayerSettings.Android.keystorePass = "sample";
            PlayerSettings.Android.keyaliasName = "sample";
            PlayerSettings.Android.keyaliasPass = "sample";
        }

        public static BuildOptions PreBuild(BuildTarget target,BuildParameter parameter)
        {

            Debug.Log($"Build {target} Start" + Environment.NewLine + JsonUtility.ToJson(parameter));

            var bi = new BuildInformation
            {
                BuildDateBinary = DateTime.Now.ToBinary(),
                BundleIdentifier = parameter.BundleIdentifier
            };

            bi.Save();

            SetAndroidParameters();

            return BuildOptions.None;
        }

        public static void PostBuild(BuildParameter parameter)
        {
            Debug.Log("Build End" + Environment.NewLine + JsonUtility.ToJson(parameter));
        }

        public static void BuildError(BuildReport report, BuildParameter parameter)
        {
            Debug.LogError("Build Error" + Environment.NewLine + JsonUtility.ToJson(parameter));

            var error = String.Join(Environment.NewLine, report.steps.SelectMany(s => s.messages).Select(m => $"{m.type} {m.content}"));
            Debug.LogError(error);

            throw new InvalidOperationException("Build Error");
        }
    }
}

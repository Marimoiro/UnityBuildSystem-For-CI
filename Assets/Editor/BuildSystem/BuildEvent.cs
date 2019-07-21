using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace BuildSystem
{
    class BuildEvent : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public static void SetAndroidParameters()
        {

            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;

            PlayerSettings.Android.keystoreName = "sample.keystore";
            PlayerSettings.Android.keystorePass = "sample";
            PlayerSettings.Android.keyaliasName = "sample";
            PlayerSettings.Android.keyaliasPass = "sample";
        }



        public int callbackOrder { get; } = 0;

        public static BuildOptions GetBuildOption(BuildParameter parameter)
        {
            return BuildOptions.None;
        }
        public void OnPostprocessBuild(BuildReport report)
        {
            var parameter = BuildParameter.GetActiveParameter();
            Debug.Log("Build End" + Environment.NewLine + JsonUtility.ToJson(parameter));
        }

        public void OnPreprocessBuild(BuildReport report)
        {

            var parameter = BuildParameter.GetActiveParameter();
            Debug.Log($"Build {report.summary.platform} Start" + Environment.NewLine + JsonUtility.ToJson(parameter));

            PlayerSettings.applicationIdentifier = parameter.BundleIdentifier;
            PlayerSettings.productName = parameter.AppName;

            var bi = ScriptableObject.CreateInstance<BuildInformation>();

            bi.BuildDateBinary = DateTime.Now.ToBinary();
            bi.BundleIdentifier = parameter.BundleIdentifier;
            bi.Environment = parameter.Environment;
            

            bi.Save();

            SetAndroidParameters();
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

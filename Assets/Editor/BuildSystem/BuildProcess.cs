using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace BuildSystem
{
    public static class BuildProcess
    {
        public static string BuildDirectory = "Build";


        private static string[] GetBuildScenes()
        {
            return EditorBuildSettings.scenes
                .Where(s => s.enabled).Where(s => File.Exists(s.path))
                .Select(s => s.path).ToArray();
        }

        public static string GetBuildPath()
        {
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return Path.Combine(BuildDirectory, "build.apk");
                case BuildTarget.iOS:
                    return Path.Combine(BuildDirectory, "xcode");
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.StandaloneWindows:
                    return Path.Combine(BuildDirectory, "build.exe");
                default:
                    throw new InvalidOperationException(EditorUserBuildSettings.activeBuildTarget +
                                                        "is not supported!");
                    

            }
        }



        /// <summary>
        /// Build active build target
        /// </summary>
        [MenuItem("BuildSystem/Build with Active Parameters")]
        public static void Build()
        {

            //Clear Directory
            if (Directory.Exists(BuildDirectory))
            {
                Directory.Delete(BuildDirectory, true);
            }

            AssetDatabase.CreateFolder("", BuildDirectory);

            var target = EditorUserBuildSettings.activeBuildTarget;


            var report = BuildPipeline.BuildPlayer(GetBuildScenes(), GetBuildPath(), target, BuildEvent.GetBuildOption(BuildParameter.GetActiveParameter()));

            if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
            {
                return;
            }

            BuildEvent.BuildError(report, BuildParameter.GetActiveParameter());
        }

        /// <summary>
        /// Build specific target
        /// </summary>
        /// <param name="target"></param>
        public static void Build(BuildTarget target)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildPipeline.GetBuildTargetGroup(target), target);
            Build();
        }

        public static void Build(string buildParameterName)
        {
            BuildParameter.GetParameter(buildParameterName).Build();
        }
    }
}
     
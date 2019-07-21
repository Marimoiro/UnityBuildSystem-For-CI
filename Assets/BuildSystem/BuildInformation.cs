using System;
using UnityEngine;

namespace BuildSystem
{
    public class BuildInformation : ScriptableObject
    {

        public string Environment;
        public long BuildDateBinary;
        public DateTime BuildDate => DateTime.FromBinary(BuildDateBinary);

        public string BundleIdentifier;


        private static BuildInformation instance;

        public static BuildInformation Instance
        {
            get { return instance = instance ?? Resources.Load<BuildInformation>("BuildInformation"); }
        }
    }
}
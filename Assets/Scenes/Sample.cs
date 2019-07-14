using System.Collections;
using System.Collections.Generic;
using System.Data;
using BuildSystem;
using UnityEngine;
using UnityEngine.UI;

public class Sample : MonoBehaviour
{
    public Text BundleId;
    public Text BuildDate;
    void Start()
    {
        var i = BuildInformation.Instance;
        BundleId.text = i.BundleIdentifier;
        BuildDate.text = i.BuildDate.ToLongDateString() + " " + i.BuildDate.ToLongTimeString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

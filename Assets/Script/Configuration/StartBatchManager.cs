using UnityEngine;
using System.Diagnostics;
using System.IO;
using Lavender.Systems;
using UnityEngine.UI;

public class StartBatchManager : MonoBehaviour
{
    uint pid = 0;

    private void Start() 
    {
        ConfigManager.EnsureInitialization();
        StartBat();
    }

    public void StartBat()
    {
        if (ConfigManager.config.batFileLocation != "" && ConfigManager.config.AutoStart)
            pid = StartExternalProcess.Start(ConfigManager.config.batFileLocation);
        UnityEngine.Debug.Log("Batch file with PID: " + pid);
    }

    public void StopBat()
    {
        if (pid != 0)
        {
            StartExternalProcess.KillProcess(pid);
            UnityEngine.Debug.Log("Batch file with PID: " + pid + " killed");
        }
    }

    private void ExitApplication()
    {
        OnDestroy();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    private void OnDestroy() 
    {
        StopBat();
    }
}

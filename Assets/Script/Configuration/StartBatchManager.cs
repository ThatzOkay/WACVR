using UnityEngine;
using System.Diagnostics;
using System.IO;
using Lavender.Systems;
using UnityEngine.UI;

public class StartBatchManager : MonoBehaviour
{
    uint pid = 0;
	public Toggle AutoStartCheckBox;

    private void Start() 
    {
        ConfigManager.EnsureInitialization();
        if (ConfigManager.config.batFileLocation != "" && ConfigManager.config.batFileAutoStart)
            pid = StartExternalProcess.Start(ConfigManager.config.batFileLocation);
        UnityEngine.Debug.Log("Batch file with PID: " + pid);

        AutoStartCheckBox.isOn = ConfigManager.config.batFileAutoStart;
    }

    public void StartBat()
    {
        Start();
    }

    public void StopBat()
    {
        OnDestroy();
    }

    public void SetAutoStartValue(bool Active)
    {
        ConfigManager.config.batFileAutoStart = Active;
        ConfigManager.SaveFile();
    }
    private void OnDestroy() 
    {
        if (pid != 0)
        {
            StartExternalProcess.KillProcess(pid);
            UnityEngine.Debug.Log("Batch file with PID: " + pid + " killed");
        }
    }
}

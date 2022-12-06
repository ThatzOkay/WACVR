using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class SpectatorManager : MonoBehaviour
{
    CameraSmooth cameraSmooth;
    Camera SpectatorCam;
    public Transform SpectatorFPTarget;
    public Transform SpectatorTPTarget;

    void Start()
    {
        cameraSmooth = GetComponent<CameraSmooth>();
        SpectatorCam = GetComponent<Camera>();
        var modeWidget = ConfigManager.GetConfigPanelWidget("SpectatorMode");
        var fovWidget = ConfigManager.GetConfigPanelWidget("SpectatorFOV");
        var fpsWidget = ConfigManager.GetConfigPanelWidget("SpectatorFPS");
        var smoothWidget = ConfigManager.GetConfigPanelWidget("SpectatorSmooth");


        var modeDropdown = modeWidget.GetComponent<TMP_Dropdown>();
        var fovSlider = fovWidget.GetComponent<Slider>();
        var fpsDropdown = fpsWidget.GetComponent<TMP_Dropdown>();
        var smoothSlider = smoothWidget.GetComponent<Slider>();

        modeDropdown.onValueChanged.AddListener((int value) => {
            if (SpectatorCam == null || cameraSmooth == null || SpectatorFPTarget == null || SpectatorTPTarget == null) 
                return;
            switch (value)
            {
                case 0:
                    if (gameObject.activeSelf)
                        gameObject.SetActive(false);
                    break;
                case 1:
                    if (!gameObject.activeSelf)
                        gameObject.SetActive(true);
                    cameraSmooth.target = SpectatorFPTarget;
                    cameraSmooth.smoothSpeed = (float)ConfigManager.config.SpectatorSmooth;
                    SpectatorCam.cullingMask |= 1 << LayerMask.NameToLayer("TPBlock"); // Enable TPBlock Layer Mask
                    SpectatorCam.cullingMask &=  ~(1 << LayerMask.NameToLayer("FPBlock")); // Disable FPBlock Layer Mask
                    break;
                case 2:
                    if (!gameObject.activeSelf)
                        gameObject.SetActive(true);
                    cameraSmooth.target = SpectatorTPTarget;
                    cameraSmooth.smoothSpeed = 1;
                    SpectatorCam.cullingMask &=  ~(1 << LayerMask.NameToLayer("TPBlock")); // Disable TPBlock Layer Mask
                    SpectatorCam.cullingMask |= 1 << LayerMask.NameToLayer("FPBlock"); // Enable FPBlock Layer Mask
                    break;
            }
        });

        fovSlider.onValueChanged.AddListener((float value) => {
            SpectatorCam.fieldOfView = value;
        });

        fpsDropdown.onValueChanged.AddListener((int value) => {
            var fpsString = Enum.GetName(typeof(CEnum.FPS), value);
            Application.targetFrameRate = int.Parse(fpsString.Remove(0, 3));
        });

        smoothSlider.onValueChanged.AddListener((float value) => {
            cameraSmooth.smoothSpeed = value;
        });

        modeDropdown.onValueChanged?.Invoke(modeDropdown.value);
        fovSlider.onValueChanged?.Invoke(fovSlider.value);
        fpsDropdown.onValueChanged?.Invoke(fpsDropdown.value);
        smoothSlider.onValueChanged?.Invoke(smoothSlider.value);
    
        ApplyTPCamTransform();
    }

    void ApplyTPCamTransform() 
    {
        if (SpectatorTPTarget == null)
            return;
        switch (ConfigManager.config.TPCamActivePin)
        {
            default:
            case 0:
                SpectatorTPTarget.position = new Vector3(ConfigManager.config.TPCamPosition[0],
                                                ConfigManager.config.TPCamPosition[1],
                                                ConfigManager.config.TPCamPosition[2]);
                SpectatorTPTarget.rotation = Quaternion.Euler(ConfigManager.config.TPCamRotation[0],
                                                             ConfigManager.config.TPCamRotation[1],
                                                             ConfigManager.config.TPCamRotation[2]);
                break;
            case 1:
                SpectatorTPTarget.position = new Vector3(ConfigManager.config.TPCamPosition1[0],
                                                ConfigManager.config.TPCamPosition1[1],
                                                ConfigManager.config.TPCamPosition1[2]);
                SpectatorTPTarget.rotation = Quaternion.Euler(ConfigManager.config.TPCamRotation1[0],
                                                             ConfigManager.config.TPCamRotation1[1],
                                                             ConfigManager.config.TPCamRotation1[2]);
                break;
            case 2:
                SpectatorTPTarget.position = new Vector3(ConfigManager.config.TPCamPosition2[0],
                                                ConfigManager.config.TPCamPosition2[1],
                                                ConfigManager.config.TPCamPosition2[2]);
                SpectatorTPTarget.rotation = Quaternion.Euler(ConfigManager.config.TPCamRotation2[0],
                                                             ConfigManager.config.TPCamRotation2[1],
                                                             ConfigManager.config.TPCamRotation2[2]);
                break;

        }
        
    }

    public void RecallCameraLocation(int pinNumber)
    {
        if (SpectatorTPTarget == null)
            return;
        switch (pinNumber)
        {
            default:
            case 0:
                SpectatorTPTarget.position = new Vector3(ConfigManager.config.TPCamPosition[0],
                                                ConfigManager.config.TPCamPosition[1],
                                                ConfigManager.config.TPCamPosition[2]);
                SpectatorTPTarget.rotation = Quaternion.Euler(ConfigManager.config.TPCamRotation[0],
                                                             ConfigManager.config.TPCamRotation[1],
                                                             ConfigManager.config.TPCamRotation[2]);
                break;
            case 1:
                SpectatorTPTarget.position = new Vector3(ConfigManager.config.TPCamPosition1[0],
                                                ConfigManager.config.TPCamPosition1[1],
                                                ConfigManager.config.TPCamPosition1[2]);
                SpectatorTPTarget.rotation = Quaternion.Euler(ConfigManager.config.TPCamRotation1[0],
                                                             ConfigManager.config.TPCamRotation1[1],
                                                             ConfigManager.config.TPCamRotation1[2]);
                break;
            case 2:
                SpectatorTPTarget.position = new Vector3(ConfigManager.config.TPCamPosition2[0],
                                                ConfigManager.config.TPCamPosition2[1],
                                                ConfigManager.config.TPCamPosition2[2]);
                SpectatorTPTarget.rotation = Quaternion.Euler(ConfigManager.config.TPCamRotation2[0],
                                                             ConfigManager.config.TPCamRotation2[1],
                                                             ConfigManager.config.TPCamRotation2[2]);
                break;

        }
        ConfigManager.config.TPCamActivePin = pinNumber;
        ConfigManager.SaveFile();
    }

    public void SaveTransform() // will be called from TPCameraCube
    {
        if (SpectatorTPTarget == null)
            return;
        switch (ConfigManager.config.TPCamActivePin)
        {
            default:
            case 0:
                ConfigManager.config.TPCamPosition[0] = SpectatorTPTarget.position.x;
                ConfigManager.config.TPCamPosition[1] = SpectatorTPTarget.position.y;
                ConfigManager.config.TPCamPosition[2] = SpectatorTPTarget.position.z;
                ConfigManager.config.TPCamRotation[0] = SpectatorTPTarget.rotation.eulerAngles.x;
                ConfigManager.config.TPCamRotation[1] = SpectatorTPTarget.rotation.eulerAngles.y;
                ConfigManager.config.TPCamRotation[2] = SpectatorTPTarget.rotation.eulerAngles.z;
                break;
            case 1:
                ConfigManager.config.TPCamPosition1[0] = SpectatorTPTarget.position.x;
                ConfigManager.config.TPCamPosition1[1] = SpectatorTPTarget.position.y;
                ConfigManager.config.TPCamPosition1[2] = SpectatorTPTarget.position.z;
                ConfigManager.config.TPCamRotation1[0] = SpectatorTPTarget.rotation.eulerAngles.x;
                ConfigManager.config.TPCamRotation1[1] = SpectatorTPTarget.rotation.eulerAngles.y;
                ConfigManager.config.TPCamRotation1[2] = SpectatorTPTarget.rotation.eulerAngles.z;
                break;
            case 2:
                ConfigManager.config.TPCamPosition2[0] = SpectatorTPTarget.position.x;
                ConfigManager.config.TPCamPosition2[1] = SpectatorTPTarget.position.y;
                ConfigManager.config.TPCamPosition2[2] = SpectatorTPTarget.position.z;
                ConfigManager.config.TPCamRotation2[0] = SpectatorTPTarget.rotation.eulerAngles.x;
                ConfigManager.config.TPCamRotation2[1] = SpectatorTPTarget.rotation.eulerAngles.y;
                ConfigManager.config.TPCamRotation2[2] = SpectatorTPTarget.rotation.eulerAngles.z;
                break;

        }
        ConfigManager.SaveFile();
    }
}

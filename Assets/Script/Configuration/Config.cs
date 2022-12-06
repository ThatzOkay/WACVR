using System.Collections;
using WindowsInput.Native;
using UnityEngine;

public class Config
{
    public int CaptureMode = 3;
    public int CaptureFPS = 4;
    public bool CaptureDesktop = false;
    public int DesktopIndex = 0;
    public int SpectatorMode = 2;
    public int SpectatorFPS = 2;
    public float SpectatorFOV = 40;
    public float SpectatorSmooth = 0.05f;
    public int TPCamActivePin = 0;
    public float[] TPCamPosition = new float[3] { -0.6f, 1.8f, -1.2f };
    public float[] TPCamRotation = new float[3] { 23, 35, 0 };
    public float[] TPCamPosition1 = new float[3] { -0.6f, 1.8f, -1.2f };
    public float[] TPCamRotation1 = new float[3] { 23, 35, 0 };
    public float[] TPCamPosition2 = new float[3] { -0.6f, 1.8f, -1.2f };
    public float[] TPCamRotation2 = new float[3] { 23, 35, 0 };
    public float HandSize = 0.08f;
    public float HandX = 0;
    public float HandY = 0;
    public float HandZ = 0;
    public float PlayerHeight = 0;
    public int Skybox = 0;
    public int Enviroment = 0;
    public float HapticDuration = 0.1f;
    public float HapticAmplitude = 0.75f;
    public int TouchSampleRate  = 3;
    public int HandTrackingMode = 1;
    public float Threshold = 0.3f;
    public bool TouchAirWall = false;
    public bool UseIPCLighting = true;
    public bool UseIPCTouch = true;
    public float LightStrength = 1.25f;
    public int TestKeyBind = 40;
    public int ServiceKeyBind = 41;
    public int CoinKeyBind = 31;
    public int CustomKeyBind = 170;
    public bool FlatShadedRing = false;
    public bool DynamicProbe = true;
    public bool PostProcess = true;
    public int AntiAliasing = 0;
    public bool batFileAutoStart = true;
    public string batFileLocation = "";

}

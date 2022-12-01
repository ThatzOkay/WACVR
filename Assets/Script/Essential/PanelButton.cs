using System;
using System.Runtime.InteropServices;
using UnityEngine;
using WindowsInput.Native;

public class PanelButton : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern uint MapVirtualKey(uint uCode, uint uMapType);
    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

    public event Action ButtonPressed;
    public event Action ButtonReleased;

    public VirtualKeyCode key;
    public VirtualKeyCode key2;

    public bool isToggle;
    public bool doesBeep;

    public bool isOn;
    private int _insideColliderCount = 0;

    //public GameObject camera; // just generating warnings lmao
    public AudioSource audioSrc;

    void Start()
    {
        if (isToggle)
        {
            // initialize toggle state
            ButtonPress();
            ButtonRelease();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _insideColliderCount += 1;
        ButtonPress();
        if (doesBeep)
            audioSrc?.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        _insideColliderCount = Mathf.Clamp(_insideColliderCount - 1, 0, _insideColliderCount);

        if (_insideColliderCount == 0)
        {
            ButtonRelease();
        }
    }

    private void ButtonPress()
    {
        ButtonPressed?.Invoke();
        if (isToggle)
        {
            if (!isOn)
            {
                keybd_event(System.Convert.ToByte(key2), (byte)MapVirtualKey((uint)key2, 0), 2, UIntPtr.Zero);
                keybd_event(System.Convert.ToByte(key), (byte)MapVirtualKey((uint)key, 0), 0, UIntPtr.Zero);
                isOn = true;
            }
            else
            {
                keybd_event(System.Convert.ToByte(key), (byte)MapVirtualKey((uint)key, 0), 2, UIntPtr.Zero);
                keybd_event(System.Convert.ToByte(key2), (byte)MapVirtualKey((uint)key2, 0), 0, UIntPtr.Zero);
                isOn = false;
            }

        }
        else
        {
            keybd_event(System.Convert.ToByte(key), (byte)MapVirtualKey((uint)key, 0), 0, UIntPtr.Zero);
        }
    }

    private void ButtonRelease()
    {
        ButtonReleased?.Invoke();
        keybd_event(System.Convert.ToByte(key), (byte)MapVirtualKey((uint)key, 0), 2, UIntPtr.Zero);
        keybd_event(System.Convert.ToByte(key2), (byte)MapVirtualKey((uint)key2, 0), 2, UIntPtr.Zero);
    }

    public void PressButton()
    {
        ButtonPress();
        if (doesBeep)
            audioSrc?.Play();
        ButtonRelease();
    }
}

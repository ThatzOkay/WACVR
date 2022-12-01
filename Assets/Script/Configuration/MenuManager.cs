using UnityEngine;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> ObjectsToToggle;
    void Start()
    {

    }

    public void ToggleMenu()
    {
        //bool isOpen = ObjectsToToggle[0].activeSelf;
        foreach (var tab in ObjectsToToggle)
        {
            tab.SetActive(!tab.activeSelf);
        }
    }
}
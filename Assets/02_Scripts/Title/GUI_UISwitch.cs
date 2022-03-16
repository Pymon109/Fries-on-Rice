using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GUI_UISwitch : MonoBehaviour
{
    ScreenState state;
    public void ButtonOnShowUI() 
    { 
        gameObject.SetActive(true); 
        if(state)
            HardwareInputManager.instance.PushStateStack(state);
    }
    public void ButtonOnHideUI() 
    {
        gameObject.SetActive(false);
        if(state)
            HardwareInputManager.instance.EscapeCurrentState();
    }


    private void Awake()
    {
        state = GetComponent<ScreenState>();
    }
    private void Update()
    {
        //DetectTouch();
    }
}

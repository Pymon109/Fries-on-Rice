using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_GameQuitPopupButtons : MonoBehaviour
{
    [SerializeField] ScreenState m_state;
    [SerializeField] ScreenState m_titleState;
    public void btn_yse()
    {
        m_state.DoBackProcess();
    }

    public void btn_no()
    {
        HardwareInputManager.instance.PushStateStack(m_titleState);
        gameObject.SetActive(false);
    }
}

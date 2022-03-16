using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenState_Floating : ScreenState
{
    [SerializeField] GameObject m_gScreen;
    override public void DoBackProcess()
    {
        m_gScreen.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenState_Title : ScreenState
{
    [SerializeField] GameObject m_gGameQuitPopUp;
    override public void DoBackProcess()
    {
        m_gGameQuitPopUp.SetActive(true);
    }

    private void Start()
    {
        m_eStateType = E_SCREEN_STATE_TYPE.TITLE;
    }
}

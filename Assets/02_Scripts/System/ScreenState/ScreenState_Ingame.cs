using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenState_Ingame : ScreenState
{
    [SerializeField] GameObject m_gGameStopPopup;
    override public void DoBackProcess()
    {
        m_gGameStopPopup.SetActive(true);
        IngameManager.instance.GameStop();
    }

    private void Start()
    {
        m_eStateType = E_SCREEN_STATE_TYPE.INGAME;
    }
}

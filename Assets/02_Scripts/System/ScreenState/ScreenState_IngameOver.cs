using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenState_IngameOver : ScreenState
{
    override public void DoBackProcess()
    {
        HardwareInputManager.instance.JustPopStateStack();
        HardwareInputManager.instance.JustPopStateStack();
        LoadingSceneManager.LoadScene("TitleScene");
    }

    private void Start()
    {
        m_eStateType = E_SCREEN_STATE_TYPE.INGAME_OVER;
    }
}

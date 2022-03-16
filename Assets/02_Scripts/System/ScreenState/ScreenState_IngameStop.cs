using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenState_IngameStop : ScreenState
{
    
    override public void DoBackProcess()
    {
        //IngameManager.instance.guiManager.Btn_Main();

        IngameManager.instance.SetState(IngameManager.E_INGAMESTATE.GAMEOVER);
        //HardwareInputManager.instance.JustPopStateStack();
        LoadingSceneManager.LoadScene("TitleScene");
    }

    private void Start()
    {
        m_eStateType = E_SCREEN_STATE_TYPE.INGAME_STOP;
    }
}

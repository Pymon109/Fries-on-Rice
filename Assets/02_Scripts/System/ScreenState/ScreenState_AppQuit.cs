using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenState_AppQuit : ScreenState
{
    override public void DoBackProcess()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void Start()
    {
        m_eStateType = E_SCREEN_STATE_TYPE.APP_QUIT;
    }
}

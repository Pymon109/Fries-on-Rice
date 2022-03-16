using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenState : MonoBehaviour
{
    public enum E_SCREEN_STATE_TYPE
    {
        APP_QUIT = 0,
        TITLE,
        OPTION,
        GOLD_SHOP,
        FRY_SHOP,
        COLLECTTION,
        INGAME,
        INGAME_STOP,
        INGAME_OVER,
        POPUP
    }
    [SerializeField] protected E_SCREEN_STATE_TYPE m_eStateType;
    public E_SCREEN_STATE_TYPE eStateType { get { return m_eStateType; } }

    public void PushSelf()
    {
        HardwareInputManager.instance.PushStateStack(this);
    }
    public virtual void DoBackProcess() { }
}

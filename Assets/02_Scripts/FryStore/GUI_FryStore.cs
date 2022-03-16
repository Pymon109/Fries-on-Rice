using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_FryStore : MonoBehaviour
{
    [SerializeField] GUI_FryStoreScrollView m_guiFryStoreScrollView;
    [SerializeField] GUI_StorePopUp m_guiPopUp;
    public GUI_StorePopUp guiPopUp { get { return m_guiPopUp; } }

/*    public void StoreOpen()
    {
        gameObject.SetActive(true);
        HardwareInputManager.instance.PushStateStack(GetComponent<ScreenState>());
    }

    public void StoreClose()
    {
        HardwareInputManager.instance.EscapeCurrentState();
    }*/

    private void Start()
    {
        //m_guiFryStoreScrollView.guiFryStore = this;
    }
}

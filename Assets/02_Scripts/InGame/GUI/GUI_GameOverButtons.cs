using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_GameOverButtons : MonoBehaviour
{
    [SerializeField] GUI_UISwitch m_fryStore;
    [SerializeField] GUI_FryStoreScrollView m_fryStoreScrollView;
    public void Btn_Main()
    {
        HardwareInputManager.instance.EscapeCurrentState();
    }

    public void Btn_Again()
    {
        IngameManager.instance.SetState(IngameManager.E_INGAMESTATE.INIT);
        HardwareInputManager.instance.JustPopStateStack();
    }

    public void Btn_Store()
    {
        Time.timeScale = 1f;
        m_fryStore.ButtonOnShowUI();
        m_fryStoreScrollView.UpdateDisplay();
    }

    public void Btn_AD()
    {
        if (AdmobManager.instance.ShowRewardAd())
            PlayerData.instance.AddGold(IngameManager.instance.ingameGold.earnedGold * 4);
        IngameManager.instance.guiManager.guiGameOver.SetADButtonInterActive(false);
    }
}

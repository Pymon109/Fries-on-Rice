using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButtons : MonoBehaviour
{
    [SerializeField] GUI_UISwitch m_fryStore;
    [SerializeField] GUI_UISwitch m_fryEncylopedia;
    [SerializeField] GUI_FryEncyclopediaScrollView m_fryEncylopediaScrollView;
    [SerializeField] GUI_FryStoreScrollView m_fryStoreScrollView;
    public void Btn_DifficultNormal()
    {
        GameManager.instance.eGameMode = IngameManager.E_GAMEMODE.NORMAL;
        LoadingSceneManager.LoadScene("InGameScene");
    }
    public void Btn_DifficultHard()
    {
        GameManager.instance.eGameMode = IngameManager.E_GAMEMODE.HARD;
        LoadingSceneManager.LoadScene("InGameScene");
        // 각각 난이도마다 다른 씬으로 나눌것인지는 미정
    }
    public void Btn_Ranking()
    {
        GameCenterManager.instance.ShowLeaderBoeard();
    }
    public void Btn_Encyclopedia()
    {
        m_fryEncylopedia.ButtonOnShowUI();
        m_fryEncylopediaScrollView.UpdateDisplay();
    }

    public void Btn_FryStore()
    {
        m_fryStore.ButtonOnShowUI();
        m_fryStoreScrollView.UpdateDisplay();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GUI_GameOver m_gameOver;
    public GUI_GameOver guiGameOver { get { return m_gameOver; } }

    [SerializeField] GoldShopManager m_goldShopManager;
    [SerializeField] GUI_TopBar m_topBar;
    public GUI_TopBar gui_topBar { get { return m_topBar; } }

    [SerializeField] GameObject m_StopButton;
    [SerializeField] GameObject m_gGameStopPopup;

    [SerializeField] float m_fDelayTimeForResult;
    public float fDelayTimeForResult { set { m_fDelayTimeForResult = value; } }
    IEnumerator WaitForResultUI(bool command)
    {
        yield return new WaitForSeconds(m_fDelayTimeForResult);
        SoundManager.instance.SFXAudioSourcePlay(SoundManager.E_SFX_TYPE.GAMEOVER);
        SetActiveResultUI(command);
    }

    public void ShowResult(bool command)
    {
        

        m_goldShopManager.gameObject.SetActive(command);
        m_StopButton.SetActive(!command);
        m_topBar.SetActiveIngameGoldUI(!command);

        float time;
        if (command)
        {
            time = 0.1f;
            StartCoroutine(WaitForResultUI(command));
        }
        else
        {
            time = 1f;
            SetActiveResultUI(command);
        }
        Time.timeScale = time;
        Time.fixedDeltaTime = 0.02f * time;
    }

    void SetActiveResultUI(bool command)
    {
        m_gameOver.gameObject.SetActive(command);
        m_gameOver.SetGameOverScreen();

        if (command)
            HardwareInputManager.instance.PushStateStack(m_gameOver.GetComponent<ScreenState>());
    }

    public void ShowGameStopPopup()
    {
        HardwareInputManager.instance.EscapeCurrentState();
    }

    public void HideGameStopPopup()
    {
        m_gGameStopPopup.SetActive(false);
        IngameManager.instance.GameContinue();
        HardwareInputManager.instance.PushStateStack(IngameManager.instance.GetComponent<ScreenState>());
    }

    public void Btn_Main()
    {
        HardwareInputManager.instance.EscapeCurrentState();
    }
}

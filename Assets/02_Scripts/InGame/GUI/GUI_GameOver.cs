using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_GameOver : MonoBehaviour
{
    [SerializeField] Text m_txtStackCount;
    [SerializeField] Text m_txtMaxRecord;
    [SerializeField] Text m_txtearnedGold;

    [SerializeField] Button m_btnAD;

    //[SerializeField] GameObject m_gNewRecordEffect;

    public void SetGameOverScreen()
    {
        m_txtStackCount.text = IngameManager.instance.stack.GetStackCount().ToString();
        string mode = "{mode}";
        switch(IngameManager.instance.gameMode)
        {
            case IngameManager.E_GAMEMODE.NORMAL:
                mode = "ui_012";
                break;
            case IngameManager.E_GAMEMODE.HARD:
                mode = "ui_013";
                break;
        }

        string text = "";
        if (OptionManager.instance)
        {
            var value = OptionManager.instance.GetCurrentOptionValue(OptionManager.E_OPTION_TYPE.LANGAUGE);
            switch (value)
            {
                case OptionManager.E_OPTION_VALUE.KOR:
                    text = DataManager.instance.languageData.GetStrings(mode).kor;
                    break;

                case OptionManager.E_OPTION_VALUE.ENG:
                    text = DataManager.instance.languageData.GetStrings(mode).eng;
                    break;
            }
        }
        

        //m_txtMaxRecord.text = mode + " 최고 기록 " + PlayerData.instance.GetBestRecord(IngameManager.instance.gameMode);
        m_txtearnedGold.text = IngameManager.instance.ingameGold.earnedGold.ToString();

        m_txtMaxRecord.gameObject.GetComponent<Langauge>().frontWord = text + " ";
        if(PlayerData.instance)
        {
            m_txtMaxRecord.gameObject.GetComponent<Langauge>().backtWord = " " + PlayerData.instance.GetBestRecord(IngameManager.instance.gameMode).ToString();
        }
/*        if (IngameManager.instance.stack.GetStackCount() >= PlayerData.instance.GetBestRecord(IngameManager.instance.gameMode))
            m_gNewRecordEffect.SetActive(true);
        else
            m_gNewRecordEffect.SetActive(false);*/

        if (IngameManager.instance.ingameGold.earnedGold > 0)
            SetADButtonInterActive(true);
        else
            SetADButtonInterActive(false);

        //m_txtearnedGold.gameObject.GetComponent<Langauge>().backtWord = " " + IngameManager.instance.ingameGold.earnedGold.ToString().ToString();
    }

    public void SetADButtonInterActive(bool command)
    {
        m_btnAD.interactable = command;
    }

    private void Update()
    {
/*        float time = 1f;
        if (gameObject.activeInHierarchy)
            time = 0.1f;
        Time.timeScale = time;
        Time.fixedDeltaTime = 0.02f * time;*/
    }
}

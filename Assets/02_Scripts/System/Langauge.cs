using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Langauge : MonoBehaviour
{
    Text m_txtTarget;
    [SerializeField] string m_sID;
    public string id { set { m_sID = value; } }

    string m_sFrontWord = "";
    public string frontWord { set { m_sFrontWord = value; } }
    string m_sBacktWord = "";
    public string backtWord { set { m_sBacktWord = value; } }

    void UpdateLangauege()
    {
        var value = OptionManager.instance.GetCurrentOptionValue(OptionManager.E_OPTION_TYPE.LANGAUGE);
        string text = "";
        switch(value)
        {
            case OptionManager.E_OPTION_VALUE.KOR:
                text = m_sFrontWord + DataManager.instance.languageData.GetStrings(m_sID).kor + m_sBacktWord;
                break;

            case OptionManager.E_OPTION_VALUE.ENG:
                text = m_sFrontWord + DataManager.instance.languageData.GetStrings(m_sID).eng + m_sBacktWord;
                break;
        }

        m_txtTarget.text = text;
    }

    private void Awake()
    {
        m_txtTarget = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateLangauege();
    }
}

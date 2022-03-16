using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    [SerializeField] Sprite m_imgOn;
    [SerializeField] Sprite m_imgOff;

    [SerializeField] Sprite m_imgKor_on;
    [SerializeField] Sprite m_imgKor_off;

    [SerializeField] Sprite m_imgEng_on;
    [SerializeField] Sprite m_imgEng_off;

    [SerializeField] Button m_btnBGM;
    [SerializeField] Button m_btnSFX;

    [SerializeField] Button m_btnKor;
    [SerializeField] Button m_btnEng;

    [SerializeField] private Text bgmText;
    [SerializeField] private Text effectText;
    [SerializeField] Button AdButton;
    [SerializeField] private Text adText;
    [SerializeField] private Button adRemoveButton;


    public void btn_BGM()
    {
        if (OptionManager.instance.CompareOption(OptionManager.E_OPTION_TYPE.BGM, OptionManager.E_OPTION_VALUE.ON))
        {
            OptionManager.instance.SetOption(OptionManager.E_OPTION_TYPE.BGM, OptionManager.E_OPTION_VALUE.OFF);
            //bgmText.text = "BGM Off";
            bgmText.gameObject.GetComponent<Langauge>().id = "ui_003";
            m_btnBGM.image.sprite = m_imgOff;

        }
        else
        {
            OptionManager.instance.SetOption(OptionManager.E_OPTION_TYPE.BGM, OptionManager.E_OPTION_VALUE.ON);
            //bgmText.text = "BGM On";
            bgmText.gameObject.GetComponent<Langauge>().id = "ui_002";
            m_btnBGM.image.sprite = m_imgOn;
        }
    }

    public void btn_SFX()
    {
        if (OptionManager.instance.CompareOption(OptionManager.E_OPTION_TYPE.SFX, OptionManager.E_OPTION_VALUE.ON))
        {
            OptionManager.instance.SetOption(OptionManager.E_OPTION_TYPE.SFX, OptionManager.E_OPTION_VALUE.OFF);
            //effectText.text = "SFX Off";
            effectText.gameObject.GetComponent<Langauge>().id = "ui_005";
            m_btnSFX.image.sprite = m_imgOff;
        }
        else
        {
            OptionManager.instance.SetOption(OptionManager.E_OPTION_TYPE.SFX, OptionManager.E_OPTION_VALUE.ON);
            //effectText.text = "SFX On";
            effectText.gameObject.GetComponent<Langauge>().id = "ui_004";
            m_btnSFX.image.sprite = m_imgOn;
        }
    }

    public void btn_Language(OptionManager.E_OPTION_VALUE value)
    {
        OptionManager.instance.SetOption(OptionManager.E_OPTION_TYPE.LANGAUGE, value);
    }

    public void btn_Korean()
    {
        OptionManager.instance.SetOption(OptionManager.E_OPTION_TYPE.LANGAUGE, OptionManager.E_OPTION_VALUE.KOR);
        m_btnKor.image.sprite = m_imgKor_on;
        m_btnEng.image.sprite = m_imgEng_off;
    }
    public void btn_English()
    {
        OptionManager.instance.SetOption(OptionManager.E_OPTION_TYPE.LANGAUGE, OptionManager.E_OPTION_VALUE.ENG);
        m_btnKor.image.sprite = m_imgKor_off;
        m_btnEng.image.sprite = m_imgEng_on;
    }

    public void btn_AdRemove()
    {
        if(!PlayerData.instance.isAdRemoved)
        {
            OptionManager.instance.OptionClose();
        }
    }

    public void UpdateOptionUI()
    {
        if (OptionManager.instance.CompareOption(OptionManager.E_OPTION_TYPE.BGM, OptionManager.E_OPTION_VALUE.ON))
            m_btnBGM.image.sprite = m_imgOn;
        else
            m_btnBGM.image.sprite = m_imgOff;
        if (OptionManager.instance.CompareOption(OptionManager.E_OPTION_TYPE.SFX, OptionManager.E_OPTION_VALUE.ON))
            m_btnSFX.image.sprite = m_imgOn;
        else
            m_btnSFX.image.sprite = m_imgOff;

        if (OptionManager.instance.CompareOption(OptionManager.E_OPTION_TYPE.LANGAUGE, OptionManager.E_OPTION_VALUE.KOR))
        {
            m_btnKor.image.sprite = m_imgKor_on;
            m_btnEng.image.sprite = m_imgEng_off;
        }
        else
        {
            m_btnKor.image.sprite = m_imgKor_off;
            m_btnEng.image.sprite = m_imgEng_on;
        }

        if (PlayerData.instance.isAdRemoved)
            AdButton.interactable = false;
        else
            AdButton.interactable = true;
    }

    public void UpdateOptionUIADRemoveRistner(TitleUIManager titleUI)
    {
        //TitleUIManager titleUI = GameObject.Find("TitleSystem").GetComponent<TitleUIManager>();
        adRemoveButton.onClick.AddListener(titleUI.GoldShopOpen);
    }

    private void Start()
    {
/*        TitleUIManager titleUI = GameObject.Find("TitleSystem").GetComponent<TitleUIManager>();
        UpdateOptionUIADRemoveRistner(titleUI);*/
        UpdateOptionUI();
    }
}

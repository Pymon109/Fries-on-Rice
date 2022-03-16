using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Langauge_IMG : MonoBehaviour
{
    [SerializeField] Sprite[] m_sprites = new Sprite[(int)LanguageData.E_LANGUAGE.MAX_COUNT];

    Image m_img;

    public void SetKorSprite(Sprite sprite)
    {
        m_sprites[(int)LanguageData.E_LANGUAGE.KOR] = sprite;
    }
    public void SetEngSprite(Sprite sprite)
    {
        m_sprites[(int)LanguageData.E_LANGUAGE.ENG] = sprite;
    }

    void UpdateLangauege()
    {
        var value = OptionManager.instance.GetCurrentOptionValue(OptionManager.E_OPTION_TYPE.LANGAUGE);
        switch (value)
        {
            case OptionManager.E_OPTION_VALUE.KOR:
                m_img.sprite = m_sprites[(int)LanguageData.E_LANGUAGE.KOR];
                break;

            case OptionManager.E_OPTION_VALUE.ENG:
                m_img.sprite = m_sprites[(int)LanguageData.E_LANGUAGE.ENG];
                break;
        }
        m_img.SetNativeSize();
    }

    private void Awake()
    {
        m_img = GetComponent<Image>();
    }

    private void Update()
    {
        UpdateLangauege();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_PopUp : MonoBehaviour
{
    [SerializeField] Langauge_IMG m_language_img;
    [SerializeField] Image m_imgFry;
    [SerializeField] Text m_txtFryName;

    [SerializeField] ScreenState m_screenState;

    FryData m_fry;

    public void SetPopUp(FryData fry)
    {
        m_fry = fry;

        m_language_img.SetKorSprite(fry.spriteFryCollectionPopUp_kor);
        m_language_img.SetEngSprite(fry.spriteFryCollectionPopUp_eng);
    }

    public void PopUpActive(bool command)
    {
        gameObject.SetActive(command);
        if (command)
            HardwareInputManager.instance.PushStateStack(m_screenState);
        else
            HardwareInputManager.instance.EscapeCurrentState();
    }
}

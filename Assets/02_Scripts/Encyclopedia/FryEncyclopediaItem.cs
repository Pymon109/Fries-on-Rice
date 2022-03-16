using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FryEncyclopediaItem : MonoBehaviour
{
    [SerializeField] Image m_imgFryImage;

    FryData m_fry;
    public int GetFryID() { return m_fry.iFryID; }

    public void SetItem(FryData fry)
    {
        m_fry = fry;
        m_imgFryImage.sprite = fry.spriteFryImage;
        m_imgFryImage.SetNativeSize();
        UpdateScreen();
    }
    public void UpdateScreen()
    {
        if (PlayerData.instance.GetFryHoldings(m_fry.iFryID))
        {
            m_imgFryImage.sprite = m_fry.spriteFryImage;
            m_imgFryImage.color = Color.white;
            m_imgFryImage.SetNativeSize();
        }
        else
        {
            m_imgFryImage.sprite = m_fry.spriteFryImage;
            m_imgFryImage.color = Color.black;
            m_imgFryImage.SetNativeSize();
        }
    }


    public void ShowPopUp()
    {
        if (PlayerData.instance.GetFryHoldings(m_fry.iFryID))
        {
            GUI_PopUp popUp = transform.parent.parent.parent.GetComponent<GUI_FryEncyclopediaScrollView>().popUp;
            popUp.PopUpActive(true);
            popUp.SetPopUp(m_fry);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FryStoreItem : MonoBehaviour
{
    [SerializeField] Text m_txtFryName;
    [SerializeField] Image m_imgFryImage;

    [SerializeField] Button m_btnBuy;
    [SerializeField] Button m_btnFryIMG;
    [SerializeField] Transform m_trsPrice;
    [SerializeField] Text m_txtPrice;

    [SerializeField] Text m_txtMessage;

    GUI_StorePopUp m_guiPopUp;
    public GUI_StorePopUp guiPopUp { set { m_guiPopUp = value; } }
    FryData m_fry;

    public void SetItem(FryData fry)
    {
        m_fry = fry;
        m_txtFryName.gameObject.GetComponent<Langauge>().id = fry.sNameID;

        if(fry.eCompensation == FryData.E_FRY_COMPENSATION_TYPE.BUY)
        {
            //±¸¸Å·Î È¹µæÇÏ´Â Æ¢±è
            m_imgFryImage.sprite = fry.spriteFryImage;
            m_imgFryImage.SetNativeSize();

            m_btnBuy.gameObject.SetActive(true);
            m_btnBuy.interactable = true;
            m_btnFryIMG.interactable = true;

            m_trsPrice.gameObject.SetActive(true);
            m_txtPrice.text = fry.iPrice.ToString();

            m_txtMessage.gameObject.SetActive(false);
        }
        else
        {
            //±¸¸Å·Î È¹µæ ºÒ°¡ Æ¢±è
            m_imgFryImage.sprite = fry.spriteFryImage;
            m_imgFryImage.color = Color.black;
            m_imgFryImage.SetNativeSize();

            m_btnBuy.gameObject.SetActive(true);
            m_btnBuy.interactable = false;
            m_btnFryIMG.interactable = false;

            m_trsPrice.gameObject.SetActive(false);

            m_txtMessage.gameObject.SetActive(true);
            m_txtMessage.GetComponent<Langauge>().id = fry.sCompensationID;

        }
        UpdateItem();
    }

    public void UpdateItem()
    {
        if (PlayerData.instance.GetFryHoldings(m_fry.iFryID))
        {
            //±¸¸Å ¿Ï·á
            //m_btnBuy.gameObject.SetActive(false);
            m_btnBuy.interactable = false;
            m_btnBuy.image.color = Color.clear;
            m_btnFryIMG.interactable = false;
            m_imgFryImage.color = Color.white;
            m_trsPrice.gameObject.SetActive(false);

            m_txtMessage.gameObject.SetActive(true);
            m_txtMessage.GetComponent<Langauge>().id = "ui_025";
            m_btnBuy.interactable = false;
        }
    }

    public void ButtonOnBuy()
    {
        m_guiPopUp.item = this;
        m_guiPopUp.CheckBuy(m_fry);
/*        if (PlayerData.instance.SpendGold(m_fry.iPrice))
        {
            *//*            PlayerData.instance.SetFryHoldings(m_fry.iFryID);
                        UpdateItem();*//*
            popUp.CheckBuy(m_fry);
        }
        else
        {
            //Debug.Log("°ñµå ºÎÁ· UI¶ç¿ì±â");
            popUp.NotEnoughGold();
        }*/
    }
}

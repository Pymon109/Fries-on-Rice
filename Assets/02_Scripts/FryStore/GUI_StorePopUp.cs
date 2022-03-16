using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI_StorePopUp : MonoBehaviour
{
    [SerializeField] Text m_txtMessage;
    [SerializeField] GameObject m_buttons;
    [SerializeField] Button m_btnOk;
    [SerializeField] Button m_btnCancle;

    [SerializeField] GUI_NotEnoughGold m_gNotEnoughGold;

    [SerializeField] ScreenState m_screenState;

    FryData m_fry;
    FryStoreItem m_item;
    public FryStoreItem item { set { m_item = value; } }

    public void NotEnoughGold()
    {
        /*        gameObject.SetActive(true);
                m_buttons.SetActive(false);
                m_txtMessage.GetComponent<Langauge>().id = "ui_028";*/
        //m_gNotEnoughGold.SetActive();
        m_gNotEnoughGold.gameObject.SetActive(true);
        m_gNotEnoughGold.SetState(GUI_NotEnoughGold.E_STATE.FADE_IN);
        PopUpActive(false);
        //gameObject.SetActive(false);
    }

    public void CheckBuy(FryData fry)
    {
        //gameObject.SetActive(true);
        PopUpActive(true);
        m_fry = fry;
        m_buttons.SetActive(true);
        m_txtMessage.GetComponent<Langauge>().id = "ui_029";
    }

    public void Btn_Buy()
    {
        if (PlayerData.instance.SpendGold(m_fry.iPrice))
        {
            //gameObject.SetActive(false);
            PopUpActive(false);
            PlayerData.instance.SetFryHoldings(m_fry.iFryID);
            m_item.UpdateItem();
        }
        else
            NotEnoughGold();
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

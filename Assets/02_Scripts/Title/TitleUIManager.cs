using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    //[SerializeField] private Text goldText;
    [SerializeField] private Button optionButton;
    [SerializeField] private GameObject goldShop;

    [SerializeField] Text m_txt_debugGoogleEmail;

    // Start is called before the first frame update
    void Start()
    {
        if(optionButton)
        {
            optionButton.onClick.AddListener(OptionManager.instance.OptionOpen);
        }

        OptionManager.instance.optionUIs.GetComponent<OptionUI>().UpdateOptionUIADRemoveRistner(this);
        AdmobManager.instance.ToggleBannerAd(!PlayerData.instance.isAdRemoved);
    }

    // Update is called once per frame
    void Update()
    {
        //goldText.text = PlayerData.instance.playerGold + "";
        //m_txt_debugGoogleEmail.text = GameCenterManager.instance.email;
    }
    public void GoldShopOpen()
    {
        if(!PlayerData.instance.isAdRemoved)
        {
            goldShop.SetActive(true);
            HardwareInputManager.instance.PushStateStack(goldShop.GetComponent<ScreenState>());
        }
    }
}

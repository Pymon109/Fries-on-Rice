using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldShop : MonoBehaviour
{
    [SerializeField] Langauge_IMG m_languageIMG;

    [SerializeField] Sprite m_spriteRemoveADs_kor;
    [SerializeField] Sprite m_spriteRemoveADs_eng;

    [SerializeField] Sprite m_spriteComplete_kor;
    [SerializeField] Sprite m_spriteComplete_eng;

    // 함수 매개변수 money는 IAP 호출기능 추가 고려해서 넣음
    public void BuyGold(int money)
    {
        int plusGold = 0;
        switch(money)
        {
            case 1200:
                plusGold = 20000;
                break;
            case 2500:
                plusGold = 50000;
                break;
            case 3900:
                plusGold = 100000;
                break;
            case 4900:
                plusGold = 150000;
                break;
        }
        PlayerData.instance.AddGold(plusGold);
        List<FryData> iapFries = DataManager.instance.friesData.GetIAPFries();
        for(int i = 0; i < iapFries.Count; i++)
            PlayerData.instance.SetFryHoldings(iapFries[i].iFryID);
    }
    public void BuyPackage(int money)
    {
        if(!PlayerData.instance.isAdRemoved)
        {
            PlayerData.instance.isAdRemoved = true;
            PlayerData.instance.AddGold(25000);

            List<FryData> packageFries = DataManager.instance.friesData.GetPackageFries();
            for (int i = 0; i < packageFries.Count; i++)
                PlayerData.instance.SetFryHoldings(packageFries[i].iFryID);

            List<FryData> iapFries = DataManager.instance.friesData.GetIAPFries();
            for (int i = 0; i < iapFries.Count; i++)
                PlayerData.instance.SetFryHoldings(iapFries[i].iFryID);

            m_languageIMG.SetKorSprite(m_spriteComplete_kor);
            m_languageIMG.SetEngSprite(m_spriteComplete_eng);
        }
    }

    public void GoldShopClose()
    {
        HardwareInputManager.instance.EscapeCurrentState();
    }

    private void Start()
    {
        if (PlayerData.instance.isAdRemoved)
        {
            m_languageIMG.SetKorSprite(m_spriteComplete_kor);
            m_languageIMG.SetEngSprite(m_spriteComplete_eng);
        }
        else
        {
            m_languageIMG.SetKorSprite(m_spriteRemoveADs_kor);
            m_languageIMG.SetEngSprite(m_spriteRemoveADs_eng);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    public void PuchaseComplete()
    {
        List<FryData> iapFries = DataManager.instance.friesData.GetIAPFries();
        for (int i = 0; i < iapFries.Count; i++)
            PlayerData.instance.SetFryHoldings(iapFries[i].iFryID);
    }

    public void BuyPackageComplet()
    {
        List<FryData> packageFries = DataManager.instance.friesData.GetPackageFries();
        for (int i = 0; i < packageFries.Count; i++)
            PlayerData.instance.SetFryHoldings(packageFries[i].iFryID);
    }
}

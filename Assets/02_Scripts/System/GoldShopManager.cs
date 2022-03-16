using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldShopManager : MonoBehaviour
{
    [SerializeField] private Text goldText;
    [SerializeField] private GameObject goldShop;

    void Update()
    {
        goldText.text = PlayerData.instance.playerGold + "";
    }
    public void GoldShopOpen()
    {
        goldShop.SetActive(true);
        HardwareInputManager.instance.PushStateStack(goldShop.GetComponent<ScreenState>());
    }
}

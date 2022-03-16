using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;

public class IAPButton : MonoBehaviour
{
    public IAPButton m_iapButton;

    private void Awake()
    {
        m_iapButton = GetComponent<IAPButton>();
    }
    private void Start()
    {
/*        this.m_iapButton.onPurchaseComplete.AddListener(new UnityEngine.Events.UnityAction<Product>((product) =>
        {
            Debug.Log(string.Format("구매 성공 : "));
        }));*/
    }
}

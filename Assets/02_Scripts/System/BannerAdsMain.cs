using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdsMain : MonoBehaviour, IUnityAdsInitializationListener
{
    static BannerAdsMain unique;

    public string m_sGameID = "4577791";
    public bool m_bIsTestMode = true;

    private void Awake()
    {
        Advertisement.Initialize(m_sGameID, m_bIsTestMode, this);

        if (unique == null)
        {
            unique = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("배너 광고 메인 스크립트 복수 존재");
            Destroy(gameObject);
        }
    }

/*    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Init complete.");
    }

    public void OnInitializationFail(UnityAdsInitializationError error, string message)
    {
        Debug.LogFormat("[Unity Ads Init failed\n error : {0}\n message : {1}", error, message);
    }*/

    void IUnityAdsInitializationListener.OnInitializationComplete()
    {
        Debug.Log("Unity Ads Init complete.");
        throw new System.NotImplementedException();
    }

    void IUnityAdsInitializationListener.OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogFormat("[Unity Ads Init failed\n error : {0}\n message : {1}", error, message);
        throw new System.NotImplementedException();
    }
}

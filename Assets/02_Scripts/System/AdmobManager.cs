using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class AdmobManager : MonoBehaviour
{
    static AdmobManager unique;
    static public AdmobManager instance { get { return unique; } }

    public bool isTestMode;
    public string TestDeviceID;

    public Text m_txtFrontADLoad;
    public Text m_txtBannerADLoad;
    public Text m_txtRewardADLoad;
    public Text m_txtDebugLogText;

    public Button FrontAdsBtn, RewardAdsBtn;

    private void Awake()
    {
        if (unique == null)
        {
            unique = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("�ֵ�� �Ŵ��� ���� ����");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        var requestConfiguration = new RequestConfiguration
           .Builder()
           .SetTestDeviceIds(new List<string>() { TestDeviceID }) // test Device ID
           .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

/*        this.rewardAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardAd.OnAdClosed += HandleRewardedAdClosed;*/

        LoadBannerAd();
        ToggleBannerAd(false);
        LoadFrontAd();
        LoadRewardAd();
    }

    void Update()
    {
        FrontAdsBtn.interactable = frontAd.IsLoaded();
        RewardAdsBtn.interactable = rewardAd.IsLoaded();
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }



    #region ��� ����
    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "ca-app-pub-2920533430330283/9982115468";
    BannerView bannerAd;


    void LoadBannerAd()
    {
        string logText = "��ʱ��� ����";
        bannerAd = new BannerView(isTestMode ? bannerTestID : bannerID,
            AdSize.SmartBanner, AdPosition.Bottom);
        bannerAd.LoadAd(GetAdRequest());
        //ToggleBannerAd(false);
    }

    public void ToggleBannerAd(bool b)
    {
        if (b) bannerAd.Show();
        else bannerAd.Hide();
    }
    #endregion

    #region ���� ����
    const string frontTestID = "ca-app-pub-3940256099942544/8691691433";
    const string frontID = "ca-app-pub-2920533430330283/6777367628";
    InterstitialAd frontAd;


    public void LoadFrontAd()
    {
        string logText = "���鱤�� ����";
        frontAd = new InterstitialAd(isTestMode ? frontTestID : frontID);
        frontAd.LoadAd(GetAdRequest());
        frontAd.OnAdClosed += (sender, e) =>
        {
            logText = "���鱤�� ����";
        };
        m_txtFrontADLoad.text = logText;
    }

    public void ShowFrontAd()
    {
         if (!PlayerData.instance.isAdRemoved)
        {
            frontAd.Show();
            LoadFrontAd();
        }
    }
    #endregion


    #region ������ ����
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardID = "ca-app-pub-2920533430330283/8129977746";
    RewardedAd rewardAd;


    public bool LoadRewardAd()
    {
        bool result = false;
        string logText = "������ ���� ����";

        rewardAd = new RewardedAd(isTestMode ? rewardTestID : rewardID);
        rewardAd.LoadAd(GetAdRequest());
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            logText = "������ ���� ����";
            result = true;
        };

        m_txtRewardADLoad.text = logText;
        return result;
    }

    public bool ShowRewardAd()
    {
        rewardAd.Show();
        return LoadRewardAd();
    }
    public void ShowRewardAd_debug()
    {
        rewardAd.Show();
        LoadRewardAd();
    }

    private void RewardedAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("RewardedAd_OnAdFailedToLoad");
        m_txtDebugLogText.text = "RewardedAd_OnAdFailedToLoad";
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded event received");
        m_txtDebugLogText.text = "HandleRewardedAdLoaded event received";
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening event received");
        m_txtDebugLogText.text = "HandleRewardedAdOpening event received";
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToShow event received with message: " + args.Message);
        m_txtDebugLogText.text = "HandleRewardedAdFailedToShow event received with message: " + args.Message;
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed event received");
        m_txtDebugLogText.text = "HandleRewardedAdClosed event received";
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        Debug.Log("HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type);
        m_txtDebugLogText.text = "HandleRewardedAdRewarded event received for " + amount.ToString() + " " + type;
    }

    #endregion
}
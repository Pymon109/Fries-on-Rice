using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class BannerAds : MonoBehaviour
{
    public string adUnitId = "Banner_Android";
    public Button btnLoad;
    public Button btnShow;
    public Button btnHide;

    void Start()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

        btnShow.interactable = false;
        btnHide.interactable = false;

        this.btnLoad.onClick.AddListener(() =>
        {
            this.Load();
        });

        this.btnShow.onClick.AddListener(() =>
        {
            this.Show();
        });

        this.btnHide.onClick.AddListener(() =>
        {
            this.Hide();
        });

        Load();
        Show();
    }

    private void Load()
    {

        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.Load(adUnitId, options);
    }

    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        btnShow.interactable = true;
        btnHide.interactable = true;
    }

    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }


    void Show()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(adUnitId, options);
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }


    void Hide()
    {
        Advertisement.Banner.Hide();
    }
}


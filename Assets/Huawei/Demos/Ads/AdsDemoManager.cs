using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HuaweiMobileServices.Ads;
using HmsPlugin;
using UnityEngine.UI;
using UnityEngine.Networking;
using HuaweiMobileServices.Base;

public class AdsDemoManager : MonoBehaviour
{
    [SerializeField]
    private Toggle testAdStatusToggle;

    [SerializeField]
    private GameObject nativeAdObj;

    private void Start()
    {
        HMSAdsKitManager.Instance.OnNativeAdLoaded = OnNativeAdLoaded;
        HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
        HMSAdsKitManager.Instance.OnInterstitialAdClosed = OnInterstitialAdClosed;
        testAdStatusToggle.isOn = HMSAdsKitSettings.Instance.Settings.GetBool(HMSAdsKitSettings.UseTestAds);

    }

    public void ShowBannerAd()
    {
        HMSAdsKitManager.Instance.ShowBannerAd();
    }

    public void HideBannerAd()
    {
        HMSAdsKitManager.Instance.HideBannerAd();
    }

    public void ShowRewardedAd()
    {
        Debug.Log("[HMS] AdsDemoManager ShowRewardedAd");
        HMSAdsKitManager.Instance.ShowRewardedAd();
    }

    public void NativeAd()
    {
        HMSAdsKitManager.Instance.LoadNativeAd();
    }

    public void ShowInterstitialAd()
    {
        Debug.Log("[HMS] AdsDemoManager ShowInterstitialAd");
        HMSAdsKitManager.Instance.ShowInterstitialAd();
    }

    public void OnRewarded(Reward reward)
    {
        Debug.Log("[HMS] AdsDemoManager rewarded!");
    }

    public void OnInterstitialAdClosed()
    {
        Debug.Log("[HMS] AdsDemoManager interstitial ad closed");
    }

    public void SetTestAdStatus()
    {
        HMSAdsKitManager.Instance.SetTestAdStatus(testAdStatusToggle.isOn);
        HMSAdsKitManager.Instance.DestroyBannerAd();
        HMSAdsKitManager.Instance.LoadAllAds();
    }

    private void OnNativeAdLoaded()
    {
        nativeAdObj.gameObject.SetActive(true);
        nativeAdObj.transform.Find("Image - NativeAd").GetComponent<RawImage>().texture = HMSAdsKitManager.Instance.GetNativeAdImage();
        nativeAdObj.transform.Find("Text - Title").GetComponent<Text>().text = HMSAdsKitManager.Instance.GetNativeAdTitle();
        nativeAdObj.transform.Find("Text - Ad Source").GetComponent<Text>().text = HMSAdsKitManager.Instance.GetNativeAdSource();
        nativeAdObj.transform.Find("Button - Info").GetComponentInChildren<Text>().text = HMSAdsKitManager.Instance.GetNativeAdButtonInfo();
        nativeAdObj.transform.Find("Button - Info").GetComponent<Button>().onClick.AddListener(() => 
        { 
            Application.OpenURL(HMSAdsKitManager.Instance.GetNativeAdClickUrl());
            HMSAdsKitManager.Instance.RecordNativeAdClick();
        });
    }
}

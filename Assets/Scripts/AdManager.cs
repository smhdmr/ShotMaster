using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    private string adUnitId = "ca-app-pub-5580735773986956/4474925619";
    private InterstitialAd interstitialAd;
    private AdRequest adRequest;

    public int adCounter = 0;


    private void Awake()
    {
        Instance = this;
        MobileAds.Initialize(initStatus => { });
        this.interstitialAd = new InterstitialAd(adUnitId);
        adRequest = new AdRequest.Builder().Build();
        this.interstitialAd.LoadAd(adRequest);
        this.interstitialAd.OnAdClosed += OnAdClosed;
    }
    

    private void OnAdClosed(object sender, EventArgs args)
    {
        interstitialAd.LoadAd(adRequest);
    }


    public void AdCounter()
    {
        adCounter++;

        if(adCounter >= 2 && interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
            adCounter = 0;
        }
    }

}

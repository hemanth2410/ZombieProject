using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
public class InitializedStartAds : MonoBehaviour
{
   // string banner_Ad_Id = "ca-app-pub-3940256099942544/6300978111";
    string banner_Ad_Id = "ca-app-pub-2453689695723299/9592954140";


    //string interstitial_Ad01_Id = "ca-app-pub-2453689695723299/2467420395";
   // string interstitial_Ad01_Id = "ca-app-pub-3940256099942544/1033173712";
    private BannerView bannerView;
   // private InterstitialAd startInterstitialAd;
    public static InitializedStartAds _instance;
    public void Awake()
    {

        if (_instance != null && _instance != this)
        {

            Destroy(gameObject);
            return;
        }


        _instance = this;
     
        DontDestroyOnLoad(gameObject);


    }

    public void Start()
    {
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();

        //this.RequestStartInterstitial();
   
        
    }

    #region BannerAd
    public void RequestBanner()
    {

        
        this.bannerView = new BannerView(banner_Ad_Id, AdSize.Banner, AdPosition.Bottom);
       

    }

    public void ShowBannerAd()
    {

       
        AdRequest request = new AdRequest.Builder().Build();

       
        this.bannerView.LoadAd(request);
       
    }

    #endregion
    //#region StartInterstitialAd
    //public void RequestStartInterstitial()
    //{

    //    // Initialize an InterstitialAd.
    //    this.startInterstitialAd = new InterstitialAd(interstitial_Ad01_Id);

    //    // Called when an ad request has successfully loaded.
    //    this.startInterstitialAd.OnAdLoaded += HandleOnStartAdLoaded;
    //    // Called when an ad request failed to load.
    //    this.startInterstitialAd.OnAdFailedToLoad += HandleOnStartAdFailedToLoad;
    //    // Called when an ad is shown.
    //    this.startInterstitialAd.OnAdOpening += HandleOnStartAdOpened;
    //    // Called when the ad is closed.
    //    this.startInterstitialAd.OnAdClosed += HandleOnStartAdClosed;
    //    // Called when the ad click caused the user to leave the application.
    //    this.startInterstitialAd.OnAdLeavingApplication += HandleOnStartAdLeavingApplication;

    //    // Create an empty ad request.
    //    AdRequest request = new AdRequest.Builder().Build();

    //    // Create an empty ad request with testDevice.
    //    //AdRequest request = new AdRequest.Builder()
    //    //    .AddTestDevice("330C015351C7944872F31984DE11DE9F")
    //    //    .Build();

    //    // Load the interstitial with the request.
    //    this.startInterstitialAd.LoadAd(request);
      
    //}
    //public void ShowStartInterstitialAd()
    //{
    //    if (this.startInterstitialAd.IsLoaded())
    //    {
    //        this.startInterstitialAd.Show();
           
    //    }

    //}


    ////FOR EVENTS AND DELEGATE interstitial ads
    //public void HandleOnStartAdLoaded(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdLoaded event received");
    //}

    //public void HandleOnStartAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //    this.RequestStartInterstitial();
    //}

    //public void HandleOnStartAdOpened(object sender, EventArgs args)
    //{
    //    AudioManager.current.Mute();


    //}

    //public void HandleOnStartAdClosed(object sender, EventArgs args)
    //{
    //    this.RequestStartInterstitial();
    //    AudioManager.current.UpdateSound();
    //}

    //public void HandleOnStartAdLeavingApplication(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdLeavingApplication event received");
    //}
    //#endregion


}

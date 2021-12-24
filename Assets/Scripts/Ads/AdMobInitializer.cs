using UnityEngine;
using System;
using GoogleMobileAds.Api;
using TMPro;

public class AdMobInitializer : MonoBehaviour
{

   string interstitial_Ad01_Id = "ca-app-pub-2453689695723299/1522892437";
    //string interstitial_Ad01_Id = "ca-app-pub-3940256099942544/1033173712";
  
    string rewarded_Ad01_Id = "ca-app-pub-2453689695723299/8120375786";
    //string rewarded_Ad01_Id = "ca-app-pub-3940256099942544/5224354917";



    //string rewarded_Ad02_Id = "ca-app-pub-2453689695723299/2996590351";

    //string rewarded_Ad02_Id = "ca-app-pub-3940256099942544/5224354917";

    //private BannerView bannerView;
    private InterstitialAd gameInterstitialAd;
    //private InterstitialAd startInterstitialAd;
    
    private RewardedAd shopRewardedAd;
   // private RewardedAd gameRewardedAd;
 
    public static AdMobInitializer _instance;
    private GameManager gmInst;

    //[HideInInspector]public bool gameAdSuccess;
  


    public void Awake()
    {
      
        if (_instance != null && _instance != this)
        {

            Destroy(gameObject);
            return;
        }


        _instance = this;
        gmInst = GameManager.current;
   
        DontDestroyOnLoad(gameObject);


    }

    public void Start()
    {
      //  MobileAds.Initialize(initStatus => { });

      //  this.RequestBanner();
     
        this.RequestGameInterstitial();
      //this.RequestStartInterstitial();      
        this.RequestShopRewardedAd();
        //this.RequestGameRewardedAd();
        //this.ShowBannerAd();
        InitializedStartAds._instance.ShowBannerAd();
       // InitializedStartAds._instance.ShowStartInterstitialAd();
       
    }

    //#region BannerAd
    //public void RequestBanner()
    //{

    //    // Create a 320x50 banner at the top of the screen.
    //    this.bannerView = new BannerView(banner_Ad_Id, AdSize.Banner, AdPosition.Bottom);
    //    t.text = "requesting beanner";
    //    //// Called when an ad request has successfully loaded.
    //    //this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
    //    //// Called when an ad request failed to load.
    //    //this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
    //    //// Called when an ad is clicked.
    //    //this.bannerView.OnAdOpening += this.HandleOnAdOpened;
    //    //// Called when the user returned from the app after an ad click.
    //    //this.bannerView.OnAdClosed += this.HandleOnAdClosed;
    //    //// Called when the ad click caused the user to leave the application.
    //    //this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

    //}

    //public void ShowBannerAd()
    //{
        
    //    // Create an empty ad request.
    //    AdRequest request = new AdRequest.Builder().Build();
        
    //        // Load the banner with the request.
    //        this.bannerView.LoadAd(request);
    //    t2.text = "showing beanner";
    //}

    //#endregion

    #region GameInterstitialAd
    public void RequestGameInterstitial()
    {

        // Initialize an InterstitialAd.
        this.gameInterstitialAd = new InterstitialAd(interstitial_Ad01_Id);

        // Called when an ad request has successfully loaded.
        this.gameInterstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.gameInterstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.gameInterstitialAd.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.gameInterstitialAd.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.gameInterstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Create an empty ad request with testDevice.
        //AdRequest request = new AdRequest.Builder()
        //    .AddTestDevice("330C015351C7944872F31984DE11DE9F")
        //    .Build();

        // Load the interstitial with the request.
        this.gameInterstitialAd.LoadAd(request);
    }
    public void ShowGameInterstitialAd()
    {
        if (this.gameInterstitialAd.IsLoaded())
        {
            this.gameInterstitialAd.Show();
        }
      
    }


    //FOR EVENTS AND DELEGATE interstitial ads
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        this.RequestGameInterstitial();
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        AudioManager.current.Mute();


    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        this.RequestGameInterstitial();
        AudioManager.current.UpdateSound();
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
    #endregion
    //#region StartInterstitialAd
    //public void RequestStartInterstitial()
    //{

    //    // Initialize an InterstitialAd.
    //    this.startInterstitialAd = new InterstitialAd(interstitial_Ad02_Id);

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
    //    t3.text = "request and load start IAD";
    //}
    //public void ShowStartInterstitialAd()
    //{
    //    if (this.startInterstitialAd.IsLoaded())
    //    {
    //        this.startInterstitialAd.Show();
    //        t4.text = "showing start IAD";
    //    }

    //}


    ////FOR EVENTS AND DELEGATE interstitial ads
    //public void HandleOnStartAdLoaded(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdLoaded event received");
    //}

    //public void HandleOnStartAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //    this.RequestGameInterstitial();
    //}

    //public void HandleOnStartAdOpened(object sender, EventArgs args)
    //{
    //    AudioManager.current.Mute();


    //}

    //public void HandleOnStartAdClosed(object sender, EventArgs args)
    //{
    //    this.RequestGameInterstitial();
    //    AudioManager.current.UpdateSound();
    //}

    //public void HandleOnStartAdLeavingApplication(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAdLeavingApplication event received");
    //}
    //#endregion
    
    #region OldRewardedAdWorking
    public void RequestShopRewardedAd()
    {
        this.shopRewardedAd = new RewardedAd(rewarded_Ad01_Id);

        // Called when an ad request has successfully loaded.
        this.shopRewardedAd.OnAdLoaded += HandleShopRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.shopRewardedAd.OnAdFailedToLoad += HandleShopRewardedAdFailedToLoad;
        // Called shopRewardedAd an ad is shown.
        this.shopRewardedAd.OnAdOpening += HandleShopRewardedAdOpening;
        // Called when an ad request failed to show.
        this.shopRewardedAd.OnAdFailedToShow += HandleShopRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.shopRewardedAd.OnUserEarnedReward += HandleUserEarnedShopReward;
        // Called when the ad is closed.
        this.shopRewardedAd.OnAdClosed += HandleShopRewardedAdClosed;


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.shopRewardedAd.LoadAd(request);
    }





    //public void RequestGameRewardedAd()
    //{
    //    gameAdSuccess = false;
    //    this.gameRewardedAd = new RewardedAd(rewarded_Ad02_Id);

    //    // Called when an ad request has successfully loaded.
    //    this.gameRewardedAd.OnAdLoaded += HandleGameRewardedAdLoaded;
    //    // Called when an ad request failed to load.
    //    this.gameRewardedAd.OnAdFailedToLoad += HandleGameRewardedAdFailedToLoad;
    //    // Called shopRewardedAd an ad is shown.
    //    this.gameRewardedAd.OnAdOpening += HandleGameRewardedAdOpening;
    //    // Called when an ad request failed to show.
    //    this.gameRewardedAd.OnAdFailedToShow += HandleGameRewardedAdFailedToShow;
    //    // Called when the user should be rewarded for interacting with the ad.
    //    this.gameRewardedAd.OnUserEarnedReward += HandleUserEarnedGameReward;
    //    // Called when the ad is closed.
    //    this.gameRewardedAd.OnAdClosed += HandleGameRewardedAdClosed;


    //    // Create an empty ad request.
    //    AdRequest request = new AdRequest.Builder().Build();
    //    // Load the rewarded ad with the request.
    //    this.gameRewardedAd.LoadAd(request);
    //}

   // EVENTS FOR SHOP REWARDED ADS   ------------- E - V - E - N - T - S -----------

    public void HandleShopRewardedAdLoaded(object sender, EventArgs args)
    {

        MonoBehaviour.print("HandleRewardedAdLoaded event received");


    }

    public void HandleShopRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
       
        this.RequestShopRewardedAd();

    }

    public void HandleShopRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAd OpenAd event received");
        AudioManager.current.Mute();
    }

    public void HandleShopRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {

        MonoBehaviour.print("FailedtoShow event received");
    }

    public void HandleShopRewardedAdClosed(object sender, EventArgs args)
    {

        this.RequestShopRewardedAd();
        AudioManager.current.UpdateSound();
    }


    public void HandleUserEarnedShopReward(object sender, Reward args)
    {

        gmInst.AddCoins(200);
        GameEvents.current.SaveGame();
        this.RequestShopRewardedAd();


    }


    // EVENTS FOR GAME REWARDED ADS

    //public void HandleGameRewardedAdLoaded(object sender, EventArgs args)
    //{

    //    MonoBehaviour.print("HandleRewardedAdLoaded event received");


    //}

    //public void HandleGameRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    //{
        
    //    this.RequestGameRewardedAd();

    //}

    //public void HandleGameRewardedAdOpening(object sender, EventArgs args)
    //{
    //    MonoBehaviour.print("HandleAd OpenAd event received");
    //    AudioManager.current.Mute();
    //}

    //public void HandleGameRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    //{

    //    MonoBehaviour.print("FailedtoShow event received");
    //}

    //public void HandleGameRewardedAdClosed(object sender, EventArgs args)
    //{

    //    this.RequestGameRewardedAd();
    //    AudioManager.current.UpdateSound();
    //}


    //public void HandleUserEarnedGameReward(object sender, Reward args)
    //{

       
    //    gmInst.AddCoins(LevelManager.current.totalLevelCoinsGained);

    //    GameEvents.current.GameRewardSuccess();

    //    this.RequestGameRewardedAd();





    //}


   //  if user chose to watch rewarded add
    //public void ShowGameRewardedAd()
    //{
       
    //    if (this.gameRewardedAd.IsLoaded())
    //    {
    //        this.gameRewardedAd.Show();

    //    }

        

    //}
   //  if user chose to watch rewarded add
    public void ShowShopRewardedAd()
    {
        if (this.shopRewardedAd.IsLoaded())
        {
            this.shopRewardedAd.Show();

        }

    }


    #endregion

}

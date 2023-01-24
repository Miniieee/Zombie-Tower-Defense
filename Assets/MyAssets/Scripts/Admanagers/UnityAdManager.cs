using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Audio;

public class UnityAdManager : MonoBehaviour, IUnityAdsListener
{
    private string appstoreID = "3872914";
    private string playstoreID = "3872915";

    private string interstitialAd = "video";
    private string rewardedVideoAd = "rewardedVideo";
    private string bannerAD = "ZombieBanner";

    [Header("Attributes")]
    public bool isTargetPlayStore;
    public bool isTestAd;
    public bool isMenu;

    [Header("Linked objects")]
    [SerializeField] Button BtnReward;
    [SerializeField] GameObject nextLevel;
    [SerializeField] GameOver gameOver;
    [SerializeField] AudioMixer audioMixer;

    void Start()
    {
        ShowBannerAd();
        Advertisement.AddListener(this);
        InitializeAdvertisement();
        if (isMenu)
        {
            StartCoroutine(PlayBannerAd());
        }
    }

    private void InitializeAdvertisement()
    {
        if (isTargetPlayStore)
        {
            Advertisement.Initialize(playstoreID, isTestAd);
            return;
        }
        Advertisement.Initialize(appstoreID, isTestAd);
    }

    public void PlayInterstitialAd()
    {
        if (!Advertisement.IsReady(interstitialAd))
        {
            return;
        }
        Advertisement.Show(interstitialAd);
        nextLevel.SetActive(false);
    }

    public void PlayRewardedVideoAd()
    {
        if (!Advertisement.IsReady(rewardedVideoAd))
        {
            return;
        }
        BtnReward.interactable = false;
        Advertisement.Show(rewardedVideoAd);
    }

    IEnumerator PlayBannerAd()
    {
        while (!Advertisement.IsReady(bannerAD))
        {
            yield return null;
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerAD);
    }

    public void HideBannerAd()
    {
        Advertisement.Banner.Hide(true);
    }

    public void ShowBannerAd()
    {
        Advertisement.Banner.Hide(false);
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        BtnReward.interactable = true;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        audioMixer.SetFloat("MasterVolume", 0f);
        BtnReward.interactable = true;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        audioMixer.SetFloat("MasterVolume", 1f);
        BtnReward.interactable = true;

        switch (showResult)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Finished:
                if (placementId == rewardedVideoAd)
                {
                    gameOver.ExtraCoins();
                }
                break;
        }
    }
}

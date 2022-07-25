using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace InternalAssets.Scripts.ClickerGame.ADDS
{
    public class InterstitialAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string androidAdID = "Interstitial_Android";
        [SerializeField] private string iOSAdID = "Interstitial_iOS";
        private string _adID;

        private void Awake()
        {
            _adID = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOSAdID
                : androidAdID;
        }

        private void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adID);
            Advertisement.Load(_adID, this);
        }
        
        public void ShowAd()
        {
            Debug.Log("Loading Ad: " + _adID);
            Advertisement.Show(_adID, this);
        }

        
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Interstitial Ads Loaded.");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("Interstitial Ads start.");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            LoadAd();
        }
    }
}

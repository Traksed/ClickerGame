using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace InternalAssets.Scripts.ClickerGame.ADDS
{
    public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private Button showAdButton;
        [SerializeField] private string androidAdID = "Rewarded_Android";
        [SerializeField] private string iOSAdID = "Rewarded_iOS";
        private string adID;
        
        private void Awake()
        {
            adID = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOSAdID
                : androidAdID;
            //showAdButton.interactable = false;
        }

        private void Start()
        {
            LoadAd();
        }

        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + adID);
            Advertisement.Load(adID, this);
        }
        
        public void ShowAd()
        {
            Advertisement.Show(adID, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitID)
        {
            Debug.Log("Rewarded Ads Loaded.");

            if (adUnitID.Equals(adID))
            {
                showAdButton.onClick.AddListener(ShowAd);

                showAdButton.interactable = true;
            }
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log("Rewarded Ads Start.");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            
        }

        public void OnUnityAdsShowComplete(string adUnitID, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitID.Equals(adID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Your reward is Nothing");
                MainMenu.Bonus = 2;
            }
        }
    }
}

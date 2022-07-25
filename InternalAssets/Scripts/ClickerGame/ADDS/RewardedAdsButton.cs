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
        private string _adID;
        
        private void Awake()
        {
            _adID = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOSAdID
                : androidAdID;
        }

        private void Start()
        {
            LoadAd();
        }

        private void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adID);
            Advertisement.Load(_adID, this);
        }
        
        public void ShowAd()
        {
            Advertisement.Show(_adID, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitID)
        {
            Debug.Log("Rewarded Ads Loaded.");

            if (adUnitID.Equals(_adID))
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
            if (!adUnitID.Equals(_adID) || !showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)) return;
            Debug.Log("Your reward is x2");
            MainMenu.Bonus = 2;
            MainMenu.BonusTimer = 60;
        }
    }
}

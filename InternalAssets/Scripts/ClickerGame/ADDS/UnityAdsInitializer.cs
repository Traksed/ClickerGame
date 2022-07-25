using UnityEngine;
using UnityEngine.Advertisements;

namespace InternalAssets.Scripts.ClickerGame.ADDS
{
    public class UnityAdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] private string androidGameID = "4848673";
        [SerializeField] private string iOSGameID = "4848672";
        [SerializeField] private bool testMode = true;
        private string _gameID;
        private IUnityAdsInitializationListener _unityAdsInitializationListenerImplementation;

        private void Awake()
        {
            InitializeAds();
            
        }

        private void InitializeAds()
        {
            _gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSGameID : androidGameID;
            Advertisement.Initialize(_gameID, !testMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads Initialization Complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}

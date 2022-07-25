using UnityEngine;
using UnityEngine.Advertisements;

namespace InternalAssets.Scripts.ClickerGame.ADDS
{
    public class BannerAds : MonoBehaviour
    {
        [SerializeField] BannerPosition bannerPosition;
        [SerializeField] private string androidAdID = "Banner_Android";
        [SerializeField] private string iOSAdID = "Banner_iOS";
        private string _adID;
        
        private void Awake()
        {
            _adID = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOSAdID
                : androidAdID;
        }
        
        private void Start()
        {
            Advertisement.Banner.SetPosition(bannerPosition);
            LoadBanner();
        }

        public void LoadBanner()
        {
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };
            
            Advertisement.Banner.Load(_adID, options);
        }

        private void OnBannerLoaded()
        {
            
            Debug.Log("Banner is loaded");
            ShowBannerAd();

        }
        private void OnBannerError(string message)
        {
            
            Debug.Log($"Banner Error: {message}");
            ShowBannerAd();

        }

        private void ShowBannerAd()
        {

            BannerOptions options = new BannerOptions
            {

                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHided,
                showCallback = OnBannerShown
            };
            Advertisement.Banner.Show(_adID,options);
        }

        private void OnBannerClicked()
        {
            
        }
        
        private void OnBannerHided()
        {
            
        }
        
        private void OnBannerShown()
        {
            
        }
        
    }
}

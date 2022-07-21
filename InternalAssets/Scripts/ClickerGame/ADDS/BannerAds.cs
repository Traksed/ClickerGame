using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace InternalAssets.Scripts.ClickerGame.ADDS
{
    public class BannerAds : MonoBehaviour
    {
        [SerializeField] BannerPosition bannerPosition;
        [SerializeField] private string androidAdID = "Banner_Android";
        [SerializeField] private string iOSAdID = "Banner_iOS";
        private string adID;
        
        private void Awake()
        {
            adID = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? iOSAdID
                : androidAdID;
        }
        
        private void Start()
        {
            Advertisement.Banner.SetPosition(bannerPosition);
            LoadBanner();
            StartCoroutine(LoadAdBanner());
        }

        private IEnumerator LoadAdBanner()
        {
            yield return new WaitForSeconds(1f);
            LoadBanner();
        }

        public void LoadBanner()
        {
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };
            
            Advertisement.Banner.Load(adID, options);
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

        public void ShowBannerAd()
        {

            BannerOptions options = new BannerOptions
            {

                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHided,
                showCallback = OnBannerShown
            };
            Advertisement.Banner.Show(adID,options);
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

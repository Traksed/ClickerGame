using UnityEngine;
using UnityEngine.UI;

namespace InternalAssets.Scripts.ClickerGame.Main
{
    public class SoundController : MonoBehaviour
    {
        private AudioSource _audio;
        [SerializeField]private AudioSource musicSfx;
        [SerializeField] private AudioSource soundSfx;
        public AudioClip clickSound;
        public AudioClip[] sounds;
        [SerializeField] private Sprite[] settingButtonSprites;
        [SerializeField] private Image soundButton;
        [SerializeField] private Image musicButton;

        public void OnClickMusicButton()
        {
            if (musicSfx.volume >= 0.1f)
            {
                musicSfx.volume = 0;
                musicButton.sprite = settingButtonSprites[0];
            }
            else
            {
                musicSfx.volume = 0.8f;
                musicButton.sprite = settingButtonSprites[1];
            }
        }
        
        public void OnClickSoundButton()
        {
            if (soundSfx.volume >= 0.1f)
            {
                soundSfx.volume = 0;
                soundButton.sprite = settingButtonSprites[2];
            }
            else
            {
                soundSfx.volume = 0.8f;
                soundButton.sprite = settingButtonSprites[3];
            }
        }

        private void Start()
        {
            _audio = GetComponent<AudioSource>();
            
        }

        public void OnClickSound()
        {
            _audio.PlayOneShot(clickSound);
        }
        
        public void OnAchievementSound()
        {
            _audio.PlayOneShot(sounds[0]);
        }
        
        public void OnUpgradeSound()
        {
            _audio.PlayOneShot(sounds[1]);
        }
        
        public void OnFarmtSound()
        {
            _audio.PlayOneShot(sounds[2]);
        }
        
        public void OnMenuSound()
        {
            _audio.PlayOneShot(sounds[3]);
        }
        
        public void OnOtherSound()
        {
            _audio.PlayOneShot(sounds[4]);
        }
    }
}

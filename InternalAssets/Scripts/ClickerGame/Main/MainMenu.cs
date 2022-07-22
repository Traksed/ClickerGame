using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Threading;
using InternalAssets.Scripts.ClickerGame.ADDS;

namespace InternalAssets.Scripts.ClickerGame
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]private int _score, _totalScore, _passIncome, _clickIncome, _clickScore;
        [SerializeField] private TMP_Text[] scoreText;
        [SerializeField] private TMP_Text[] achievCollectText;
        [SerializeField] private GameObject[] objects;
        [SerializeField] private bool[] isAchiev;
        [SerializeField] private Button[] achievements;
        [SerializeField] private Button[] farms;
        [SerializeField] private Button[] upgrades;
        private TimeSpan _ts;
        [SerializeField] private Image skinButton;
        [SerializeField] private Sprite[] skinPool;
        //public RewardedAdsButton rAdButton;
        private int _skinNumber;
        private Save sv = new();
        //public InterstitialAds ad;
        public  static int Bonus = 1;
        public static float BonusTimer; 
        [SerializeField] private TMP_Text timerText;



        private void Awake()
        {

            if (PlayerPrefs.HasKey("SAVE"))
            {
                sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SAVE"));
                _score = sv.score;
                _clickScore = sv.clickScore;
                _clickIncome = sv.clickIncome;
                _totalScore = sv.totalScore;
                _passIncome = sv.passIncome;
                _skinNumber = sv.skinNumber;
                for (int i = 0; i < 16; i++)
                {
                    isAchiev[i] = sv.isAchievements[i];
                }
            }
            else
            {
                sv = new Save();
                _score = sv.score;
                _clickScore = sv.clickScore;
                _clickIncome = sv.clickIncome;
                _totalScore = sv.totalScore;
                _passIncome = sv.passIncome;
                _skinNumber = sv.skinNumber;
            }

            if (PlayerPrefs.HasKey("LastSession"))
            {
                _ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));
                _score += (int)_ts.TotalSeconds * _passIncome;
                _totalScore += (int)_ts.TotalSeconds * _passIncome;
                
            }
        }

        private void Start()
        {
            for (int i = 0; i < 16; i++)
            {
                if (isAchiev[i])
                {
                    achievCollectText[i].text = "Collected";
                }
            }
            
            skinButton.sprite = skinPool[_skinNumber];
            StartCoroutine(FarmCoroutine());
            //rAdButton.LoadAd(); 
        }
        
        private void Update()
        {
            

            if (_score >= 10000)
            {
                scoreText[0].text = _score/1000 + "k Coins";
            }
            else
            {
                scoreText[0].text = _score + " Coins";
            }
            
            if (_clickIncome >= 10000)
            {
                scoreText[1].text = _clickIncome/1000 + "k per click";
            }
            else
            {
                scoreText[1].text = _clickIncome + " per click";
            }

            scoreText[2].text = _passIncome + "/sec";
            
            if (BonusTimer > 0)
            {
                BonusTimer -= 1 * Time.deltaTime;
                timerText.text = BonusTimer.ToString("0") + "S";
            }
            else
            {
                if (Bonus <= 1) return;
                Bonus = 1;
            }


        }
        
        // Main Buttons
        // -------------------------------------------------------------------------------------------------------
        
        public void SaveButton()
        {
            sv.score = _score;
            sv.clickIncome = _clickIncome;
            sv.clickScore = _clickScore;
            sv.passIncome = _passIncome;
            sv.totalScore = _totalScore;
            sv.skinNumber = _skinNumber;
            sv.isAchievements = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                sv.isAchievements[i] = isAchiev[i];
            }
            PlayerPrefs.SetString("SAVE", JsonUtility.ToJson(sv));
            PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
        }
        
        public void SettingsButton()
        {
            objects[0].SetActive(false);
            objects[1].SetActive(false);
            objects[5].SetActive(true);
            //ad.ShowAd();
        }
        
        public void QuitButton()
        {
            Application.Quit();
        }
        
        public void BackButton()
        {
            objects[0].SetActive(true);
            objects[1].SetActive(true);
            objects[2].SetActive(false);
            objects[3].SetActive(false);
            objects[4].SetActive(false);
            objects[5].SetActive(false);
        }

        public void AchievButton()
        {
            GetScoreAchievements();
            GetClickAchievements();
            GetUpgradeAchievements();
            GetFarmAchievements();
            objects[0].SetActive(false);
            objects[1].SetActive(false);
            objects[2].SetActive(true);
            
        }
    
        public void UpgradeButton()
        {
            GetUpgrades();
            objects[0].SetActive(false);
            objects[1].SetActive(false);
            objects[3].SetActive(true);
        }
    
        public void FarmButton()
        {
            GetFarms();
            objects[0].SetActive(false);
            objects[1].SetActive(false);
            objects[4].SetActive(true);
        }
        
        public void ClickButton()
        {
            _clickScore++;
            _score += _clickIncome * Bonus;
            _totalScore+= _clickIncome * Bonus;
            
        }
        
        // -------------------------------------------------------------------------------------------------------
        
        // Achievement Panel Vision
        // -------------------------------------------------------------------------------------------------------
        private void GetScoreAchievements()
        {
            if (_totalScore >= 1000 && !isAchiev[0])
            {
                achievements[0].interactable = true;
            }
           

            if (_totalScore >= 10000 && !isAchiev[1])
            {
                achievements[1].interactable = true;
            }
           
            
            if (_totalScore >= 250000 && !isAchiev[2])
            {
                achievements[2].interactable = true;
            }
            
            
            
            if (_totalScore >= 1000000 && !isAchiev[3])
            {
                achievements[3].interactable = true;
            }
            
        }
        
        private void GetUpgradeAchievements()
        {
            if (_clickIncome >= 10 && !isAchiev[4])
            {
                achievements[4].interactable = true;
            }
            
            
            if (_clickIncome >= 1000 && !isAchiev[5])
            {
                achievements[5].interactable = true;
            }
            
            
            if (_clickIncome >= 10000 && !isAchiev[6])
            {
                achievements[6].interactable = true;
            }
           
            
            if (_clickIncome >= 100000 && !isAchiev[7])
            {
                achievements[7].interactable = true;
            }
        }

        private void GetClickAchievements()
        {
            if (_clickScore >= 100 && !isAchiev[8])
            {
                achievements[8].interactable = true;
            }
           

            if (_clickScore >= 500 && !isAchiev[9])
            {
                achievements[9].interactable = true;
            }
           

            if (_clickScore >= 2500 && !isAchiev[10])
            {
                achievements[10].interactable = true;
            }
           

            if (_clickScore >= 10000 && !isAchiev[11])
            {
                achievements[11].interactable = true;
            }
        }
        
        private void GetFarmAchievements()
        {
            if (_passIncome >= 10 && !isAchiev[12])
            {
                achievements[12].interactable = true;
            }
           
            if (_passIncome >= 100 && !isAchiev[13])
            {
                achievements[13].interactable = true;
            }

            if (_passIncome >= 1000 && !isAchiev[14])
            {
                achievements[14].interactable = true;
            }
            
            if (_passIncome >= 5000 && !isAchiev[15])
            {
                achievements[15].interactable = true;
            }
        }
        // Achievement Panel Functions
        // -------------------------------------------------------------------------------------------------------
        
        // Total Score Achievements
        public void GetAchiev1()
        {
            isAchiev[0] = !isAchiev[0];
            achievements[0].interactable = false;
            achievCollectText[0].text = "Collected";
            skinButton.sprite = skinPool[1];
            _skinNumber = 1;

        }
        
        public void GetAchiev2()
        {
            isAchiev[1] = !isAchiev[1];
            achievements[1].interactable = false;
            achievCollectText[1].text = "Collected";
            skinButton.sprite = skinPool[2];
            _skinNumber = 2;

        }
        
        public void GetAchiev3()
        {
            isAchiev[2] = !isAchiev[2];
            achievements[2].interactable = false;
            achievCollectText[2].text = "Collected";
            skinButton.sprite = skinPool[3];
            _skinNumber = 3;

        }
        
        public void GetAchiev4()
        {
            isAchiev[3] = !isAchiev[3];
            achievements[3].interactable = false;
            achievCollectText[3].text = "Collected";
            skinButton.sprite = skinPool[4];
            _skinNumber = 4;

        }
        
        // Upgrade Achievements
        public void GetAchiev5()
        {
            isAchiev[4] = !isAchiev[4];
            achievements[4].interactable = false;
            achievCollectText[4].text = "Collected";
            upgrades[1].interactable = true;
            upgrades[2].interactable = true;
            upgrades[3].interactable = true;
            upgrades[4].interactable = true;

        }
        
        public void GetAchiev6()
        {
            isAchiev[5] = !isAchiev[5];
            achievements[5].interactable = false;
            achievCollectText[5].text = "Collected";
            upgrades[5].interactable = true;
            upgrades[6].interactable = true;
            upgrades[7].interactable = true;
            upgrades[8].interactable = true;

        }
        
        public void GetAchiev7()
        {
            isAchiev[6] = !isAchiev[6];
            achievements[6].interactable = false;
            achievCollectText[6].text = "Collected";
            upgrades[9].interactable = true;
            upgrades[10].interactable = true;

        }
        
        public void GetAchiev8()
        {
            isAchiev[7] = !isAchiev[7];
            achievements[7].interactable = false;
            achievCollectText[7].text = "Collected";
            //Honor.

        }
        
        // Total Click Achievements
        public void GetAchiev9()
        {
            isAchiev[8] = !isAchiev[8];
            achievements[8].interactable = false;
            achievCollectText[8].text = "Collected";
            _score += 200;
            

        }
        
        public void GetAchiev10()
        {
            isAchiev[9] = !isAchiev[9];
            achievements[9].interactable = false;
            achievCollectText[9].text = "Collected";
            _score += 2500;

        }
        
        public void GetAchiev11()
        {
            isAchiev[10] = !isAchiev[10];
            achievements[10].interactable = false;
            achievCollectText[10].text = "Collected";
            _score += 50000;

        }
        
        public void GetAchiev12()
        {
            isAchiev[11] = !isAchiev[11];
            achievements[11].interactable = false;
            achievCollectText[11].text = "Collected";
            //My Respect.

        }
        
        // Farm Achievements
        public void GetAchiev13()
        {
            isAchiev[12] = !isAchiev[12];
            achievements[12].interactable = false;
            achievCollectText[12].text = "Collected";
            farms[1].interactable = true;
            farms[2].interactable = true;
            farms[3].interactable = true;

        }
        
        public void GetAchiev14()
        {
            isAchiev[13] = !isAchiev[13];
            achievements[13].interactable = false;
            achievCollectText[13].text = "Collected";
            farms[4].interactable = true;
            farms[5].interactable = true;
            farms[6].interactable = true;

        }
        
        public void GetAchiev15()
        {
            isAchiev[14] = !isAchiev[14];
            achievements[14].interactable = false;
            achievCollectText[14].text = "Collected";
            farms[7].interactable = true;

        }
        
        public void GetAchiev16()
        {
            isAchiev[15] = !isAchiev[15];
            achievements[15].interactable = false;
            achievCollectText[15].text = "Collected";
            //Awesome!

        }
        
        // -------------------------------------------------------------------------------------------------------
        
        // Upgrades Panel Vision
        // -------------------------------------------------------------------------------------------------------
        private void GetUpgrades()
        {
            if (isAchiev[4])
            {
                upgrades[1].interactable = true;
                upgrades[2].interactable = true;
                upgrades[3].interactable = true;
                upgrades[4].interactable = true;
            }
            
            if (isAchiev[5])
            {
                upgrades[5].interactable = true;
                upgrades[6].interactable = true;
                upgrades[7].interactable = true;
                upgrades[8].interactable = true;
            }

            if (!isAchiev[6]) return;
            upgrades[9].interactable = true;
            upgrades[10].interactable = true;
            

        }
        
        // -------------------------------------------------------------------------------------------------------
        
        
        // Upgrades Panel Functions
        // -------------------------------------------------------------------------------------------------------

        public void GetUpgrade1()
        {
            if (_score < 100) return;
            _score -= 100;
            _clickIncome++;

        }
        
        public void GetUpgrade2()
        {
            if (_score < 500) return;
            _score -= 500;
            _clickIncome += 5;

        }
        
        public void GetUpgrade3()
        {
            if (_score < 1000) return;
            _score -= 1000;
            _clickIncome += 10;

        }
        
        public void GetUpgrade4()
        {
            if (_score < 2500) return;
            _score -= 2500;
            _clickIncome += 25;

        }
        
        public void GetUpgrade5()
        {
            if (_score < 10000) return;
            _score -= 10000;
            _clickIncome += 100;

        }
        
        public void GetUpgrade6()
        {
            if (_score < 25000) return;
            _score -= 25000;
            _clickIncome += 250;

        }
        
        public void GetUpgrade7()
        {
            if (_score < 50000) return;
            _score -= 50000;
            _clickIncome += 500;

        }
        
        public void GetUpgrade8()
        {
            if (_score < 100000) return;
            _score -= 100000;
            _clickIncome += 1000;

        }
        
        public void GetUpgrade9()
        {
            if (_score < 200000) return;
            _score -= 200000;
            _clickIncome += 2000;

        }
        
        public void GetUpgrade10()
        {
            if (_score < 350000) return;
            _score -= 350000;
            _clickIncome += 3500;

        }
        
        public void GetUpgrade11()
        {
            if (_score < 500000) return;
            _score -= 500000;
            _clickIncome += 5000;

        }
        // -------------------------------------------------------------------------------------------------------

        
        // Farm Panel Vision
        // -------------------------------------------------------------------------------------------------------
        private void GetFarms() 
        {
            if (isAchiev[12])
            {
                farms[1].interactable = true;
                farms[2].interactable = true;
                farms[3].interactable = true;
            }
            
            if (isAchiev[13])
            {
                farms[4].interactable = true;
                farms[5].interactable = true;
                farms[6].interactable = true;
            }

            if (!isAchiev[14]) return;
            farms[7].interactable = true;
            farms[8].interactable = true;

        }
        
        // -------------------------------------------------------------------------------------------------------

        // Farm Panel Functions
        // -------------------------------------------------------------------------------------------------------
        
        public void buyFarm1()
        {
            if (_score < 1000) return;
            _score -= 1000;
            _passIncome++;
        }
        
        public void buyFarm2()
        {
            if (_score < 10000) return;
            _score -= 10000;
            _passIncome += 10;
        }
        
        public void buyFarm3()
        {
            if (_score < 2500) return;
            _score -= 25000;
            _passIncome += 25;
        }
        
        public void buyFarm4()
        {
            if (_score < 100000) return;
            _score -= 100000;
            _passIncome += 100;
        }
        
        public void buyFarm5()
        {
            if (_score < 500000) return;
            _score -= 500000;
            _passIncome += 500;
        }
        
        public void buyFarm6()
        {
            if (_score < 1000000) return;
            _score -= 1000000;
            _passIncome += 1000;
        }
        
        public void buyFarm7()
        {
            if (_score < 2500000) return;
            _score -= 2500000;
            _passIncome += 2500;
        }
        
        public void buyFarm8()
        {
            if (_score < 5000000) return;
            _score -= 5000000;
            _passIncome += 5000;
        }
        
        // -------------------------------------------------------------------------------------------------------
        
        
        // Farm Coroutine
        // -------------------------------------------------------------------------------------------------------

        private IEnumerator FarmCoroutine()
        {
            yield return new WaitForSeconds(1);
            _score += _passIncome * Bonus;
            _totalScore += _passIncome * Bonus;
            StartCoroutine(FarmCoroutine());
        }
        
        
        // -------------------------------------------------------------------------------------------------------
        
        /*public void RefreshScore()
        {
            _score = 0;
            _totalScore = 0;
            _clickIncome = 1;
            _clickScore = 0;
            _passIncome = 0;
            
            for (int i = 0; i < 16; i++)
            {
                if (isAchiev[i])
                {
                    isAchiev[i] = !isAchiev[i];
                }
            }
            _skinNumber = 0;
        }*/
        

#if UNITY_ANDROID && !UNITY_EDITOR
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                sv.score = _score;
                sv.clickIncome = _clickIncome;
                sv.clickScore = _clickScore;
                sv.passIncome = _passIncome;
                sv.totalScore = _totalScore;
                sv.skinNumber = _skinNumber;
                sv.isAchievements = new bool[16];
                for (int i = 0; i < 16; i++)
                {
                    sv.isAchievements[i] = isAchiev[i];
                }
                PlayerPrefs.SetString("SAVE", JsonUtility.ToJson(sv));
                PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
            }
        }
#else
        private void OnApplicationQuit()
        {
            sv.score = _score;
            sv.clickIncome = _clickIncome;
            sv.clickScore = _clickScore;
            sv.passIncome = _passIncome;
            sv.totalScore = _totalScore;
            sv.skinNumber = _skinNumber;
            sv.isAchievements = new bool[16];
            for (int i = 0; i < 16; i++)
            {
                sv.isAchievements[i] = isAchiev[i];
            }
            PlayerPrefs.SetString("SAVE", JsonUtility.ToJson(sv));
            PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());

        }
#endif
        
    }
    
    
   
}
[Serializable]
public class Save
{
    public int score;
    public int totalScore;
    public int passIncome ;
    public int clickIncome = 1;
    public int clickScore;
    public int skinNumber;
    public bool[] isAchievements;

}
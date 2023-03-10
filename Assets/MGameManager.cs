using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoreMountains.TopDownEngine;
namespace MoreMountains.TopDownEngine
{
    public class MGameManager : MonoBehaviour
    {
        enum GameState
        {
            Playing,
            ShopMenu
        }
        public PlayerStats playerStats;
        public static MGameManager instance;

        public float currentExperience;
        public float totalExperienceSofar;
        public float maxExperience;

        public float level;

        public float coins;

        public float waveTimer;

        public bool is_game_running;

        public int waveIndex = 1;

        //ui
        //text
        public TMP_Text text_waveTimer; //increment each wave with 10 seconds
        public TMP_Text text_currentWave;
        public TMP_Text text_coins;
        public TMP_Text text_currentLevel;
        public TMP_Text text_enemiesAlive;

        public TMP_Text text_ammo;
        public TMP_Text text_LevelStatus;
        //bars

        public Slider experience_bar;

        //canvas
        public Image ShopPanel;

        public GameObject offerObjectMenu;
        //y = m*x+b
        //wavetimer = m * wave + 20
        //wavetimer = 10 * 1 + 20 = 40

        private void Awake()
        {
            instance = this;

        }

        private void Start()
        {
            GameEventManager.instance.onStartCountdown += Instance_onStartCountdown;
            GameEventManager.instance.onLevelCleared += Instance_onLevelCleared;
            waveTimer = 3f;
            offerObjectMenu.gameObject.SetActive(false);
            playerStats = FindObjectOfType<PlayerStats>();
            //is_game_running = true;
            waveIndex = 1;
            currentExperience = playerStats.currentExperience;
            // currentExperience = FindObjectOfType<Player>().experience;
            //coins = FindObjectOfType<Player>().coins;

            //waveTimer = 10 * waveIndex + 10;

            //ShopPanel.gameObject.SetActive(false);
        }

        private void Instance_onLevelCleared()
        {
            if (text_LevelStatus != null)
            {
                text_LevelStatus.text = "Level Cleared";
            }
        }

        public void StartGame()
        {
            GameEventManager.instance.startCountdown();
            is_game_running = true;

            offerObjectMenu.gameObject.SetActive(false);
        }

        private void Update()
        {
            #region UI
            //text_currentWave.text = "WAVE " + waveIndex;
            //text_waveTimer.text = " " + Mathf.FloorToInt(waveTimer);

            if (text_currentLevel != null)
            {
                text_currentLevel.text = "LEVEL " + playerStats.level;
            }
            if (text_enemiesAlive != null)
            {
                text_enemiesAlive.text = "Enemies Alive:  " + FindObjectOfType<BasicSpawner>().allEntitiesAlive.Count;
            }
            

            text_coins.text = "coins: " + coins;

            experience_bar.value = playerStats.currentExperience;
            experience_bar.maxValue = playerStats.maxExperience;
            #endregion
            text_ammo.text = "ammo: " + FindObjectOfType<Weapon>().CurrentAmmoLoaded + " / " + FindObjectOfType<Weapon>().MagazineSize;
            //print(FindObjectOfType<Weapon>().CurrentAmmoLoaded);

            if (is_game_running)
            {
                //foreach (var allEnemies in FindObjectOfType<EnemySpawner>().objectpool)
                //{
                //    allEnemies.GetComponent<Enemy>().speed = 100f;
                //}

                if (playerStats.currentExperience >= playerStats.maxExperience)
                {
                    playerStats.maxExperience = playerStats.maxExperience * 1.25f + 5;
                    //maxExperience = SetNewMaxExpFunction()
                    playerStats.level++;
                    playerStats.currentExperience = 0;

                    //FindObjectOfType<EnemySpawner>().totalToSpawnPerLevel += 3;
                    //FindObjectOfType<EnemySpawner>().amountToSpawnPerLevel += 1;

                    //ShopPanel.gameObject.SetActive(true);
                    //FindObjectOfType<SkillChooser>().ShuffleSkills();
                    //is_game_running = false;
                }
                if (startCountdown)
                {
                    waveTimer -= Time.deltaTime;
                    text_waveTimer.text = " " + Mathf.CeilToInt(waveTimer);
                    if (waveTimer <= 0)
                    {
                        //spawn
                        GameEventManager.instance.startSpawning();

                        waveTimer = 0;
                        startCountdown = false;
                        text_waveTimer.gameObject.SetActive(false);
                    }
                }

                //if (FindObjectOfType<BasicSpawner>().allEntitiesAlive.Count <= 0)
                //{
                //    GameEventManager.instance.levelCleared();
                //}

                //if (waveTimer > 0)
                //{
                //    waveTimer -= Time.deltaTime;
                //}
                //if (waveTimer < 0)
                //{
                //    is_game_running =false;
                //    ShopPanel.gameObject.SetActive(true);
                //}
            }
            else
            {
                //foreach (var allEnemies in FindObjectOfType<EnemySpawner>().objectpool)
                //{
                //    allEnemies.GetComponent<Enemy>().speed = 0f;
                //}
            }
        }



        public void CloseShop()
        {
            //waveIndex++;
            is_game_running = true;
            ShopPanel.gameObject.SetActive(false);
        }
        public bool startCountdown;
        private void Instance_onStartCountdown()
        {
            startCountdown = true;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Traits")]
    [SerializeField] float fireRate;
    [SerializeField] float damage;
    [SerializeField] float shotSpeed;
    [SerializeField] float playerSpeed;
    [SerializeField] float playerHealth;
    [SerializeField] float currentExp;
    [SerializeField] int playerLevel;
    [SerializeField] float levelExpLimit;
    [SerializeField] float currentTime;
    [SerializeField] int goldCoinCount;
    [SerializeField] float enemySpawnRate;
    [SerializeField] int enemySpawnCount;
    [SerializeField] float luck;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider expBarSlider;
    [SerializeField] GameObject pausePanel;

    [Header("Game")]
    [SerializeField] WaveController wc;

    int waveCount;
    private int secs, mins;

    private void Awake()
    {
        Application.targetFrameRate = 45;
    }

    void Start()
    {
        waveCount = 0;
        currentTime = 0;
        goldCoinCount = 0;
        currentExp = 0;
        playerLevel = 1;
        playerHealth = 100;
        levelExpLimit = 50 * playerLevel;
        expBarSlider.minValue = 0;
        expBarSlider.maxValue = levelExpLimit;
        expBarSlider.value = currentExp;
        goldText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = Time.fixedTime;
        secs = Mathf.RoundToInt(currentTime) % 60;
        mins = Mathf.RoundToInt(currentTime) / 60;

        timerText.text = mins + ":" + secs;

        if (currentTime >= 30 * waveCount)
        {
            Debug.Log("Spawning a wave");
            waveCount += 1;
            wc.SpawnAreaWave();
        }

        if (currentTime == 300)
        {
            wc.SpawnBossWave();
        }
    }

    public void gainExp(int expVal)
    {
        currentExp += expVal;
        expBarSlider.value = currentExp;
        checkIfLevelUp();
    }

    private void checkIfLevelUp()
    {
        if (currentExp >= levelExpLimit)
        {
            playerLevel += 1;
            levelText.text = "Level " + playerLevel;
            currentExp = 0;
            expBarSlider.minValue = 0;
            expBarSlider.maxValue = levelExpLimit;
            expBarSlider.value = currentExp;
        }
    }

    public void getCoin(int amount)
    {
        goldCoinCount += amount;
        goldText.text = goldCoinCount.ToString();
    }

    public void PauseTheGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void UnauseTheGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}

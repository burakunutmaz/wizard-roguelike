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
    [SerializeField] int currentTime;
    [SerializeField] int goldCoinCount;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider expBarSlider;


    private int secs, mins;

    private void Awake()
    {
        Application.targetFrameRate = 45;
    }

    void Start()
    {
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
        currentTime = Mathf.FloorToInt(Time.fixedTime);
        secs = currentTime % 60;
        mins = currentTime / 60;

        timerText.text = mins + ":" + secs;
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
    }

    public void UnauseTheGame()
    {
        Time.timeScale = 1;
    }
}

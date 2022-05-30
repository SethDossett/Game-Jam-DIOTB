using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText, _coinAmount, _mainText;
    [SerializeField] TextMeshProUGUI _firstButton, _secondButton, _thirdButton;
    AudioSource _audioSource;
    int _score;
    int _coins;
    int _first;
    int _second;
    int _third;

    private void OnEnable()
    {
        _mainText.text = "Upgrades";
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        _score = PlayerPrefs.GetInt("HighScore");
        _coins = PlayerPrefs.GetInt("TotalCoins");

        _scoreText.text = _score.ToString();
        _coinAmount.text = _coins.ToString();

        GetSavedValues();
    }

    void GetSavedValues()
    {
        _first = PlayerPrefs.GetInt("FirstUpgrade");
        _second = PlayerPrefs.GetInt("SecondUpgrade");
        _third = PlayerPrefs.GetInt("ThirdtUpgrade");

        if(_first == 1) _firstButton.text = "Purchased";
        if (_second == 1) _secondButton.text = "Purchased";
        if (_third == 1) _thirdButton.text = "Purchased";

        
    }
    public void Upgrade(int value)
    {
        if (value == 0) FirstUpgrade();

        if (value == 1) SecondUpgrade();

        if (value == 2) ThirdUpgrade();

    }
    private void FirstUpgrade()
    {
        if(_first == 1)
            return;

        if(_coins <= 15)
            _mainText.text = "Not Enough Coins";
        else
        {
            _audioSource.Play();
            _mainText.text = "Purchased";
            _firstButton.text = "Purchased";
            PlayerPrefs.SetInt("FirstUpgrade", 1);
        }

        Invoke("ResetText", 3);
    }
    private void SecondUpgrade()
    {
        if (_second == 1)
            return;

        if (_coins <= 25)
            _mainText.text = "Not Enough Coins";
        else
        {
            _audioSource.Play();
            _mainText.text = "Purchased";
            _secondButton.text = "Purchased";
            PlayerPrefs.SetInt("SecondUpgrade", 1);
        }

        Invoke("ResetText", 3);
    }
    private void ThirdUpgrade()
    {
        if (_third == 1)
            return;

        if (_coins <= 35)
            _mainText.text = "Not Enough Coins";
        else
        {
            _audioSource.Play();
            _mainText.text = "Purchased";
            _thirdButton.text = "Purchased";
            PlayerPrefs.SetInt("ThirdtUpgrade", 1);
        }

        Invoke("ResetText", 3);
    }

    void ResetText()
    {
        _mainText.text = "Upgrades";
    }
}

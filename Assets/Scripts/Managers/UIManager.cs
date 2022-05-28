using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timeText, _healthText, _promptText, _scoreText, _pointsToAdd, _gameOverScore, _coinText;

    [Header("References")]
    [SerializeField] Image _healthBar;
    [SerializeField] GameObject _timer;
    [SerializeField] GameObject _pointSystem;
    [SerializeField] GameObject _coinUI;
    [SerializeField] Image _juiceBar;
    [SerializeField] GameObject _jetPackUI;
    [SerializeField] GameObject deadScreen;
    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] GameObject gameMusic;
    [SerializeField] GeneralEventSO _addPoints;

    [Header("Values")]
    [SerializeField] float _time;
    [SerializeField] int _coinAmount;
    int _currentPoints;
    int _totalPoints;
    [SerializeField] int _incrimentValue = 100;
    bool _countDown = true;

    private void OnEnable()
    {
        _addPoints.OnRaiseEvent += AddPoints;
    }
    private void OnDisable()
    {
        _addPoints.OnRaiseEvent -= AddPoints;
    }
    void Start()
    {
        _time = 7f;
        _totalPoints = 0;
        _currentPoints = 0;
        _scoreText.text = _currentPoints.ToString();
        _coinAmount = 0;
        _coinText.text = "0";
        _coinUI.SetActive(false);
        _jetPackUI.SetActive(false);
        _countDown = true;
        CountDown();
    }

    void Update()
    {
        if (_countDown) CountDown();
        else UpdateTime();

        //UpdateHealth();
    }

    public void UpdateHealth(int newHealth)
    {
        print("update health input " + newHealth *.01f+ ". changing fill amount from " + _healthBar.fillAmount);
        _healthBar.fillAmount = newHealth*.01f;
        print("to" + _healthBar.fillAmount);
        _healthText.text = Mathf.RoundToInt(_healthBar.fillAmount * 100).ToString() + "%";
    }

    public void UpdatePrompt(string message)
    {
        _promptText.text = message;
    }
    public void UpdateCoins()
    {
        _coinAmount++;

        _coinText.text = _coinAmount.ToString();
    }
    public void UpdateJetJuice(float juice)
    {
        _juiceBar.fillAmount = juice * 0.1f;
    }
    private void AddPoints()
    {
        StartCoroutine(UpdatePoints());
        
    }
    private IEnumerator UpdatePoints()
    {
        int points = _pointSystem.GetComponent<PointSystem>().GetPoints();
        StartCoroutine(PointsToAdd(points));
        _totalPoints += points;

        while (_currentPoints < _totalPoints)
        {
            _currentPoints += _incrimentValue;
            _scoreText.text = _currentPoints.ToString();
            yield return null;
        }
        
    }
    private IEnumerator PointsToAdd(int points)
    {
        _pointsToAdd.text = $"+ {points}";
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => _currentPoints >= _totalPoints);
        _pointsToAdd.text = "Score";

    }
    private void CountDown()
    {
        _time -= Time.deltaTime;    
        _timeText.text = $"{(Mathf.RoundToInt(_time)).ToString()}";
        if (_time <= 0f)
        {
            _time = 0f;
            _countDown = false;
        }
         
    }
    private void UpdateTime()
    {
        _time += Time.deltaTime;

        int sec = Mathf.RoundToInt(_time);
        int min = 0;
        if (sec >= 60)
        {
            sec = 0;
            min++;
        }
        min = (min % 60);

        _timeText.text = $"{min}:{(Mathf.RoundToInt(_time) % 60).ToString("D2")}";
    }

    public void ShowDeadScreen()
    {
        gameMusic.SetActive(false);
        deadScreen.SetActive(true);
        _coinUI.SetActive(true); 
        _jetPackUI.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        _gameOverScreen.SetActive(true);
        deadScreen.SetActive(false);
        _gameOverScore.text = _totalPoints.ToString();

    }
}

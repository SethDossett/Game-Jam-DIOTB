using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image _healthBar;
    [SerializeField] GameObject _timer;
    [SerializeField] GameObject _pointSystem;
    [SerializeField] GameObject deadScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject gameMusic;
    [SerializeField] GeneralEventSO _addPoints;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI  _timeText, _healthText, _promptText, _scoreText;

    [Header("Values")]
    [SerializeField] float _time;
    int _currentPoints;
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
        _currentPoints = 0;
        _scoreText.text = _currentPoints.ToString();
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
    private void AddPoints()
    {
        StartCoroutine(UpdatePoints());
    }
    private IEnumerator UpdatePoints()
    {
        int points = _pointSystem.GetComponent<PointSystem>().GetPoints();
        while(_currentPoints < points)
        {
            _currentPoints += _incrimentValue;
            _scoreText.text = _currentPoints.ToString();
            yield return null;
        }
        
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
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        deadScreen.SetActive(false);

    }
}

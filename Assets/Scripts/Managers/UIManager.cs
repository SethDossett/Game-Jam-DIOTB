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
    [SerializeField] GeneralEventSO _addPoints;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI  _timeText, _healthText, _promptText, _scoreText;
    [SerializeField] float _time;
    int _currentPoints;

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
        _time = 0f;
        _currentPoints = 0;
        _scoreText.text = _currentPoints.ToString();
    }

    void Update()
    {
        UpdateTime();
        UpdatePrompt();
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        _healthText.text = Mathf.RoundToInt(_healthBar.fillAmount * 100).ToString() + "%";
    }

    private void UpdatePrompt()
    {
        
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
            _currentPoints += 100;
            _scoreText.text = _currentPoints.ToString();
            yield return null;
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
}

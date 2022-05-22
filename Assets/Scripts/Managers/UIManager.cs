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

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI  _timeText, _healthText, _promptText, _scoreText;
    [SerializeField] float _time;

    void Start()
    {
        _time = 0f;
    }

    void Update()
    {
        UpdateTime();
        UpdatePoints();
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

    private void UpdatePoints()
    {
        int points = _pointSystem.GetComponent<PointSystem>().GetPoints();

        _scoreText.text = points.ToString();
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

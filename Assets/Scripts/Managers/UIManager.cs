using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image _healthBar;
    [SerializeField] GameObject _timer;

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI  _timeText, _healthText, _promptText, _scoreText;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

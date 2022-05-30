using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _instuctions;
    [SerializeField] GameObject _upgrades;
    [SerializeField] GameObject _options;
    [SerializeField] TextMeshProUGUI _hiscoreText;

    private void Start()
    {
        Time.timeScale = 1f;
        _hiscoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        SceneManager.LoadScene(1);
        yield break;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetHiscores()
    {
        PlayerPrefs.DeleteAll();
        _hiscoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
    }
    public void OpenMenu(int option)
    {
        if(option == 0)
        {
            _upgrades.SetActive(false);
            _options.SetActive(false);

            if (_instuctions.activeInHierarchy)
                _instuctions.SetActive(false);
            else
                _instuctions.SetActive(true);
            
        }

        if(option == 1)
        {
            _instuctions.SetActive(false);
            _options.SetActive(false);

            if (_upgrades.activeInHierarchy)
                _upgrades.SetActive(false);
            else
                _upgrades.SetActive(true);
        }

        if(option == 2)
        {
            _instuctions.SetActive(false);
            _upgrades.SetActive(false);

            if (_options.activeInHierarchy)
                _options.SetActive(false);
            else
                _options.SetActive(true);
        }
    }

}

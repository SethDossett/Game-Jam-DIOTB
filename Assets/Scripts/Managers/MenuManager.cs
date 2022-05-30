using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _instuctions;
    [SerializeField] GameObject _upgrades;
    [SerializeField] GameObject _options;

    public void StartGame()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetHiscores()
    {
        PlayerPrefs.DeleteAll();
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

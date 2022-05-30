using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    SaveGame _save;
    UIManager _UIManager;

    private void Start()
    {
        _save = FindObjectOfType<SaveGame>();
        _UIManager = GetComponentInParent<UIManager>();

    }
    public void RestartGame()
    {
        _save.Save(_UIManager._totalPoints, _UIManager._coinAmount);
        StartCoroutine(ChangeScene(1));
    }

    IEnumerator ChangeScene(int buildIndex)
    {
        yield return new WaitForSecondsRealtime(1.5f);

        SceneManager.LoadScene(buildIndex);
    }

    public void BackToMenu() 
    {
        _save.Save(_UIManager._totalPoints, _UIManager._coinAmount);

        StartCoroutine(ChangeScene(0));
    }

}

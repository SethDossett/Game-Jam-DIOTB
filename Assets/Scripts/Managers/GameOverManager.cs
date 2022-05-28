using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        SceneManager.LoadScene(1);
        print("YEADFSFGH");
    }
}

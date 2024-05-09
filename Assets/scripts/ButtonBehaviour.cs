using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    private void Start()
    {
        Config.CreateScoreFile();
    }
    public void LoadScenes(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void ResetGameSetting()
    {
        GameSetting.Instance.ResetGameSetting();
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

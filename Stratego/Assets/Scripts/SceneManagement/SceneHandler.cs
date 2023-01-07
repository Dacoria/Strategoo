using Photon.Pun;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler instance;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        instance = this;
        if (!PhotonNetwork.IsConnected)
        {
            GoToLoadingScene();
        }
    }

    public void GoToLoadingScene()
    {
        Settings.DefaultLevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(Statics.SCENE_LOADING);
    }

    public int GetCurrentSceneNr()
    {
        var currentSceneName = SceneManager.GetActiveScene().name;
        var value = Regex.Match(currentSceneName, @"\d+").Value;
        return int.Parse(value);
    }
}
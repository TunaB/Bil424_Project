using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Artifacts()
    {
        SceneManager.LoadScene("Artifacts");
    }
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

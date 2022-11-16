using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    //public AudioMenu audio;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Tap2Play()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void NewGameBTN()
    {
        SceneManager.LoadScene("LoadingScene");
        //audio.music.Stop();
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

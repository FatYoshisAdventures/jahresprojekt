using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Host()
    {
        PlayerPrefs.SetInt("host", 1);
        SceneManager.LoadScene(1);
    }

    public void join()
    {
        PlayerPrefs.SetInt("host", 0);
        SceneManager.LoadScene(1);
    }
}

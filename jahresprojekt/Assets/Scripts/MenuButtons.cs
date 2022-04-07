using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    [SerializeField] private GameObject input;

    public void Host()
    {
        PlayerPrefs.SetInt("host", 1);
        SceneManager.LoadScene(1);
    }

    public void join()
    {
        PlayerPrefs.SetInt("host", 0);
        if (!checkIP(input.GetComponent<TMP_InputField>().text))
        {
            //no valid ip address entered
            return;
        }
        
        PlayerPrefs.SetString("ip", input.GetComponent<TMP_InputField>().text);
        Debug.Log(input.GetComponent<TMP_InputField>().text);
        //add checks for ip e.g 4 point every value between 0 and 255


        SceneManager.LoadScene(1);
    }

    private bool checkIP(string ip)
    {
        //(\b25[0-5]|\b2[0-4][0-9]|\b[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3} regex vielleicht a möglich
        string[] ipValues = ip.Split('.', System.StringSplitOptions.RemoveEmptyEntries);
        
        if (ipValues.Length != 4)
        {
            return false;
        }
        foreach (var item in ipValues)
        {
            int check;
            if (int.TryParse(item, out check))
            {
                if (check < 0 || check > 255)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }
}

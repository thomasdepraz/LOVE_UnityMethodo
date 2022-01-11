using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void Play()
    {
        //Debug.Log("load level");
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Debug.Log("quit app");
        Application.Quit();
    }
}

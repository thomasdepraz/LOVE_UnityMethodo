using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    private List<string> _keyStrokeHistory;
    public AudioClip pranked;
    private AudioSource audioSource;

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

    ///hidden feature
    ///
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Awake()
    {
        _keyStrokeHistory = new List<string>();
    }

    void Update()
    {
        KeyCode keyPressed = DetectKeyPressed();
        AddKeyStrokeToHistory(keyPressed.ToString());
        //keyStrokeText.text = "HISTORY: " + GetKeyStrokeHistory();
        if (GetKeyStrokeHistory().Equals("UpArrow,UpArrow,DownArrow,DownArrow,LeftArrow,RightArrow,LeftArrow,RightArrow,B,A"))
        {
            Debug.Log("KONAMI CHEAT CODE DETECTED!");
            audioSource.enabled = true;
            audioSource.PlayOneShot(pranked);
            ClearKeyStrokeHistory();
        }
    }

    private KeyCode DetectKeyPressed()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }

    private void AddKeyStrokeToHistory(string keyStroke)
    {
        if (!keyStroke.Equals("None"))
        {
            _keyStrokeHistory.Add(keyStroke);
            if (_keyStrokeHistory.Count > 10)
            {
                _keyStrokeHistory.RemoveAt(0);
            }
        }
    }

    private string GetKeyStrokeHistory()
    {
        return String.Join(",", _keyStrokeHistory.ToArray());
    }

    private void ClearKeyStrokeHistory()
    {
        _keyStrokeHistory.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    private UIDocument m_UIDocument;

    private void Awake()
    {
        m_UIDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = m_UIDocument.rootVisualElement;
        Button continueButton = root.Q<Button>("Resume");
        continueButton.clicked += Continue;
        
        Button optionButton = root.Q<Button>("Option");
        optionButton.clicked += Option;
        Button quitButton = root.Q<Button>("Quit");
        quitButton.clicked += Quit;
    }

    public void Continue()
    {
        GameManager.Instance.UnPause();
        Debug.Log("continue");
    }

    public void Option()
    {
        Debug.Log("option");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}

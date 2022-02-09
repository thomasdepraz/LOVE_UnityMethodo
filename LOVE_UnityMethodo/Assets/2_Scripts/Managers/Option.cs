using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Option : MonoBehaviour
{
    private UIDocument m_UIDocument;

    private DropdownField resolution;

    private void Awake()
    {
        m_UIDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = m_UIDocument.rootVisualElement;
        Slider volumeSlider = root.Q<Slider>("Volume");
        //volumeSlider. += Volume;
        volumeSlider.RegisterValueChangedCallback<float>(v =>
        {
            GameManager.Instance.volume = v.newValue;
            Debug.Log("Nouveau volume : " + v.newValue);
        });

        Button returnButton = root.Q<Button>("Return");
        returnButton.clicked += ReturnToPause;

        resolution = root.Q<DropdownField>("Resolution");
        List<string> resolutionList = new List<string>();
        resolutionList.Add("1920 - 1080");
        resolutionList.Add("1280 - 720");
        resolution.choices = resolutionList;
        //resolution.index += ChangeResolution;
        resolution.RegisterValueChangedCallback<string>(ChangeResolution);
    }

    public void Volume()
    {
        GameManager.Instance.UnPause();
    }

    public void ReturnToPause()
    {
        Debug.Log("return to pause");
        GameManager.Instance.Pause();
    }

    public void ChangeResolution(ChangeEvent<string> evt)
    {
        Debug.Log("change resolution to :");
        //Camera cam = Camera.current;
        switch (resolution.index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                Debug.Log("1920, 1080");
                break;
            case 1:
                Screen.SetResolution(1280, 720, true);
                Debug.Log("1080, 720");
                break;
        }
        
    }
}

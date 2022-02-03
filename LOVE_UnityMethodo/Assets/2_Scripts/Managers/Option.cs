using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Option : MonoBehaviour
{
    private UIDocument m_UIDocument;

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
    }

    public void Volume()
    {
        GameManager.Instance.UnPause();
    }

    public void ReturnToPause()
    {
        GameManager.Instance.Pause();
    }
}

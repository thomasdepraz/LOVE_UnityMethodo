using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public RectTransform lifeCountContainer;
    public TextMeshProUGUI lifeText;

    void Start()
    {
        GameManager.Instance.uiHandler = this;    
    }

    public void AppearScoreScreen()
    {

    }

    public void UpdateLifeCount(int lifeCount, int maxLife)
    {
        lifeText.text = $"{ lifeCount} / {maxLife}";
        LeanTween.cancel(lifeCountContainer.gameObject);
        lifeCountContainer.localScale = Vector3.one;
        LeanTween.scale(lifeCountContainer.gameObject, Vector3.one * 1.2f, 0.2f).setEaseInOutQuint().setLoopPingPong(1);
    }

}

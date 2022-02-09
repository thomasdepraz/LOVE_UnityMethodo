using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public RectTransform lifeCountContainer;
    public TextMeshProUGUI lifeText;

    public GameObject container;
    public Button ReturnToMenuButton;
    public TextMeshProUGUI title;
    public TextMeshProUGUI score; 

    void Start()
    {
        ReturnToMenuButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        GameManager.Instance.uiHandler = this;    
    }

    public void AppearScoreScreen(int score, string title)
    {
        container.SetActive(true);
        this.title.text = title;
        this.score.text = $"Score : {score}";
    }

    public void UpdateLifeCount(int lifeCount, int maxLife)
    {
        lifeText.text = $"{ lifeCount} / {maxLife}";
        LeanTween.cancel(lifeCountContainer.gameObject);
        lifeCountContainer.localScale = Vector3.one;
        LeanTween.scale(lifeCountContainer.gameObject, Vector3.one * 1.2f, 0.2f).setEaseInOutQuint().setLoopPingPong(1);
    }

}

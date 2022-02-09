using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject PauseMenu;
    public GameObject OptionMenu;
    public bool isPaused = false;

    //SOUND
    public float volume = 100;

    public void Awake()
    {
        Instance = this;
    }

    public UIHandler uiHandler;
    public PlayerController player;
    public List<Level> levels;
    [HideInInspector]public Level currentLevel;
    public int currentLevelCount = 0;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        currentLevel = levels[0];
        LoadLevel(currentLevel);
    }

    private void Update()
    {

        //Pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused) Pause();
            else UnPause();
        }
    }

    public void LoadLevel(Level level)
    {
        
        if (currentLevel != null)
            currentLevel.levelContainer.SetActive(false);

        if(level!=null)
        {
            //LOAD
            currentLevel = level;
            level.levelContainer.SetActive(true);
            player.Spawn();
            currentLevelCount++;
        }
        else
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        //WIN
        //Calculate score 
        int score = currentLevelCount * 50 + (player.numberOfCheckpoint * (-5)) + (player.numberOfDeath * (-10));

        //Appear score UI
        uiHandler.AppearScoreScreen(score, "You lost !");
        //SceneManager.LoadScene(0);
    }

    public Level NextLevel()
    {
        int index = levels.IndexOf(currentLevel);

        if (index < levels.Count - 1)
            return levels[index + 1];
        else
            return null;
    }

    public void Pause()
    {
        isPaused = true;
        PauseMenu.SetActive(true);
        OptionMenu.SetActive(false);
    }

    public void UnPause()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
    }

    public void Option()
    {
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(true);
    }


}

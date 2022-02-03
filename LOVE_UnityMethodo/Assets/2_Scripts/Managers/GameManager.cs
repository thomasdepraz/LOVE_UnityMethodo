using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject PauseMenu;
    private bool isPaused = false;

    public void Awake()
    {
        Instance = this;
    }

    public PlayerController player;
    public List<Level> levels;
    [HideInInspector]public Level currentLevel;

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
        
        if(level!=null)
        {
            //LOAD
            currentLevel = level;
            level.levelContainer.SetActive(true);
            player.Spawn();

        }
        else
        {
             //WIN

        }
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
    }

    public void UnPause()
    {
        isPaused = false;
        PauseMenu.SetActive(false);
    }


}

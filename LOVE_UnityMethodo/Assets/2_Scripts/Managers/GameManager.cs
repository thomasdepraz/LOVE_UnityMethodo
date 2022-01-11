using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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


}

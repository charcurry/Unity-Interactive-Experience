using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevelTrigger : MonoBehaviour
{
    private LevelManager levelManager;
    public string levelName;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            levelManager.LoadScene(levelName);
        }
    }
}

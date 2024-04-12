using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private LevelManager levelManager;
    public bool gameWin;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (gameWin)
            {
                levelManager.LoadScene("GameWin");
            }
            if (!gameWin)
            {
                levelManager.LoadScene("GameOver");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    private float startTime;
    private int gameLength;
    private int spawnRateEnemy1 = 0;
    private int spawnRateEnemy2 = 0;
    private int spawnRateEnemy3 = 0;
    private int enemySpawnRate = 0;
    private bool isGamePaused = false;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    private int[] randomSpawnRadius = { -10, -9, -8, -7, -6, -5, 5, 6, 7, 8, 9, 10 };

    [SerializeField]
    private GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        enemySpawnage();

        spawnRateIncrease();

        // These if statements pause and unpause the game, which stops time and total time, which allows for accurate time taking of how long the player is actually playing the game
        if (Input.GetKeyDown(KeyCode.P) && isGamePaused == false)
        {
            pauseGame();
            Debug.Log("Game is paused!");
        }
        if (Input.GetKeyDown(KeyCode.U) && isGamePaused == true)
        {
            resumeGame();
            Debug.Log("Game is resumed!");
        }       
        
    }

    //Spawns in enemies, with it spawning 1 more enemy1 every 10 seconds, enemy2 every 20 and enemy3 every 30
    private void enemySpawnage()
    {
        if (spawnRateEnemy1 > enemySpawnRate)
        {
            for (var i = 0; i < spawnRateEnemy1; i++)
            {
                Instantiate(enemy1, new Vector3(randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)], 0, randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)]), Quaternion.identity);
                if (i == spawnRateEnemy1)
                {
                    i = 0;
                }
            }

            for (var i = 0; i < spawnRateEnemy2; i++)
            {
                Instantiate(enemy2, new Vector3(randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)], 0, randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)]), Quaternion.identity);
                if (i == spawnRateEnemy2)
                {
                    i = 0;
                }
            }

            for (var i = 0; i < spawnRateEnemy3; i++)
            {
                Instantiate(enemy3, new Vector3(randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)], 0, randomSpawnRadius[Random.Range(0, randomSpawnRadius.Length)]), Quaternion.identity);
                if (i == spawnRateEnemy3)
                {
                    i = 0;
                }
            }

            enemySpawnRate++;
        }



    }

    private void spawnRateIncrease()
    {
        // Updates the gameLength variable
        totalTime();

        spawnRateEnemy1 = (gameLength / 10) + 1;

        spawnRateEnemy2 = (gameLength / 20);

        spawnRateEnemy3 = (gameLength / 30);

        Debug.Log("1 is " + spawnRateEnemy1 + " 2 is " + spawnRateEnemy2 + " 3 is " + spawnRateEnemy3);
    }

    // Says how long the game has been played
    private void totalTime()
    {
        gameLength = Mathf.RoundToInt(Time.time - startTime);
    }

    // Pauses the game by setting time scale to 0
    private void pauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
    }

    //Resumes the game by setting time scale back to 1
    private void resumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
    }

}

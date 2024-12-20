using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class WaveData 
{
   public List<Pawn> pawns;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CameraController mainCamera;
    public SpawnPoint[] spawnPoints;

    public GameObject prefabEnemyUI;
    
    public bool isPaused;
    public GameObject prefabPlayerController;
    public AIController prefabAIController;
    public List<AIController> enemies;
    public int enemiesRemaining = 0;
    public Pawn prefabPlayerPawn;
    public Pawn prefabAIPawn;

    public PlayerController player;

    public int currentWave;
    public List<WaveData> waves;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void LoadSpawnPoints()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    public Pawn SpawnPawn()
    {
        return SpawnPawn(prefabPlayerPawn);
    }

    public Pawn SpawnPawn  (Pawn pawnToSpawn)
    {
        // Spawn the Player at a random spawn point
        Transform randomSpawnPoint = GetRandomSpawnPoint();
        Pawn tempPawn = Instantiate<Pawn>(prefabPlayerPawn, randomSpawnPoint.transform.position, randomSpawnPoint.transform.rotation);
        return tempPawn;
    }

    public void SpawnPlayer ()
    {
        // Spawn the Player Controller at 0,0,0, and save it in our player variable
        if (player == null)
        {
            player = Instantiate(prefabPlayerController).GetComponent<PlayerController>();
        }

        // Connect the controller and pawn!
        player.PossessPawn(SpawnPawn());

        // Connect the camera controller to the pawn
        mainCamera.target = player.pawn.transform;

        // Subscribe to the player death event
        Health playerHealth = player.pawn.GetComponent<Health>();

        if (playerHealth != null) {
            playerHealth.OnDeath.AddListener(OnPlayerDeath);
        }
    }

    public void SpawnEnemy()
    {
        SpawnEnemy(prefabAIPawn);

    }

      public void SpawnEnemy(Pawn pawnToSpawn)
    {
        UnityEngine.Debug.Log("is tghis even working");
        // Spawn the AI Controller at 0,0,0, 
        AIController newAI = Instantiate<AIController>(prefabAIController, Vector3.zero, Quaternion.identity);

        //Save it in our AI list
        enemies.Add(newAI);

        // Connect the controller and pawn!
        newAI.PossessPawn(SpawnPawn(pawnToSpawn));

        // If our AI has health 
        Health newAIHealth = newAI.pawn.GetComponent<Health>();
        if (newAIHealth != null)
        {
            // Subscribe to the new enemy's OnDeath event
            newAIHealth.OnDeath.AddListener(OnEnemyDeath);

            // Spawn a UI and attach it to the enemy
            GameObject newEnemyUI = Instantiate(prefabEnemyUI, newAI.pawn.transform) as GameObject;
            
            // Connect the enemy health
            EnemyHealthDisplay newEnemyUIScript = newEnemyUI.GetComponent<EnemyHealthDisplay>();

            if (newEnemyUIScript != null)
            {
                newEnemyUIScript.enemyHealth = newAIHealth;
            }
        }
    }
    public void OnEnemyDeath()
   {
       // Subtract 1 from enemies remaining
       enemiesRemaining--;

       // TODO: Add anything else we need to do when the enemy dies

       // If we are out of enemies, advance to the next wave
       if (enemiesRemaining <= 0) {
           // advance to next wave
           currentWave++;

           // If it exists, spawn it.
           if (currentWave < waves.Count)
           {
               SpawnWave(waves[currentWave]);
           }
           // Otherwise, Victory!
           else
           {
               DoVictory();
           }
       }        
   }

    public void DoVictory()
    {
        UnityEngine.Debug.Log("---------------VICTORY---------------");
        SceneManager.LoadScene("Victory");
    }

    public void SpawnWave (int waveNumber)
    {
        // Spawn the wave for that wave number using our overloaded function!
        SpawnWave(waves[waveNumber]);

    }

    public void SpawnWave (WaveData wave)
    {
        // For each enemy in the wave
        foreach (Pawn enemyToSpawn in wave.pawns )
        {
            // Spawn the enemy
            SpawnEnemy(enemyToSpawn);
        }

        // Save the number of enemies
        enemiesRemaining = wave.pawns.Count;
    }

    public void StartGame()
    {
        isPaused = false;
        // Set our current wave to 0
        
        currentWave = 0;

        // Connect to our camera
        FindCamera();

        // Load our spawn points
        LoadSpawnPoints();

        // Spawn player
        SpawnPlayer();

        // Spawn our current wave
        SpawnWave(waves[currentWave]);
    }

    public void ClearEnemies ()
    {
        // For every enemy in the enemy list
        foreach (AIController enemy in enemies)
        {
            // if that enemy exists
            if (enemy != null) 
            { 
                // If it has a pawn
                if (enemy.pawn!= null)
                {
                    // destroy the pawn
                    Destroy(enemy.pawn.gameObject);
                }   
                // Destroy the Controller
                Destroy (enemy.gameObject);
            }
        }
        // After we have destroyed all the enemies and pawns, clear the list of enemies
        enemies.Clear();
    }

    public Transform GetRandomSpawnPoint()
    {
        // if we have spawn points
        if (spawnPoints.Length > 0)
        {
            // return a random player spawnpoint
            return spawnPoints[Random.Range(0,spawnPoints.Length)].transform;
        }
        // Otherwise, return null
        return null;
    }

    public void FindCamera()
    {
        // Find and store the camera controller
        mainCamera = FindObjectOfType<CameraController>();
    }

    public void DoGameOver()
    {
        SceneManager.LoadScene("Failure");
    }

    public void RespawnPlayer()
    {

        // If we have enough lives
        if (player.lives > 0)
        {
            // Destroy their current pawn
            Destroy(player.pawn.gameObject);

            // Unpossess the current pawn
            player.UnpossessPawn();

            // Spawn a new pawn and possess it instead
            player.PossessPawn(SpawnPawn());

            // Connect the camera controller to the pawn
            mainCamera.target = player.pawn.transform;

           // Subtract one from lives
           player.lives--;

        } 
        // Otherwise, call the game over function
        else
        {
            DoGameOver();
        }
    }

    public void OnPlayerDeath()
    {
        // Respawn the player
        RespawnPlayer();

    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused) return;
        
    }

    public void Pause()
    {

        isPaused = true;
        Time.timeScale = 0.0f;
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
    }

    public void UnPause()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            UnPause();
        } else
        {
            Pause();
        }
    }
}



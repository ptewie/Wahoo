using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SpawnPoint : MonoBehaviour
{
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CameraController mainCamera;
    public SpawnPoint[] spawnPoints;
    
    public bool isPaused;
    public GameObject prefabPlayerController;
    public GameObject prefabAIController;
    public Pawn prefabPlayerPawn;

    public PlayerController player;

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

    public Pawn SpawnPawn ()
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

    public void StartGame()
    {
        // Connect to our camera
        FindCamera();

        // Load our spawn points
        LoadSpawnPoints();

        // Spawn player
        SpawnPlayer();
    }

    public Transform GetRandomSpawnPoint ()
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

        // TODO: Add anything else we need to do when the player dies
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void UnPause()
    {
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



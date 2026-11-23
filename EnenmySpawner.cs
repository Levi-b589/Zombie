using TMPro;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private int firstLevelZombies = 3;
    [SerializeField] private int extraZombiesPerLevel = 2;
    [SerializeField] private int finalLevel = 5;
    [SerializeField] private TMP_Text levelText;

    [Header("Zombies")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Key")]
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private Transform keySpawnPoint;

    private int currentLevel;
    private int zombiesAlive;
    private int zombiesKilled;
    private bool keySpawned;

    //makes  sure you can't pick up the key
    private void Start()
    {
        KeyPickup.hasKey = false;
        StartNextLevel();
    }
    //makes sure what level you are at and if you are at the final level
    private void StartNextLevel()
    {
        if (currentLevel >= finalLevel)
        {
            SpawnKey();
            return;
        }
        //tells me if the spawner doesn't have a enemy prefab on it
        if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Assign an enemy prefab and at least one spawn point.", this);
            return;
        }
        //allows the level counter to go up by 1
        currentLevel++;

        int zombiesThisLevel = firstLevelZombies + (currentLevel - 1) * extraZombiesPerLevel;

        zombiesAlive = zombiesThisLevel;
        zombiesKilled = 0;
        UpdateLevelText();

        Debug.Log($"Level {currentLevel}: kill all {zombiesThisLevel} zombies.");

        //this connects to the spawner and allows the enemys to die and count as a kill
        for (int i = 0; i < zombiesThisLevel; i++)
        {
            Transform point = spawnPoints[i % spawnPoints.Length];

            if (point == null)
            {
                Debug.LogError($"Spawn Point {i % spawnPoints.Length} is not assigned.", this);
                continue;
            }

            GameObject enemy = Instantiate(enemyPrefab, point.position, point.rotation);

            zombie_ai ai = enemy.GetComponentInChildren<zombie_ai>();
            GameObject deathObject;

            if (ai != null)
            {
                deathObject = ai.gameObject;
            }
            else
            {
                deathObject = enemy;
            }

            //notifies that the enemy was destroyed
            EnemyDeathNotifier notifier = deathObject.GetComponent<EnemyDeathNotifier>();

            if (notifier == null)
            {
                notifier = deathObject.AddComponent<EnemyDeathNotifier>();
            }

            notifier.SetSpawner(this);
        }
    }

    public void EnemyDied()
    {
        if (keySpawned || zombiesAlive <= 0)
        {
            return;
        }

        zombiesAlive--;
        zombiesKilled++;
        UpdateLevelText();

        if (zombiesAlive == 0)
        {
            StartNextLevel();
        }
    }
    //Updates the level
    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            int total = firstLevelZombies + (currentLevel - 1) * extraZombiesPerLevel;
            levelText.text = $"LEVEL {currentLevel}   ZOMBIES: {zombiesKilled}/{total}";
        }
    }
    //Spawns the key for you to leave
    private void SpawnKey()
    {
        if (keySpawned)
        {
            return;
        }

        if (keyPrefab == null || keySpawnPoint == null)
        {
            Debug.LogError("Assign the key prefab and key spawn point.", this);
            return;
        }

        keySpawned = true;
        Instantiate(keyPrefab, keySpawnPoint.position, keySpawnPoint.rotation);

        if (levelText != null)
        {
            levelText.text = "FINAL LEVEL CLEARED — FIND THE KEY!";
        }
    }
}
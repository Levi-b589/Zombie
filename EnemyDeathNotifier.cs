using UnityEngine;

public class EnemyDeathNotifier : MonoBehaviour
{
    private EnemySpawner spawner;
    private bool alreadyReported;

    //the spawner that makes the enemy
    public void SetSpawner(EnemySpawner owner)
    {
        spawner = owner;
    }
    //makes sure the enemy isnt reported dead more than once
    void OnDestroy()
    {
        if (alreadyReported || spawner == null)
        {
            return;
        }

        alreadyReported = true;
        spawner.EnemyDied();
    }
}

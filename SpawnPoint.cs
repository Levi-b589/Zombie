using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //spawns you at the front of the room so it looks like you "walked" into the building
    public Transform spawnPoint;
    void Start()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
        }
    }
}
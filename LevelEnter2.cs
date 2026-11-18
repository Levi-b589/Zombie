using UnityEngine;
using UnityEngine.SceneManagement;
//another level enter script so you can go to the 4th scene

public class LevelEnter2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Levelenter"))
        {
            if (KeyPickup.hasKey)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("You need to find the key first!");
            }
        }
    }


}
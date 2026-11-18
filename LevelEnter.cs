using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelEnter : MonoBehaviour
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
            if(NPCInteraction.hasTalked)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("Youneed to talk to the NPC first!");
            }
        }
    }


}

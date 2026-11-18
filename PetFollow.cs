using UnityEngine;
using UnityEngine.AI;

public class PetFollow : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //NavMesh is for the pet to move around and follow you
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    //this tracks the player postion and allows the pet to actually follow you
    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}

using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public static bool hasKey = false;
    //it allows you to pick up a "key" and destroys the object when its collected
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            hasKey = true;
            Debug.Log("Key Collected");
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public static bool hasKey = false;
    private void OnTriggerEnter(Collider other)
    {
        hasKey = true;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Key Collected");
            Destroy(gameObject);
        }
    }
}

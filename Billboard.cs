using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //Makes the camera go to a specific area for settings
    void Start()
    {
        playerCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerCamera != null)
        {
            transform.LookAt(playerCamera.transform);
        }
    }
}

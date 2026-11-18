using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLooking2 : MonoBehaviour
{
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float sensitivity = 0.1f;

    private Vector2 lookInput;
    private float xRotation;

    void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", sensitivity);
    }

    // Called by Player Input when its Behavior is set to Send Messages.
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        if (Time.timeScale == 0f)
            return;

        // Turn the player left and right.
        transform.Rotate(Vector3.up * lookInput.x * sensitivity);

        // Tilt the camera up and down.
        xRotation -= lookInput.y * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void SetSensitivity(float value)
    {
        sensitivity = value;
        PlayerPrefs.SetFloat("Sensitivity", value);
    }
}
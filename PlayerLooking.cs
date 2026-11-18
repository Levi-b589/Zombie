using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLooking : MonoBehaviour
{
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float sensitivity = 0.1f;

    private Vector2 lookInput;
    private float xRotation;

    public MenuScript menu;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Called by Player Input (Send Messages)
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        float sens = menu.mouseSensitivity;

        // Left/right: turn the whole player
        transform.Rotate(Vector3.up * lookInput.x * sensitivity * menu.mouseSensitivity);

        // Up/down: tilt only the camera
        xRotation -= lookInput.y * sensitivity * menu.mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    public void SetSensitivity(float value)
    {
        sensitivity = value;
    }

}

using UnityEngine;
using UnityEngine.Rendering;

public class fps_Camera : MonoBehaviour
{

    public Transform player;
    public float mouseSens = 2f;
    float cam_ver_rot = 0f;

    bool lockedcurser = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSens;
        float inputY = Input.GetAxis("Mouse Y") * mouseSens;

        cam_ver_rot -= inputY;
        cam_ver_rot = Mathf.Clamp(cam_ver_rot, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cam_ver_rot;

        player.Rotate(Vector3.up * inputX);
    }
}

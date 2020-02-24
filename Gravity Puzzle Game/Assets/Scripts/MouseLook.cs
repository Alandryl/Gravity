using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    PlayerMovementScriptNew playerMovement;

    public float mouseSensitivity = 100f;

    public GameObject player;
    public Transform playerBody;

    float xrotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovementScriptNew>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.rotationTimeLeft <= 0f)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xrotation -= mouseY;
            xrotation = Mathf.Clamp(xrotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}

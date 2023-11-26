using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] Transform freelookCamera;

    float horizontalAxis;
    float verticalAxis;
    Vector3 horizontalDirection;
    Vector3 verticalDirection;
    Vector3 movementDirection;

    private void Awake()
    {
        TryGetComponent(out rb);
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");
        horizontalDirection = horizontalAxis * freelookCamera.right;
        verticalDirection = verticalAxis * freelookCamera.forward;
        horizontalDirection.y = 0;
        verticalDirection.y = 0;

        movementDirection = horizontalDirection + verticalDirection;
        rb.velocity = movementDirection * speed * Time.fixedDeltaTime;

        Debug.Log($"Horizontal: {horizontalAxis}");
        Debug.Log($"Vertical: {verticalAxis}");
    }
}
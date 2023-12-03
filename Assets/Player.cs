using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] Transform freelookCamera;

    [SerializeField] float powerUpDuration;

    float horizontalAxis;
    float verticalAxis;
    Vector3 horizontalDirection;
    Vector3 verticalDirection;
    Vector3 movementDirection;

    Coroutine powerUpCoroutine;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;


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

        //Debug.Log($"Horizontal: {horizontalAxis}");
        //Debug.Log($"Vertical: {verticalAxis}");
    }

    public void PickPowerUp()
    {
        Debug.Log("Pick Power UP");
        if(powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }
        powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    IEnumerator StartPowerUp()
    {
        Debug.Log("Start Power Up");
        OnPowerUpStart?.Invoke();
        yield return new WaitForSeconds(powerUpDuration);
        
        Debug.Log("Stop Power Up");
        OnPowerUpStop?.Invoke();
    }
}
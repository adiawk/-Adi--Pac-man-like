using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] int health = 3;
    [SerializeField] float speed;
    [SerializeField] Transform freelookCamera;

    [SerializeField] float powerUpDuration;
    [SerializeField] Transform respawnPoint;
    [SerializeField] TextMeshProUGUI textHealth;


    bool isPowerUpActive;

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

        UpdateUI();
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

    private void OnCollisionEnter(Collision collision)
    {
        if(isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
        
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
        isPowerUpActive = true;
        OnPowerUpStart?.Invoke();
        yield return new WaitForSeconds(powerUpDuration);
        
        Debug.Log("Stop Power Up");
        OnPowerUpStop?.Invoke();
        isPowerUpActive = false;
    }

    void UpdateUI()
    {
        textHealth.text = $"Health: {health}";
    }

    public void Dead()
    {
        health -= 1;
        if(health > 0)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            health = 0;
            Debug.Log("LOSE - PLAYER DEAD");
        }

        UpdateUI();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PICKABLE_TYPE
{
    COIN,
    POWER_UP
}

public class Pickable : MonoBehaviour
{
    public PICKABLE_TYPE type;

    public Action<Pickable> OnPicked;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnPicked?.Invoke(this);
            Debug.Log($"Player Enter Trigger: {type}");
            Destroy(gameObject);
        }
    }
}

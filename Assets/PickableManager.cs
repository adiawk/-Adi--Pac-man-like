using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    public Player player;

    public List<Pickable> _pickableList = new List<Pickable>();

    // Start is called before the first frame update
    void Start()
    {
        InitializePickableList();
    }

    void InitializePickableList()
    {
        Pickable[] pickableObjects = FindObjectsOfType<Pickable>();
        for (int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);

            pickableObjects[i].OnPicked += OnPickablePicked;
        }

        //Debug.Log($"Pickable Amount: {_pickableList.Count}");
    }

    void OnPickablePicked(Pickable pickableObject)
    {
        _pickableList.Remove(pickableObject);

        if(_pickableList.Count <= 0)
        {
            Debug.Log("WIN");
        }

        if(pickableObject.type == PICKABLE_TYPE.POWER_UP)
        {
            player.PickPowerUp();
        }
    }
}

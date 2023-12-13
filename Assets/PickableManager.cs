using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;

    public Player player;

    public List<Pickable> _pickableList = new List<Pickable>();

    // Start is called before the first frame update
    void Start()
    {
        InitializePickableList();

        scoreManager.SetMaxScore(_pickableList.Count);
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

            SceneManager.LoadScene("WinScreen");
        }

        if(pickableObject.type == PICKABLE_TYPE.POWER_UP)
        {
            player.PickPowerUp();
        }

        try
        {
            scoreManager.AddScore();
        }
        catch { };
    }
}

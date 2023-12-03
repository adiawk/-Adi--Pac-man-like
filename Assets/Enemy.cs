using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    BaseState currentState;

    public PatrolState PatrolState = new PatrolState();
    public ChaseState ChaseState = new ChaseState();
    public RetreatState RetreatState = new RetreatState();

    public List<Transform> waypoints = new List<Transform>();

    [HideInInspector]
    public NavMeshAgent navMeshAgent;

    public float chaseDistance;
    public Player player;

    private void Awake()
    {
        currentState = PatrolState;
        currentState.EnterState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if(player != null)
        {
            player.OnPowerUpStart += StartRetreating;
            player.OnPowerUpStop += StopRetreating;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void SwitchState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    void StopRetreating()
    {
        SwitchState(PatrolState);
    }
}

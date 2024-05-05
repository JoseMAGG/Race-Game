using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarAIMovement : MonoBehaviour
{

    public CollisionManager collisionManager;

    private Checkpoint goal;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
    }
    internal int GetDoneCheckpointCount()
    {
        return collisionManager.GetDoneCheckpoints();
    }

    // Update is called once per frame
    void Update()
    {
        goal = collisionManager.nextCheckpoint;
        agent.destination = goal.transform.position;
    }

    public void SetMovementEnabled(bool enabled)
    {
        agent.isStopped = !enabled;
    }
}

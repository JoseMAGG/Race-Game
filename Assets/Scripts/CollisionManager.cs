using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollisionManager : MonoBehaviour
{
    public List<Checkpoint> checkpointList;
    private Checkpoint lastCheckpoint;
    private Checkpoint nextCheckpoint;
    private int checksCompleted;
    private CarMovement movement;

    private void Start()
    {
        movement = GetComponent<CarMovement>();
    }

    internal void SetCheckpoint(Checkpoint checkpoint)
    {
        lastCheckpoint = checkpoint;
        if (checkpoint == nextCheckpoint)
            checksCompleted++;
        int checkIndex = checkpointList.FindIndex(x => x == lastCheckpoint);
        int nextIndex = (checkIndex + 1) % checkpointList.Count;
        nextCheckpoint = checkpointList[nextIndex];

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            transform.position = ShiftRandomly(lastCheckpoint.transform.position);
            transform.rotation = lastCheckpoint.transform.rotation;
            if (movement) movement.Stop();
        }
    }

    private Vector3 ShiftRandomly(Vector3 position)
    {
        position.x += Random.Range(-10, 10);
        position.z += Random.Range(-10, 10);
        return position;
    }

}

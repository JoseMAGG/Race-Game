using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public CollisionManager collisionManager;
    public CarSoundManager soundManager;
    public Rigidbody rigidBody;
    public float force = 2f;
    public float torque = 2f;
    public float maxSpeed = 100f;

    private float speed;
    private bool lockAcceleration;
    //private int gear = 1;
    //private const int MAX_GEAR = 5;
    //private const int MIN_GEAR = 1;

    void Update()
    {
        CheckSpeed();

        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    IncreaseGear();
        //}
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    DecreaseGear();
        //}
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.velocity *= 0.95f;
        }

        if (Input.GetKey(KeyCode.UpArrow) && !lockAcceleration)
        {
            soundManager.PlayRunningSound();
            rigidBody.AddForce(transform.forward * force * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) && !lockAcceleration)
        {
            rigidBody.AddForce(transform.forward * -force * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.up * -torque * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.up * torque * Time.deltaTime);
        }
    }

    //private void DecreaseGear()
    //{
    //    if (gear > MIN_GEAR)
    //        gear--;
    //}

    //private void IncreaseGear()
    //{
    //    if (gear < MAX_GEAR)
    //        gear++;
    //}

    private void CheckSpeed()
    {
        speed = rigidBody.velocity.magnitude;
        Vector3 normalizedVelocity = Vector3.Normalize(rigidBody.velocity);
        float clampedSpeed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
        //float maxGearSpeed = maxSpeed * gear;
        //float minGearSpeed = maxSpeed * (gear - 1);
        //if (!lockAcceleration && (speed > maxGearSpeed * 1.2f || speed < minGearSpeed * 0.8f))
        //    StartCoroutine(BreakEngine());
        //else
            rigidBody.velocity = normalizedVelocity * clampedSpeed;
    }

    private IEnumerator BreakEngine()
    {
        soundManager.PlayOverloadSound();
        float clampedSpeed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);
        rigidBody.velocity = Vector3.Normalize(rigidBody.velocity) * clampedSpeed;
        lockAcceleration = true;
        yield return new WaitForSeconds(3);
        collisionManager.Respawn();
        lockAcceleration = false;
    }

    public void Stop()
    {
        rigidBody.velocity = Vector3.zero;
        //gear = 1;
    }

    internal int GetDoneCheckpointCount()
    {
        return collisionManager.GetDoneCheckpoints();
    }

    internal float GetSpeed()
    {
        return speed;
    }

    //internal int GetGear()
    //{
    //    return gear;
    //}

    //internal float GetStress()
    //{
    //    float maxGearSpeed = maxSpeed * gear * 1.2f;
    //    float minGearSpeed = maxSpeed * (gear - 1) * 0.8f;
    //    return (speed - minGearSpeed) / (maxGearSpeed - minGearSpeed);
    //}
}

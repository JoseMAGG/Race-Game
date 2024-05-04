using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public CollisionManager collisionManager;
    public Rigidbody rigidBody;
    public float force = 2f;
    public float torque = 2f;
    public float maxSpeed = 100f;

    private int gear = 1;
    private float speed;
    private bool lockAcceleration;
    private const int MAX_GEAR = 5;
    private const int MIN_GEAR = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckSpeed();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            IncreaseGear();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            DecreaseGear();
        }
        if (Input.GetKey(KeyCode.C))
        {
            rigidBody.velocity *= 0.95f;
        }

        if (Input.GetKey(KeyCode.UpArrow) && !lockAcceleration)
        {
            rigidBody.AddForce(transform.forward * force);
        }
        if (Input.GetKey(KeyCode.DownArrow) && !lockAcceleration)
        {
            rigidBody.AddForce(transform.forward * -force);
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

    private void DecreaseGear()
    {
        if (gear > MIN_GEAR)
            gear--;
    }

    private void IncreaseGear()
    {
        if (gear < MAX_GEAR)
            gear++;
    }

    private void CheckSpeed()
    {
        speed = rigidBody.velocity.magnitude;
        Vector3 normalizedVelocity = Vector3.Normalize(rigidBody.velocity);
        float maxGearSpeed = maxSpeed * gear;
        float minGearSpeed = maxSpeed * (gear - 1);
        if (!lockAcceleration && (speed > maxGearSpeed * 1.2f || speed < minGearSpeed * 0.8f))
            StartCoroutine(BreakEngine());
        else
            rigidBody.velocity = normalizedVelocity * speed;
    }

    private IEnumerator BreakEngine()
    {
        float clampedSpeed = Mathf.Clamp(speed, -maxSpeed, maxSpeed * gear);
        rigidBody.velocity = Vector3.Normalize(rigidBody.velocity) * clampedSpeed;
        lockAcceleration = true;
        yield return new WaitForSeconds(3);
        collisionManager.Respawn();
        lockAcceleration = false;
    }

    public void Stop()
    {
        rigidBody.velocity = Vector3.zero;
        gear = 1;
    }
}

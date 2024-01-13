using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MyMovement : MonoBehaviour
{
    public float speed = 4;
    public float maxVelocityChange = 18;

    private Vector2 input;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
    }

    private void FixedUpdate()
    {
        rb.AddForce(CalculateMovement(speed), ForceMode.VelocityChange);
    }

    Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVel = new Vector3(input.x, 0, input.y);
        targetVel = transform.TransformDirection(targetVel);

        targetVel *= _speed;

        Vector3 vel = rb.velocity;

        if (input.magnitude > 0.5f)
        {
            Vector3 velChange = targetVel - vel;

            velChange.x = Mathf.Clamp(velChange.x, -maxVelocityChange, maxVelocityChange);
            velChange.z = Mathf.Clamp(velChange.z, -maxVelocityChange, maxVelocityChange);
            velChange.y = 0;

            return (velChange);
        }
        else 
        {
            return new Vector3();
        }
    }
}

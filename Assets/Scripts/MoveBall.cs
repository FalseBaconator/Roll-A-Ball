using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveBall : MonoBehaviour
{

    private Rigidbody rb;
    private float MovementX;
    private float MovementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue MovementValue)
    {
        Vector2 MovementVector = MovementValue.Get<Vector2>();
        MovementX = MovementVector.x;
        MovementY = MovementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 Move = new Vector3(MovementX * 5, 0, MovementY * 5);

        rb.AddForce(Move);
    }

}

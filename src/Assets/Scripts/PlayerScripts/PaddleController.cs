using System;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    /* Class that handles paddle movement
     */
    private Rigidbody2D paddlerb2d;
    [HideInInspector] public bool extended = false;

    private float movement; // variable to take in input value, stored here to access from FixedUpdate()
    private float speed = 500f;

    private void Start()
    {
        paddlerb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal"); // by default, returns -1 if left key is pressed, +1 if right
    }

    private void FixedUpdate()
    {
        /* As FixedUpdate() runs depending on the physics frames, it is a better choice for
         * RigidBody component manipulation aka applying force, changing velocity etc.
         */

        Vector2 target = new Vector2(movement * speed, paddlerb2d.velocity.y);
        paddlerb2d.velocity =
            target * Time.deltaTime; // using Time.deltatime so that speed does not depend on framerate
    }

    public void Unlock()
    {
        LevelManager.Instance.ResetPositions();
    }
}
using System;
using Interfaces;
using UnityEngine;

public class PuckController : MonoBehaviour
{
    /* Controls puck behaviour and its triggers
     */
    private GameObject _paddleObj;
    [HideInInspector] public Rigidbody2D _puckrb2d;
    private Transform _paddleTransform;
    private Vector2 _lastVelocity;
    public float _speed = 8f;

    private AudioSource _brickCollisionSound;

    private void Start()
    {
        _paddleObj = GameObject.Find("Paddle");
        _puckrb2d = GetComponent<Rigidbody2D>();
        _paddleTransform = _paddleObj.GetComponent<Transform>();
        _brickCollisionSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!LevelManager.Instance._isActive)
        {
            transform.position = new Vector2(_paddleTransform.position.x, _paddleTransform.position.y + 1.5f);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                LevelManager.Instance._isActive = true;
                // puckrb2d.velocity = (transform.forward - transform.right).normalized * speed;
                _puckrb2d.velocity = new Vector2(-_speed, _speed);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                LevelManager.Instance._isActive = true;
                //puckrb2d.velocity = (transform.forward + transform.right).normalized * speed;
                _puckrb2d.velocity = new Vector2(_speed, _speed);
            }
        }
    }

    private void FixedUpdate()
    {
        _lastVelocity = _puckrb2d.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _brickCollisionSound.Play();
        _lastVelocity = Vector3.Reflect(_lastVelocity, other.contacts[0].normal);
        _puckrb2d.velocity = _lastVelocity;
        // Debug.Log(_puckrb2d.velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            LevelManager.Instance.LifeHandler();
        }
    }
    
}
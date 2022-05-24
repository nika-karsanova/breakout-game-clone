using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    /* Singleton class that handles HUD mechanics
     */
    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
            }

            return instance;
        }
    }

    [SerializeField] private Transform _lifeGrouper;
    [SerializeField] private GameObject _lifePrefab;
    private List<GameObject> _lives = new List<GameObject>();
    private GameObject _puckObj;
    private GameObject _paddleObj;
    private Rigidbody2D _paddlerb2d;
    [HideInInspector] public Rigidbody2D _puckrb2d;
    private Transform _paddleTransform;
    internal bool _isActive;
    private Animator _paddleanim;
    private bool grownPaddle;

    private int _remainingLives = 3;

    private void Start()
    {
        _puckObj = GameObject.Find("Puck");
        _paddleObj = GameObject.Find("Paddle");

        _paddlerb2d = _paddleObj.GetComponent<Rigidbody2D>();
        _puckrb2d = _puckObj.GetComponent<Rigidbody2D>();
        _paddleTransform = _paddleObj.GetComponent<Transform>();
        _paddleanim = _paddleObj.GetComponent<Animator>();

        InstantiateLives(_remainingLives);
    }

    public void InstantiateLives(int lives)
    {
        /*
         * Instantiates required number of lifes in HUD
         */
        for (int i = 0; i < lives; i++)
        {
            _lives.Add(Instantiate(_lifePrefab, _lifeGrouper));
        }
    }

    public void RemoveLives(int lifeidx)
    {
        /* Removes lifes from HUD but setting them inactive
         */
        _lives[lifeidx].SetActive(false);
    }

    public void LifeHandler()
    {
        if (_paddleanim.enabled) // if paddle big
        {
            grownPaddle = true;
            _paddleanim.Play("Base Layer.PaddleContraction");
        }
        else
        {
            RemoveLives(--_remainingLives);
            Debug.Log("You lost! Remaining lives: " + _remainingLives);
        }

        if (!grownPaddle) // if no paddle size powerup were picked up
        {
            ResetPositions();
        }
    }

    public void ResetPositions()
    {
        /* Resets position of paddle and the puck upon round end 
         */
        _paddleanim.enabled = false;
        grownPaddle = false; // disable animator so that powerup runs properly
        _puckrb2d.velocity = Vector2.zero;
        _paddlerb2d.velocity = Vector2.zero;
        _paddleTransform.position = new Vector3(0, _paddleTransform.position.y, 0);
        _isActive = false;

        if (_remainingLives == 0)
        {
            UIManager.Instance.DisplayFailScreen();
        }
    }
}
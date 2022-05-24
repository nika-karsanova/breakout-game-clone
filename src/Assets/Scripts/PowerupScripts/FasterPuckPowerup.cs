using Interfaces;
using UnityEngine;

public class FasterPuckPowerup : MonoBehaviour, IPowerup
{
    /* Decsribes faster powerup behaviour
     */
    private Rigidbody2D _puckrb2d;
    private float _puckSpeed;
    private float multiplier = 5f;
    private float powerup;

    private void Start()
    {
        _puckrb2d = GameObject.Find("Puck").GetComponent<Rigidbody2D>();
        _puckSpeed = GameObject.Find("Puck").GetComponent<PuckController>()._speed;
    }

    public void ApplyPowerup(Collider2D collider = null)
    {
        /* Multiplies the set puck speed by the given multiplier
         */

        if (_puckrb2d.velocity.x < 15f && _puckrb2d.velocity.y < 15f)
        {
            powerup = _puckSpeed * multiplier;
            _puckrb2d.velocity *= powerup;
        }

        Destroy(this.gameObject);
    }
}
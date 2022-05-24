using Interfaces;
using UnityEngine;

public class ExplosiveBrick : Brick, IInteraction
{
    /* Instance of explosive brick
     */
    [SerializeField] private GameObject explosionEffect;
    Collider2D[] _collidedbricks = new Collider2D[8]; // Might need to be bigger to account for powerups?
    private Collider2D _brickc2d;

    protected new void Start()
    {
        base.Start();
        SetPowerup(powerupPrefabs);
        _brickc2d = gameObject.GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ObjectInteraction();
    }

    private void Explode()
    {
        /* Function that handles the explosion of the bricks
         */
        _collidedbricks =
            Physics2D.OverlapBoxAll(transform.position, new Vector2(2.6f, 2.3f), 0.0f); // Colliders "in the area"

        _brickc2d.enabled = false; // This prevents infinite recursion from happening when 2 explosive bricks collide

        foreach (Collider2D obj in _collidedbricks)
        {
            if (obj == null || obj.CompareTag("Puck") || obj.gameObject.CompareTag("PermaBlock") ||
                ReferenceEquals(obj, _brickc2d))
                continue; // Do not blow up puck, perma block and do not blow up yourself twice

            var interaction = obj.GetComponent<IInteraction>();
            if (interaction == null) continue;
            interaction.ObjectInteraction(); // every fitting object to call objectInteraction()
        }

        GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        Destroy(this.gameObject);
    }

    public new void ObjectInteraction()
    {
        base.ObjectInteraction();
        Explode();
    }
}
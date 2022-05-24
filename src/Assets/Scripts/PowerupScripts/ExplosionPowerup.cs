using Interfaces;
using UnityEngine;

public class ExplosionPowerup : MonoBehaviour, IPowerup
{
    /* Describes explosive powerup behaviour 
     */
    [SerializeField] private GameObject explosionEffect;
    Collider2D[] _collidedbricks = new Collider2D[8];

    private void Start()
    {
    }

    public void ApplyPowerup(Collider2D collider = null)
    {
        /*This function is very similar to the one for explosive brick, so ideally it would be separated into its 
        own method to avoid code duplication. Overall, behaves similarly to Explode, with the sole difference of
        removing collider of the block powerup is one as opposed to the one of the block*/
        _collidedbricks = Physics2D.OverlapBoxAll(transform.position, new Vector2(2.6f, 2.3f), 0.0f);
        collider.enabled = false;

        foreach (Collider2D obj in _collidedbricks)
        {
            if (obj == null || obj.CompareTag("Puck") || obj.gameObject.CompareTag("PermaBlock") ||
                ReferenceEquals(obj, collider)) continue;

            var interaction = obj.GetComponent<IInteraction>();
            if (interaction == null) continue;
            interaction.ObjectInteraction();
        }

        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(explosion, 1f);
        Destroy(this.gameObject);
    }
}
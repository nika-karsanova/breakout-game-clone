using Interfaces;
using UnityEngine;

public class GrowPaddlePowerup : MonoBehaviour, IPowerup
{
    /* Describes paddle extension powerup
     */
    private Animator paddleanim;

    private void Start()
    {
        paddleanim = GameObject.Find("Paddle").GetComponent<Animator>();
    }

    public void ApplyPowerup(Collider2D collider = null)
    {
        /* Enables the animator and makes it play the correct animation
         */
        paddleanim.enabled = true;
        paddleanim.Play("Base Layer.PaddleExtension");

        Destroy(this.gameObject);
    }
}
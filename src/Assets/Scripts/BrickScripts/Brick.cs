using System.Collections.Generic;
using Interfaces;
using UnityEngine;

// using rnd = System.Random;


public class Brick : MonoBehaviour, IInteraction
{
    /* Parent class of Brick. Represents a basic brick, acts like an intermediate for Powerup application onto bricks.
  */
    [SerializeField] protected List<GameObject> powerupPrefabs;
    protected GameObject powerup;
    protected bool hasPowerup;

    protected BrickManager brickManager;
    // private rnd rnd;

    protected void Start()
    {
        // rnd = new rnd();
        brickManager = GameObject.Find("BrickSpawn").GetComponent<BrickManager>();
    }

    protected void SetPowerup(List<GameObject> powerupPrefabs)
    {
        /* Randomly instantiates a gameObject from one of the powerup prefabs in the centre of the brick.
         * If brick gets a powerup, hasPowerup = true.
         */
        int idx = Random.Range(0, powerupPrefabs.Count);
        int chance = Random.Range(0, 101);
        if (chance < 40)
        {
            powerup = Instantiate(powerupPrefabs[idx], transform.position, transform.rotation);
            hasPowerup = true;
        }
    }

    public void ObjectInteraction()
    {
        /* If brick has powerup, calls its effect before removing hasPowerup status.
         * Follow with removing brick from brickManager.
         */
        if (hasPowerup)
        {
            var interaction = powerup.GetComponent<IPowerup>();
            if (interaction == null) return;
            interaction.ApplyPowerup(this.GetComponent<Collider2D>());
            hasPowerup = false;
        }

        brickManager.RemoveBrick(this.gameObject);
    }
}
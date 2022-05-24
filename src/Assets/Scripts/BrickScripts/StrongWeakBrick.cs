using Interfaces;
using UnityEngine;

public class StrongWeakBrick : Brick, IInteraction
{
    /* Instance of strong brick, which goes weak after one hit
     */
    [SerializeField] private Sprite weak_sprite;
    private bool isStrong = true;
    private SpriteRenderer _bricksr;

    protected new void Start()
    {
        base.Start();
        SetPowerup(powerupPrefabs);
        _bricksr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ObjectInteraction();
    }

    private void Destroy()
    {
        /* Flips bool and sprite on first hit, destroys brick on second
         */
        switch (isStrong)
        {
            case true:
                isStrong = !isStrong;
                _bricksr.sprite = weak_sprite;
                break;
            case false:
                Destroy(this.gameObject);
                break;
        }
    }

    public new void ObjectInteraction()
    {
        if (!isStrong)
        {
            base.ObjectInteraction();
        }

        Destroy();
    }
}
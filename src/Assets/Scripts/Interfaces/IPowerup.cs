using UnityEngine;

namespace Interfaces
{
    public interface IPowerup
    {
        /* Interface that deals with the powerup application. Triggered by collision of a puck with brick
         */
        void ApplyPowerup(Collider2D collider=null);
    }
}
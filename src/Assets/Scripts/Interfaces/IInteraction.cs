using UnityEngine;

namespace Interfaces
{
    public interface IInteraction
    {
        /* Interface that Addresses the problem of different behaviours upon brick destructions.
         Can be easily extended to accommodate for either more types of bricks or some other
         collidable objects.
         */
        void ObjectInteraction();

    }
}
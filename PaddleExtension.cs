using UnityEngine;

public class PaddleExtension : StateMachineBehaviour
{
    /** This class is responsible for resizing the collider to the bigger sprite after extension animation plays.
  */
    private SpriteRenderer paddlesprite;

    private BoxCollider2D paddlec2d;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        paddlesprite = GameObject.Find("Paddle").GetComponent<SpriteRenderer>();
        paddlec2d = GameObject.Find("Paddle").GetComponent<BoxCollider2D>();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /* Resizing box collider of the Paddle to account for bigger size
         */
        Vector3 new_borders = paddlesprite.bounds.size;
        paddlec2d.size = new_borders;
    }
}
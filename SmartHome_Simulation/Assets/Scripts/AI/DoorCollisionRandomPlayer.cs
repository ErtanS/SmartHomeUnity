using UnityEngine;
using System.Collections;

public class DoorCollisionRandomPlayer : MonoBehaviour
{
    Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

	/// <summary>
	/// Raises the collision stay event.
	/// </summary>
	/// <param name="collision">Collision.</param>
    void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag.Equals(Config.OBJ_NAME_RANDOM_PLAYER) &&
            animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_DOOR_IDLE))
        {
            animator.SetTrigger(Config.ANIMATION_TRIGGER_OPEN);
            StartCoroutine(closeAnimation());
        }
    }

	/// <summary>
	/// Closes the Door.
	/// </summary>
	/// <returns>The animation.</returns>
    private IEnumerator closeAnimation()
    {
        yield return new WaitForSeconds(1);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(Config.ANIMATION_DOOR_OPEN_IDLE))
        {
            animator.SetTrigger(Config.ANIMATION_TRIGGER_CLOSE);
        }
    }
}
using UnityEngine;
using System.Collections;

public class DoorHandler : MonoBehaviour
{
    public bool unlocked = false;
    public bool opening = false;

    Collider2D col;
    Animator anim;
    bool hasPlayed = false;

    void Start()
    {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        // Stay on first frame until unlocked
        anim.Play("DoorOpen", 0, 0f);
        anim.speed = 0f;
    }

    void FixedUpdate()
    {
        if (opening && unlocked && !hasPlayed)
        {
            PlayOpenAnimation();
        }
    }

    void PlayOpenAnimation()
    {
        hasPlayed = true;
        anim.speed = 1f;   // begin playing animation normally
        StartCoroutine(DisableColliderMidway());
    }

    IEnumerator DisableColliderMidway()
	{
		// Wait one frame so Animator updates the state
		yield return null;

		// Get the real animation length
		float clipLength = anim.GetCurrentAnimatorStateInfo(0).length;

		// Wait until halfway through the animation
		float halfTime = clipLength * 0.5f;
		yield return new WaitForSeconds(halfTime);

		col.enabled = false;

		// Wait until end
		float remaining = clipLength - halfTime;
		yield return new WaitForSeconds(remaining);

		// Freeze at last frame
		anim.speed = 0f;
	}

}

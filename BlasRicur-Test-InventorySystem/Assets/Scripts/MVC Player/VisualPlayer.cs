using UnityEngine;

public class VisualPlayer : VisualCharacter
{
    bool startPlayingWalk;

    public override void Move(float x, float y)
    {
        //I could do sprite.flipX = (x < 0), but that would flip it if i´M going left then up (it should stay fliped in that situation)
        if (x < 0)
            sprite.flipX = true;
        else if (x > 0)
            sprite.flipX = false;

        if ((new Vector2(x,y).magnitude >= 0.3f))
        {
            if (!startPlayingWalk)
            {
                animator.Play("WalkingAnim");
                startPlayingWalk = true;
            }
        }

        else
        {
            startPlayingWalk = false;
            animator.Play("Idle");
        }
    }
}

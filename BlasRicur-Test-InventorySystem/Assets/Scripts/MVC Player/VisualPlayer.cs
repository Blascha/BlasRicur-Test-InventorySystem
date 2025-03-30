using System.Collections;
using UnityEngine;

public class VisualPlayer : VisualCharacter
{
    bool startPlayingWalk;
    [SerializeField] AudioSource walk;
    [SerializeField] AudioSource hurt;
    [SerializeField] Transform cameraTransform;
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
                walk.Play();
                animator.Play("WalkingAnim");
                startPlayingWalk = true;
            }
        }

        else
        {
            walk.Stop();
            startPlayingWalk = false;
            animator.Play("Idle");
        }
    }

    public override void Knockback(float Multiplyier)
    {
        base.Knockback(Multiplyier);
        hurt.Play();
        StopCoroutine("CamShake");
        StartCoroutine(CamShake());
    }
    IEnumerator CamShake()
    {
        WaitForSeconds wait = new WaitForSeconds(.02f);

        for (float i = .1f; i >= 0.001f; i = Mathf.Lerp(i, 0, .5f))
        {
            cameraTransform.localPosition = new Vector3(Random.Range(-i, i), Random.Range(-i, i),-10);
            yield return wait;
        }

        cameraTransform.localPosition = Vector3.forward * -10;
    }
}

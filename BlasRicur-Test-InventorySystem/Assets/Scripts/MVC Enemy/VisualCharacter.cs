using UnityEngine;
using System.Collections;

public class VisualCharacter : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Animator animator;
    Color oldColor;

    private void Start()
    {
        oldColor = sprite.color;
    }
    public void Knockback(float Multiplyier)
    {
        StopCoroutine("KnockbackAnimation");
        StartCoroutine(KnockbackAnimation(1 / Multiplyier));
    }

    public virtual void Move(float x, float y)
    {

    }

    IEnumerator KnockbackAnimation(float time = .5f)
    {
        WaitForSeconds wait = new WaitForSeconds(0.08f);

        for (float i = 0; i < time; i += 0.16f)
        {
            sprite.color = oldColor;
            yield return wait;

            sprite.color = Color.red;
            yield return wait;
        }

        sprite.color = oldColor;
    }

    public void Die()
    {
        animator.Play("Death Anim");
        Destroy(this);
    }
}

using UnityEngine;
using System.Collections;

public class VisualCharacter : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected Animator animator;

    public void Knockback(float Multiplyier)
    {
        StartCoroutine(KnockbackAnimation(1 / Multiplyier));
    }

    IEnumerator KnockbackAnimation(float time = .5f)
    {
        WaitForSeconds wait = new WaitForSeconds(0.08f);
        Color oldColor = sprite.color;

        for (float i = 0; i < time; i += 0.16f)
        {
            sprite.color = oldColor;
            yield return wait;

            sprite.color = Color.red;
            yield return wait;
        }

        sprite.color = oldColor;
    }
}

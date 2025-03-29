using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ModelCharacter : MonoBehaviour
{
    [SerializeField] protected float HP = 10;
    [SerializeField] protected float acceleration = 5;
    [SerializeField] protected float maxSpeed = 5;
    protected Rigidbody2D rig;
    [SerializeField] protected bool onKnockback = false;
    [SerializeField] protected VisualPlayer visual;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public virtual void Move(float x, float y)
    {
        if (!onKnockback)
        {
            rig.AddForce((Vector2.right * x + Vector2.up * y) * acceleration);
            rig.linearVelocity = Vector2.ClampMagnitude(rig.linearVelocity, maxSpeed);
        }
    }

    public virtual void Damage(float damage, Vector2 direction, float knockback)
    {
        HP -= damage;

        if(HP <= 0)
        {
            Die();
            return;
        }

        onKnockback = true;
        rig.AddForce(direction.normalized * knockback, ForceMode2D.Impulse);
        StartCoroutine(WaitToStopKnockBack());
    }

    protected IEnumerator WaitToStopKnockBack(float time = .1f)
    {
        yield return new WaitForSeconds(time);
        onKnockback = false;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}

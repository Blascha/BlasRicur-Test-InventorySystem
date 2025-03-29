using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ModelCharacter : MonoBehaviour
{
    [SerializeField] protected float MaxHP = 10;
    [SerializeField] protected float HP = 10;
    [SerializeField] protected float acceleration = 5;
    [SerializeField] protected float maxSpeed = 5;
    protected Rigidbody2D rig;
    [SerializeField] protected bool onKnockback = false;
    [SerializeField] protected VisualCharacter visual;
    [SerializeField] ParticleSystem particles;

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

        if (HP <= 0)
        {
            Die();
            return;
        }

        onKnockback = true;
        rig.AddForce(direction.normalized * knockback, ForceMode2D.Impulse);
        visual.Knockback(knockback);
        StartCoroutine(WaitToStopKnockBack());
    }

    protected IEnumerator WaitToStopKnockBack(float time = .1f)
    {
        yield return new WaitForSeconds(time);
        onKnockback = false;
    }

    protected virtual void Die()
    {
        SpawnManager.EnemyDeath(gameObject);
        GetComponent<AudioSource>().Play();
        particles.Play();
        rig.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        if (Random.Range(0, SpawnManager.Wave * 10) == 1)
            (Instantiate(Resources.Load("Floor Symbol")) as GameObject).transform.position = transform.position;

        StartCoroutine(WaitToDestroy());
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Destroy(gameObject);
    }
}

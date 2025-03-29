using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator hitAnimator;
    [SerializeField] ParticleSystem particles;

    public float Damage;
    public float ShotResilience;

    [SerializeField] float enemyKnockback = 1;
    public float EnemyKnockbackMultiplyier;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShotResilience --;

        if(collision.TryGetComponent(out ModelCharacter model))
        {
            model.Damage(Damage, transform.up, EnemyKnockbackMultiplyier * enemyKnockback);
        }

        if(ShotResilience <= 0.001f)
        {
            StartCoroutine(WaitAndDestroy());
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            particles.Play();
            hitAnimator.Play("IdleVFX");
            speed = 0;
        }
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }
}

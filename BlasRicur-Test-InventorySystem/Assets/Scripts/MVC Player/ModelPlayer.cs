using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPlayer : ModelCharacter
{
    [SerializeField] GameObject projectile;
    public static ModelPlayer Instance;
    public ControllerPlayer Controler;
    float damageMultiplyier;
    float HPMultiplyier;

    public float GetHP()
    {
        return HP * HPMultiplyier;
    }

    float playerKnockbackMultiplyier = 1;
    float enemyKnockbackMultiplyier = 1;
    float shotResilienceMultiplyier = 1;

    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        Instance = this;
    }
    public override void Damage(float damage, Vector2 direction, float knockback = 1)
    {
        HP -= damage;

        onKnockback = true;
        rig.AddForce(direction.normalized / (playerKnockbackMultiplyier * knockback), ForceMode2D.Impulse);
        visual.Knockback(playerKnockbackMultiplyier);
        StartCoroutine(WaitToStopKnockBack(.05f));
    }

    public override void Move(float x, float y)
    {
        if (!onKnockback)
        {
            rig.AddForce((Vector2.right * x + Vector2.up * y) * acceleration);
            rig.linearVelocity = Vector2.ClampMagnitude(rig.linearVelocity, maxSpeed);
            visual.Move(x,y);
        }
    }

    public void Fire(Vector2 direction)
    {
        GameObject actualProjectile = Instantiate(projectile);

        actualProjectile.transform.position = transform.position + (Vector3)direction.normalized * .16f;
        actualProjectile.transform.up = direction;
        Projectile projectileComponent = actualProjectile.GetComponent<Projectile>();
        projectileComponent.Damage = damageMultiplyier;
        projectileComponent.ShotResilience = shotResilienceMultiplyier;
        projectileComponent.EnemyKnockbackMultiplyier = enemyKnockbackMultiplyier;
    }

    #region Setters
    public void SetNewROFMultiplyier(float newMultiplyier)
    {
        Controler.ROFMultiplyier = newMultiplyier;
    }

    public void SetDamageMultiplyier(float newMultiplyier)
    {
        damageMultiplyier = newMultiplyier;
    }

    public void SetNewHP(float newHP)
    {
        HP = newHP;
    }

    public void SetNewPlayerKnockback(float newPlayerKnockback)
    {
        playerKnockbackMultiplyier = newPlayerKnockback;
    }
    public void SetNewEnemyKnockback(float newEnemyKnockback)
    {
        enemyKnockbackMultiplyier = newEnemyKnockback;
    }
    public void SetNewShotResilience(float newShotResilience)
    {
        shotResilienceMultiplyier = newShotResilience;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPlayer : ModelCharacter
{
    float originalMaxHP;
    [SerializeField] GameObject projectile;
    public static ModelPlayer Instance;
    public ControllerPlayer Controler;
    float damageMultiplyier;

    float playerKnockbackMultiplyier = 1;
    float enemyKnockbackMultiplyier = 1;
    float shotResilienceMultiplyier = 1;

    // Start is called before the first frame update
    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        Instance = this;
        originalMaxHP = MaxHP;
        StartCoroutine(ConstantlyHeal());
        PlayerUI.Instance.UpdateLife((int)MaxHP);
    }
    public override void Damage(float damage, Vector2 direction, float knockback = 1)
    {
        HP -= damage;

        PlayerUI.Instance.UpdateLife((int)HP);

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

    IEnumerator ConstantlyHeal()
    {
        while (true)
        {
            if (HP < MaxHP)
            {
                yield return new WaitForSeconds(5);
                HP++;
                PlayerUI.Instance.UpdateLife((int)HP);
            }
            yield return new WaitForFixedUpdate();
        }
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
        MaxHP = newHP * originalMaxHP;
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

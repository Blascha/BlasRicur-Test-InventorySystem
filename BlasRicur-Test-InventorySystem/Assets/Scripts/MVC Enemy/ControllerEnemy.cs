using UnityEngine;

public class ControllerEnemy : MonoBehaviour
{
    [SerializeField] ModelCharacter model;
    float damageCooldown = 0;
    [SerializeField] float ROF = 2;
    [SerializeField] float range;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 dir = (ModelPlayer.Instance.transform.position - transform.position);
        damageCooldown += Time.fixedDeltaTime;

        if (dir.magnitude <= range && ROF < damageCooldown)
        {
            damageCooldown = 0;
            ModelPlayer.Instance.Damage(1, dir, 1);
        }

        dir = dir.normalized;
        model.Move(dir.x, dir.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range * range);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ModelPlayer : MonoBehaviour
{
    Rigidbody2D rig;
    [SerializeField] float acceleration = 5;
    [SerializeField] float maxSpeed = 5;
    [SerializeField] GameObject projectile;
    public static ModelPlayer Instance;
    public ControllerPlayer Controler;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Instance = this;
    }

    public void Move(float x, float y)
    {
        rig.AddForce((Vector2.right * x + Vector2.up * y) * acceleration);
        rig.linearVelocity = Vector2.ClampMagnitude(rig.linearVelocity, maxSpeed);
    }

    public void Fire(Vector2 direction)
    {
        GameObject actualProjectile = Instantiate(projectile);

        actualProjectile.transform.position = transform.position + (Vector3)direction.normalized * .16f;
        actualProjectile.transform.up = direction;
    }

    public void SetNewROFMultiplyier(float newMultiplyier)
    {
        Controler.ROFMultiplyier = newMultiplyier;
    }
}

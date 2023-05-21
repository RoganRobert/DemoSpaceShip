using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HomingMissile : MonoBehaviour
{
    [Tooltip("Damage which a projectile deals to another object. Integer")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
    public bool enemyMissile;

    [Tooltip("Whether the projectile is destroyed in the collision, or not")]
    public bool destroyedByCollision;

    [Tooltip("Damage hit effect")]
    public GameObject hitEffect;

    public Vector2 direction = new Vector2(0, 1);
    public GameObject[] enemy;

    public float speed = 5f;

    public float rotateSpeed = 200f;
    List<GameObject> enemyList = new List<GameObject>();

    private Rigidbody2D rb;
    public Vector2 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        velocity = direction * speed;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < enemy.Length; i++)
        {
            
            Vector2 direction = (Vector2)enemy[i].transform.position - rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * rotateSpeed;

            rb.velocity = transform.up * speed;
            enemyList.Add(enemy[i]);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        if (enemyMissile && collision.tag == "Player") //if anoter object is 'player' or 'enemy sending the command of receiving the damage
        {
            SpaceshipController.instance.GetDamage(damage);
            if (destroyedByCollision)
                Destruction();
        }
        else if (!enemyMissile && collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            if (destroyedByCollision)
                Instantiate(hitEffect, transform.position, transform.rotation);
            Destruction();
        }
        else if (!enemyMissile && collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().projectileHitPos = transform.position;
            collision.GetComponent<Boss>().GetDamage(damage);
            if (destroyedByCollision)
                Destruction();
        }
    }

    void Destruction()
    {
        Destroy(gameObject);
    }
}
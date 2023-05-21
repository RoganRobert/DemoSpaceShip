using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines 'Enemy's' health and behavior. 
/// </summary>
public class Boss : MonoBehaviour {

    #region FIELDS
    [Tooltip("Health points in float")]
    float health, maxHealth = 700;

    public Image barHealth;
    //public int health;

    [Tooltip("Enemy's projectile prefab")]
    public GameObject Projectile;
    
    [Tooltip("VFX prefab generating after destruction")]
    public GameObject destructionVFX;
    public GameObject hitEffect;

    [HideInInspector] public Vector3 projectileHitPos;
    [HideInInspector] public int shotChance; //probability of 'Enemy's' shooting during tha path
    [HideInInspector] public float shotTimeMin, shotTimeMax; //max and min time for shooting from the beginning of the path

    #endregion

    private void Start()
    {


      Invoke("ActivateShooting", Random.Range(shotTimeMin, shotTimeMax));
      health = maxHealth;
        
    }

    private void Update()
    {
        if (health > maxHealth) health = maxHealth;
        HealthBarFillter();
        
    }
    //coroutine making a shot
    void ActivateShooting() 
    {
        if (Random.value < (float)shotChance / 100)                             //if random value less than shot probability, making a shot
        {
          
          Instantiate(Projectile,  gameObject.transform.position, Quaternion.identity);             
        }
    }

    //method of getting damage for the 'Enemy'
    public void GetDamage(int damage) 
    {
        if(health > 0)
        {
            health -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
        }
        if (health <= 0)
            Destruction();
        else
            Instantiate(hitEffect, projectileHitPos, Quaternion.identity,transform);
    }    

    //if 'Enemy' collides 'Player', 'Player' gets the damage equal to projectile's damage value
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Projectile.GetComponent<Projectile>() != null)
                SpaceshipController.instance.GetDamage(Projectile.GetComponent<Projectile>().damage);
            else
                SpaceshipController.instance.GetDamage(1);
        }
    }

    //method of destroying the 'Enemy'
    void Destruction()                           
    {        
        Instantiate(destructionVFX, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }

    void HealthBarFillter()
    {
        barHealth.fillAmount = health / maxHealth;
    }
}

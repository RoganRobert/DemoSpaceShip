using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script defines which sprite the 'Player" uses and its health.
/// </summary>

public class SpaceshipController : MonoBehaviour
{
    float health, maxHealth = 100;
    public GameObject destructionFX;
    //public GameObject hitEffect;
    //public GameObject[] hit;
    public Image ringHealth;
    public Text healthText;


    private bool shielded;
    [SerializeField]
    private GameObject shield;

    public static SpaceshipController instance;

    private void Update()
    {
        //healthText.text = health + "%";

        if (health > maxHealth) health = maxHealth;
        healthText.text = health + "%";
        ColorChange();
        HealthBarFillter();
    }
    private void Awake()
    {
        if (instance == null) 
            instance = this;        
        health = maxHealth;
        healthText.text = health + "%";
        DeactiveShield();
        DeactiveHealth();
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(float damage)
    {
        if (!shielded)
        {
            if(health > 0)
            {
                health -= damage; //reducing health for damage value, if health is less than 0, starting destruction procedure
                ActiveHealth();
            }
            
        }
        if (health <= 0)
            Destruction();
        //else
        //    for (int i = 0; i < hit.Length; i++)
        //    {
        //        Instantiate(hit[i].gameObject, hit[i].transform.position, Quaternion.identity, transform);
        //    }
            
        //Destruction();
    }    

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);           

    }

    public void ActiveShield()
    {
        shield.SetActive(true);
        shielded = true;
        Invoke("DeactiveShield", 8f);
    }

    public void DeactiveShield()
    {
        shield.SetActive(false);
        shielded = false;
    }

    public void ActiveHealth()
    {
        ringHealth.gameObject.SetActive(true);
        healthText.gameObject.SetActive(true);
        Invoke("DeactiveHealth", 2f);
    }

    public void DeactiveHealth()
    {
        ringHealth.gameObject.SetActive(false);
        healthText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShieldUp shieldUp = collision.GetComponent<ShieldUp>();
        if (shieldUp)
        {
            if (shieldUp.activateShield)
            {
                ActiveShield();
            }
            Destroy(shieldUp.gameObject);
        }
    }

    void HealthBarFillter()
    {
        ringHealth.fillAmount = health / maxHealth;
    }

    void ColorChange()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        ringHealth.color = healthColor;
    }
}

















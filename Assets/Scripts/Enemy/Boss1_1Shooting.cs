using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_1Shooting : MonoBehaviour
{
    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    private float _fireDelay;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    //time for a new shot
    [HideInInspector] public float nextFire;


    [Tooltip("current weapon power")]
    [Range(1, 4)]       //change it if you wish
    public int weaponPower = 1;

    public Guns guns;
    bool shootingIsActive = true;
    [HideInInspector] public int maxweaponPower = 4;
    public static SpaceshipShooting instance;

    // Start is called before the first frame update
    void Start()
    {
        _fireDelay = 2f;
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootingIsActive)
        {
            if (Time.time > nextFire)
            {
                _fireDelay -= Time.deltaTime;
                if (_fireDelay <= 0)
                {
                    nextFire = Time.time + 1 / fireRate;
                    _fireDelay = 2f;
                }
            }
            else
            {
                CreateLazerShot(projectileObject, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGunVFX.transform.position, Vector3.zero);
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.rightGunVFX.transform.position, Vector3.zero);
                guns.rightGunVFX.Play();
            }
        }
    }

    void CreateLazerShot(GameObject lazer, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {

        Instantiate(lazer, pos, Quaternion.Euler(rot));
    }
}

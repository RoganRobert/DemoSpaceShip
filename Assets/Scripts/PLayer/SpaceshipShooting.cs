﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//guns objects in 'Player's' hierarchy
[System.Serializable]
public class Guns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX; 
}

public class SpaceshipShooting : MonoBehaviour {

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    //public GameObject projectileObject;
    public string player_pojectile;

    //time for a new shot
    [HideInInspector] public float nextFire;


    [Tooltip("current weapon power")]
    [Range(1, 4)]       //change it if you wish
    public int weaponPower = 1; 

    public Guns guns;
    bool shootingIsActive = true;

    [HideInInspector]public int maxweaponPower = 4;

    public static SpaceshipShooting instance;

    private PoolManager _poolManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //receiving shooting visual effects components
        _poolManager = PoolManager.Instance;
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();

    }

    private void Update()
    {
        if (shootingIsActive)
        {
            if (Time.time > nextFire)
            {
                MakeAShot();
                
                nextFire = Time.time + 1 / fireRate;
            }

        }

    }

    //method for a shot
    void MakeAShot() 
    {

        switch (weaponPower) // according to weapon power 'pooling' the defined anount of projectiles, on the defined position, in the defined rotation
        {
            case 1:
                CreateLazerShot(player_pojectile, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();

                break;
            case 2:
                CreateLazerShot(player_pojectile, guns.rightGun.transform.position, Vector3.zero);
                guns.leftGunVFX.Play();
                CreateLazerShot(player_pojectile, guns.leftGun.transform.position, Vector3.zero);
                guns.rightGunVFX.Play();
                break;
            case 3:
                CreateLazerShot(player_pojectile, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                CreateLazerShot(player_pojectile, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX.Play();
                CreateLazerShot(player_pojectile, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                break;
            case 4:
                CreateLazerShot(player_pojectile, guns.centralGun.transform.position, Vector3.zero);
                guns.centralGunVFX.Play();
                CreateLazerShot(player_pojectile, guns.rightGun.transform.position, new Vector3(0, 0, -5));
                guns.leftGunVFX.Play();
                CreateLazerShot(player_pojectile, guns.leftGun.transform.position, new Vector3(0, 0, 5));
                guns.rightGunVFX.Play();
                CreateLazerShot(player_pojectile, guns.leftGun.transform.position, new Vector3(0, 0, 15));
                CreateLazerShot(player_pojectile, guns.rightGun.transform.position, new Vector3(0, 0, -15));
                break; 

        }

    }

    void CreateLazerShot(string lazer/*GameObject lazer*/, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
      GameObject b = GameObject.FindGameObjectWithTag("Pool");
      b = _poolManager.SpawnFromPool(lazer, pos, Quaternion.Euler(rot));
    //Instantiate(lazer, pos, Quaternion.Euler(rot));
    }

}
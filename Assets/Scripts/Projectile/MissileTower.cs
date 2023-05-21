using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTower : MonoBehaviour
{

    public Transform[] spwanPoints;
    public GameObject homingMissile;

    int index = 0;

    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {

        Invoke("SpwanMissile", 3f);
        //Attack();
        //coolDownTimer += Time.deltaTime;

        direction = (transform.localRotation * Vector2.up).normalized;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void  SpwanMissile()
    {
        GameObject newhomingMissile = Instantiate(homingMissile, spwanPoints[index].position, spwanPoints[index].rotation) as GameObject;
        HomingMissile goMissile = newhomingMissile.GetComponent<HomingMissile>();
        goMissile.direction = direction;
        //newhomingMissile.GetComponent<HomingMissile>();
        if (index < spwanPoints.Length)
        {
            index++;

        }
        if (index == 2)
        {

            index = 0;
        }
        Invoke("SpwanMissile", 2f);
    }

}
    
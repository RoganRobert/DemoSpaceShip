using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileTime : MonoBehaviour
{
  public float timeDestroy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  private void OnEnable()
  {
    Invoke("DestroyProjectile", timeDestroy);  
  }

  private void DestroyProjectile()
  {
      Destroy(gameObject);
  }
}

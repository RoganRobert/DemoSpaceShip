using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the size of the ‘Boundary’ depending on Viewport. When objects go beyond the ‘Boundary’, they are destroyed or deactivated.
/// </summary>
public class Boundary : MonoBehaviour {

    BoxCollider2D boundareCollider;

    //receiving collider's component and changing boundary borders
    private void Start()
    {
        boundareCollider = GetComponent<BoxCollider2D>();
        ResizeCollider();
    }

    //changing the collider's size up to Viewport's size multiply 1.5
    void ResizeCollider() 
    {        
        Vector2 viewportSize = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;
        Vector2 viewportOffet = new Vector2();

        viewportSize.y *= 1.2f;
        viewportOffet.y = viewportSize.y * (-1.0f/(6*2));

    }

    //when another object leaves collider
    private void OnTriggerExit2D(Collider2D collision) 
    {        
        if (collision.tag == "Projectile")
        {
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Bonus") 
            Destroy(collision.gameObject);

        else if (collision.tag == "Planets")
            Destroy(collision.gameObject);
    }

}

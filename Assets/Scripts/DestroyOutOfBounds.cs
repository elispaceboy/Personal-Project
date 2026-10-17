using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float boundY = 10;
    private float boundX = 25;
 
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > boundX || transform.position.x < -boundX || transform.position.y > boundY || transform.position.y < -boundY)
        {
            Destroy(gameObject);
        }
    }
}

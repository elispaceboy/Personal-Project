using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpadeMovement : MonoBehaviour
{
    private float speed = 30.0f;
    private float boundY = 8;
    private float aimBoundShift = 5.0f;
    private float angularVelocity = 10.0f;
    private Rigidbody spadeRb;
    
    // Start is called before the first frame update
    void Start()
    {
        spadeRb = GetComponent<Rigidbody>();
        Vector3 targetPos = new Vector3(-transform.position.x, Random.Range(-boundY+aimBoundShift, boundY+aimBoundShift) /2, 0).normalized;
        spadeRb.AddForce(targetPos * speed, ForceMode.Impulse);
        spadeRb.angularVelocity = (new Vector3(0, 0, angularVelocity));
    }
}

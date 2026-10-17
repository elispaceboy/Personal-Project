using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovement : MonoBehaviour
{
    private float speed = 15.0f;
    private float boundY = 8;
    private float aimBoundShift = 6.0f;
    private float angularVelocity = 5.0f;

    private Rigidbody heartRb;

    // Start is called before the first frame update
    void Start()
    {
        heartRb = GetComponent<Rigidbody>();
        Vector3 targetPos = new Vector3(-transform.position.x, Random.Range(-boundY + aimBoundShift, boundY + aimBoundShift) / 2, 0).normalized;
        heartRb.AddForce(targetPos * speed, ForceMode.Impulse);
        heartRb.angularVelocity = (new Vector3(0, 0, angularVelocity));
    }
}

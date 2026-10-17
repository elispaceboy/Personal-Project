using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondMovement : MonoBehaviour
{
    private float speed = 30.0f;
    private float angularVelocity = 10.0f;
    private GameObject player;
    private Rigidbody diamondRb;
    private float softBoundX = 20;

    // Start is called before the first frame update
    void Start()
    {
        diamondRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        diamondRb.angularVelocity = (new Vector3(0, 0, angularVelocity));
        if (Mathf.Abs(transform.position.x) > softBoundX)
        {
            float direction = player.transform.position.x - transform.position.x;
            diamondRb.AddForce((Vector3.right*direction).normalized * speed, ForceMode.Impulse);
        } else
        {
            float direction = player.transform.position.y - transform.position.y;
            diamondRb.AddForce((Vector3.up * direction).normalized * speed, ForceMode.Impulse);
        }
    }
}

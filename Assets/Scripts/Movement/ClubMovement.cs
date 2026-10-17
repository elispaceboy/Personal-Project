using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubMovement : MonoBehaviour
{
    private Rigidbody clubRb;
    private GameObject player;
    private float boostInterval = 0.5f;
    private float speed = 5.0f;
    private float angularVelocity = -10.0f;


    // Start is called before the first frame update
    void Start()
    {
        clubRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        clubRb.angularVelocity = (new Vector3(0, 0, angularVelocity));
        InvokeRepeating("BoostToPlayer", 0, boostInterval);
    }

    void BoostToPlayer()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        clubRb.AddForce(lookDirection * speed, ForceMode.Impulse);
    }
}

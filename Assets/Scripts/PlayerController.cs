using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float gravityModifier = 0.1f;
    private float speed = 50.0f;
    private float boostMultiplier = 60.0f;
    private float slowDownMultiplier = 0.98f;
    private Rigidbody playerRb;
    private GameManager gameManager;
    private bool hasPowerup = false;
    private float powerupDuration = 10;

    public ParticleSystem explosionParticle;
    public ParticleSystem cardSliceParticle;

    public GameObject powerupIconOn;
    public GameObject powerupIconOff;

    private float maxX = 14;
    private float maxY = 8;

    private float wrapEjectVelocityMultiplier = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Physics.gravity *= gravityModifier;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            MovePlayer();
        } else
        {
            playerRb.Sleep();
        }
            ConstrainPlayerMovements();
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.up * verticalInput * speed);
        playerRb.AddForce(Vector3.right * horizontalInput * speed);

        playerRb.AddTorque(Vector3.forward * verticalInput);
        playerRb.AddTorque(Vector3.forward * horizontalInput);

        //slow down (Left Shift)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // decrease speed by 2% each tick
            playerRb.velocity *= slowDownMultiplier;
        }

        //boost (Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instant boost
            playerRb.AddForce(Vector3.up * verticalInput * boostMultiplier, ForceMode.Impulse);
            playerRb.AddForce(Vector3.right * horizontalInput * boostMultiplier, ForceMode.Impulse);
            playerRb.AddTorque(Vector3.forward * UnityEngine.Random.Range(-boostMultiplier, boostMultiplier), ForceMode.Impulse);
        }
    }

    void ConstrainPlayerMovements()
    {
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(-maxX, transform.position.y, -0.5f);
            playerRb.velocity *= wrapEjectVelocityMultiplier;
        }
        else if (transform.position.x < -maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, -0.5f);
            playerRb.velocity *= wrapEjectVelocityMultiplier;
        }

        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, -maxY, -0.5f);
            playerRb.velocity *= wrapEjectVelocityMultiplier;
        }
        else if (transform.position.y < -maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, -0.5f);
            playerRb.velocity *= wrapEjectVelocityMultiplier;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Enemy. powerup = " + hasPowerup);
            if (!hasPowerup)
            {
                //Enemy collides with vulnerable player
                if (gameManager.isGameActive)
                {
                    Instantiate(explosionParticle, transform.position, transform.rotation);
                }
                gameManager.GameOver();
            }
            else
            {
                //Enemy collided with poweruped player
                Destroy(collision.gameObject);
                Instantiate(cardSliceParticle, transform.position, transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup") && !hasPowerup)
        {
            hasPowerup = true;
            StartCoroutine(PowerupCountdownRoutine());
            Destroy(other.gameObject);
            powerupIconOn.SetActive(true);
            powerupIconOff.SetActive(false);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupDuration);
        hasPowerup = false;
        powerupIconOn.SetActive(false);
        powerupIconOff.SetActive(true);
    }
}

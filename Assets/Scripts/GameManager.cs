using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public GameObject titleScreen;
    public GameObject gameOverScreen;

    private SpawnManager spawnManager;
    private float enemyInterval = 3.0f;
    private float powerupInterval = 30.0f;

    public bool isGameActive = false;
    public float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    public void StartGame()
    {
        isGameActive = true;
        timeElapsed = 0;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerup());
        titleScreen.SetActive(false);
    }

    IEnumerator SpawnEnemy()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(enemyInterval/(timeElapsed/60+1));
            spawnManager.spawnRandomEnemy();
        }
    }

    IEnumerator SpawnPowerup()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(powerupInterval);
            spawnManager.spawnPowerup();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        { 
            timeElapsed += Time.deltaTime;
            int sec = (int)(timeElapsed % 60);
            string seconds = sec + "";
            if (sec < 10)
            {
                seconds = "0" + sec;
            }

            timerText.text = "Time: "+ (int)(timeElapsed / 60) + ":"+seconds;
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

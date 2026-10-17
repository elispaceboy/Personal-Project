using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject powerupPrefab;
    private float boundY = 7;
    private float boundX = 18;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            spawnRandomEnemy();
        }
    }

    public void spawnRandomEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        if (index < enemyPrefabs.Length / 2)
        {
            Instantiate(enemyPrefabs[index], generateSpawnPosPerimeter(), enemyPrefabs[index].transform.rotation);
        } else
        {
            Instantiate(enemyPrefabs[index], generateSpawnPosVertical(), enemyPrefabs[index].transform.rotation);
        }
    }

    public void spawnPowerup()
    {
        Instantiate(powerupPrefab, generateSpawnPos(), powerupPrefab.transform.rotation);
    }

    //returns a point on the perimeter created by boundX and boundY
    private Vector3 generateSpawnPosPerimeter()
    {
        float perimeterPos = Random.Range(0, boundY * 4 + boundX * 4);
        for (int i = 0; i < 4; i++)
        {
            if (i % 2 == 0)
            {
                if (perimeterPos <= boundX*2)
                {
                    return new Vector3(perimeterPos-boundX, boundY * (i * -1 + 1), -0.5f);
                }
                perimeterPos -= boundX*2;
            } else if (i % 2 == 1)
            {
                if (perimeterPos <= boundY*2)
                {
                    return new Vector3(boundX * ((i - 1) * -1 + 1), perimeterPos-boundY, -0.5f);
                }
                perimeterPos -= boundY*2;
            }
        }
        //Extra Case so it compiles
        return new Vector3(boundX, boundY, -0.5f);
    }

    private Vector3 generateSpawnPosVertical()
    {
        float verticalPos = Random.Range(-boundY * 2, boundY * 2);
        if (Mathf.Abs(verticalPos) >= boundY)
        {
            verticalPos -= boundY * (verticalPos/Mathf.Abs(verticalPos));
            return new Vector3(-boundX, verticalPos, -0.5f);
        }
        return new Vector3(boundX, verticalPos, -0.5f);
    }

    private Vector3 generateSpawnPos()
    {
        return new Vector3(Random.Range(-boundX, boundX), Random.Range(-boundY, boundY), -0.5f);
    }
}

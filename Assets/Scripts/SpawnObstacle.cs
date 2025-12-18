using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    int totalObstacles = 100;
    int spawned = 0;


    float leftBound, rightBound, topBound, bottomBound;

    [SerializeField]
    private GameObject obstacle;
    [SerializeField]
    Transform playerTransform;


    void Start()
    {
        leftBound = -8f;
        rightBound = 8f;
        topBound = 8f;
        bottomBound = -8f;

        StartCoroutine(SpawnObstacles());
    }


    IEnumerator SpawnObstacles()
    {
        int pickSide = 0;
        while (true)
        {
            //x - m * xp + c  

            Vector3 velocity = new Vector3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-4f, 4f), 0.0f);
            Vector3 position;


            switch(pickSide)
            {
                case 0:
                    position = new Vector3(UnityEngine.Random.Range(leftBound, rightBound), topBound, 0.0f);
                    break;
                case 1:
                    position = new Vector3(UnityEngine.Random.Range(leftBound, rightBound), topBound, 0.0f);
                    break;
                case 2:
                    position = new Vector3(leftBound, UnityEngine.Random.Range(bottomBound, topBound), 0.0f);
                    break;
                case 3:
                    position = new Vector3(rightBound, UnityEngine.Random.Range(bottomBound, topBound), 0.0f);
                    break;
                default:
                    position = new Vector3(leftBound, UnityEngine.Random.Range(bottomBound, topBound), 0.0f);
                    break;

            }

            pickSide += 1;
            pickSide %= 4;

            Vector3 directionToPlayer = playerTransform.position - position;
            Vector3 normalizedDirection = directionToPlayer.normalized;
            Vector3 targetVelocity = normalizedDirection * velocity.magnitude;

            GameObject newObstacle = Instantiate(obstacle);

            newObstacle.GetComponent<Obstacle>().setLocation(position);
            newObstacle.GetComponent<Obstacle>().setVelocity(targetVelocity);

            spawned++;

            yield return new WaitForSeconds(0.2f);
        }
    }
}

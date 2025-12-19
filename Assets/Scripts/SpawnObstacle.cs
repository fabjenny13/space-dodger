using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField]
    float spawnTime;
    [SerializeField]
    float spawnDelay;

    float leftBound, rightBound, topBound, bottomBound;

    [SerializeField]
    private GameObject obstacle;
    [SerializeField]
    Transform playerTransform;


    int currSide = 0;

    void Start()
    {
        leftBound = -8f;
        rightBound = 8f;
        topBound = 8f;
        bottomBound = -8f;

        InvokeRepeating("SpawnObstacles", spawnTime, spawnDelay);
    }

    void SpawnObstacles()
    {
            //x - m * xp + c  

            Vector3 velocity;
            Vector3 position;

            float vertical = topBound;
            float horizontal = leftBound;


            switch(currSide)
            {
                case 0:
                    position = new Vector3(horizontal, topBound, 0.0f);
                    velocity = new Vector3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(-4f, 0f), 0.0f);
                    break;
                case 1:
                    position = new Vector3(horizontal, bottomBound, 0.0f);
                    velocity = new Vector3(UnityEngine.Random.Range(-4f, 4f), UnityEngine.Random.Range(0, 4f), 0.0f);
                    break;
                case 2:
                    position = new Vector3(leftBound, vertical, 0.0f);
                    velocity = new Vector3(UnityEngine.Random.Range(0, 4f), UnityEngine.Random.Range(-4f, 4f), 0.0f);
                    break;
                case 3:
                    position = new Vector3(rightBound, vertical);
                    velocity = new Vector3(UnityEngine.Random.Range(-4f, 0), UnityEngine.Random.Range(-4f, 4f), 0.0f);
                    break;
                default:
                    position = new Vector3(leftBound, vertical, 0.0f);
                    velocity = new Vector3(UnityEngine.Random.Range(0, 4f), UnityEngine.Random.Range(-4f, 4f), 0.0f);
                    break;

            }

            currSide += 1;
            currSide %= 4;

            vertical -= 1;
            if(vertical <= bottomBound)
            {
                vertical = topBound;
            }

            horizontal += 1;
            if(horizontal >= rightBound)
            {
                horizontal = leftBound;
            }

/*            Vector3 directionToPlayer = playerTransform.position - position;
            Vector3 normalizedDirection = directionToPlayer.normalized;
            Vector3 targetVelocity = normalizedDirection * velocity.magnitude;

*/            
            GameObject newObstacle = Instantiate(obstacle);

            newObstacle.GetComponent<Obstacle>().setLocation(position);
            newObstacle.GetComponent<Obstacle>().setVelocity(velocity);
        if (playerTransform.gameObject.GetComponent<Rocket>().isGameOver)
        {
            CancelInvoke("SpawnObstacle");
        }
    }
}

using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private int spawnStyle;
    [SerializeField] float speed;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform bounds;

    private float leftBound, rightBound, topBound, bottomBound;
    private int currSide = 0;



    void Start()
    {
        leftBound = bounds.position.x - bounds.localScale.x/2.0f;
        rightBound = bounds.position.x + bounds.localScale.x / 2.0f;
        topBound = bounds.position.y + bounds.localScale.y / 2.0f;
        bottomBound = bounds.position.y - bounds.localScale.y / 2.0f;

        if (spawnStyle == 1)
            InvokeRepeating("SpawnStyle1", 1, 0.7f);
        else if (spawnStyle == 2)
            InvokeRepeating("SpawnStyle2", 2, 10f);
        else if (spawnStyle == 3)
            InvokeRepeating("SpawnStyle3", 3, 15f);
    }

    void SpawnStyle1()
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

        Vector3 directionToPlayer = playerTransform.position - position;
        Vector3 normalizedDirection = directionToPlayer.normalized;
        Vector3 targetVelocity = normalizedDirection * velocity.magnitude;


        GameObject newObstacle = Instantiate(obstacle);

            newObstacle.GetComponent<Obstacle>().setLocation(position);
            newObstacle.GetComponent<Obstacle>().setVelocity(targetVelocity);


        if(FindFirstObjectByType<Rocket>().isGameOver)
        {
            CancelInvoke("SpawnStyle1");
        }
    }

    void SpawnStyle2()
    {
        //circle style!
        float angleBetween = 36.0f;
        float radius = (rightBound - leftBound) / 2 - 2;
        Vector2 center = bounds.position;
        for (int i = 0; i < 10; i++)
        {

            Vector2 position = new Vector2(radius * Mathf.Cos(i * angleBetween), radius * Mathf.Sin(i * angleBetween));
            Vector3 direction = center - position;
            Vector3 targetVelocity = direction.normalized * speed;

            Debug.Log(targetVelocity);

            GameObject newObstacle = Instantiate(obstacle);

            newObstacle.GetComponent<Obstacle>().setLocation(position);
            newObstacle.GetComponent<Obstacle>().setVelocity(targetVelocity);
        }


        if (FindFirstObjectByType<Rocket>().isGameOver)
        {
            CancelInvoke("SpawnStyle2");
        }
    }

    void SpawnStyle3()
    {
        float displacement = ((rightBound - leftBound) / 10);

        for(int i = 0; i < 10; i++)
        {
            GameObject newObstacle = Instantiate(obstacle);

            Vector2 position, velocity;

            switch (currSide)
            {
                case 0:
                    position = new Vector2(leftBound  + i * displacement, topBound);
                    velocity = new Vector2(0, -speed);
                    break;              
                case 1:                  
                    position = new Vector2(leftBound + i * displacement, bottomBound);
                    velocity = new Vector2(0, speed);
                    break;               
                case 2:                  
                    position = new Vector2(leftBound, bottomBound + i * displacement);
                    velocity = new Vector2(speed, 0);
                    break;               
                case 3:                  
                    position = new Vector2(rightBound,bottomBound + i * displacement);
                    velocity = new Vector2(-speed, 0);
                    break;               
                default:                 
                    position = new Vector2(leftBound, i * displacement);
                    velocity = new Vector2(speed, 0);
                    break;

            }

            newObstacle.GetComponent<Obstacle>().setLocation(position);
            newObstacle.GetComponent<Obstacle>().setVelocity(velocity);
        }

        if (FindFirstObjectByType<Rocket>().isGameOver)
        {
            CancelInvoke("SpawnStyle3");
        }
    }
}

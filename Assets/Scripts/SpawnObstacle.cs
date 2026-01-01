using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform playerTransform;
    [SerializeField] PointsTracker pointsTracker;
    [SerializeField] int randomSeed = 12345;


    public GameObject[] obstacles;

    private float leftBound, rightBound, topBound, bottomBound;
    private int currSide = 0;


    void Start()
    {

        UnityEngine.Random.InitState(randomSeed);

        float zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zDistance));

        leftBound = bottomLeft.x;
        rightBound = topRight.x;
        topBound = topRight.y;
        bottomBound = bottomLeft.y;

        InvokeRepeating("SpawnStyle1", 1, 2f);
    }

    void SpawnStyle1()
    {
            //x - m * xp + c  

            Vector3 velocity;
            Vector3 position;


            float disp = Mathf.Sin(UnityEngine.Random.Range(0,5)* 20 * Mathf.Deg2Rad);

            switch (currSide)
            {
                case 0:
                    disp *= (rightBound - leftBound);
                    position = new Vector3(leftBound + disp, topBound, 0.0f);
                    velocity = new Vector3(UnityEngine.Random.Range(-speed, speed), -speed, 0.0f);
                    break;
                case 1:
                    disp *= (rightBound - leftBound);
                    position = new Vector3(leftBound + disp, bottomBound, 0.0f);
                    velocity = new Vector3(UnityEngine.Random.Range(-speed, speed), speed, 0.0f);
                    break;
                case 2:
                    disp *= (topBound - bottomBound);
                    position = new Vector3(leftBound, bottomBound + disp, 0.0f);
                    velocity = new Vector3(speed, UnityEngine.Random.Range(-speed, speed), 0.0f);
                    break;
                case 3:
                    disp *= (topBound - bottomBound);
                    position = new Vector3(rightBound, bottomBound + disp);
                    velocity = new Vector3(-speed, UnityEngine.Random.Range(-speed, speed), 0.0f);
                    break;
                default:
                    disp *= (topBound - bottomBound);
                    position = new Vector3(leftBound, bottomBound + disp, 0.0f);
                    velocity = new Vector3(speed, UnityEngine.Random.Range(-speed, speed), 0.0f);
                    break;

            }
            GameObject newObstacle = Instantiate(obstacles[0]);

            newObstacle.GetComponent<Obstacle>().setLocation(position);
            newObstacle.GetComponent<Obstacle>().setVelocity(velocity);


        

        currSide += 1;
        currSide %= 4;


        if(FindFirstObjectByType<Rocket>().isGameOver)
        {
            CancelInvoke("SpawnStyle1");
        }
    }

    void SpawnStyle2()
    {

        //circle style!
        float angleBetween = 36.0f;
        float radius = Mathf.Max((rightBound - leftBound) / 2, (topBound - bottomBound) / 2);
        Vector2 center = new Vector2(0,0);
        for (int i = 0; i < 10; i++)
        {
            Vector2 position = new Vector2(radius * Mathf.Cos(i * angleBetween), radius * Mathf.Sin(i * angleBetween));
            Vector3 direction = center - position;
            Vector3 targetVelocity = direction.normalized * speed;


            GameObject newObstacle = Instantiate(obstacles[1]);

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
            GameObject newObstacle = Instantiate(obstacles[2]);

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
        currSide += 1;
        currSide %= 4;
        if (FindFirstObjectByType<Rocket>().isGameOver)
        {
            CancelInvoke("SpawnStyle3");
        }
    }
}

using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private float speed = 5.0f;
    private float leftBound;
    private float rightBound;
    private float topBound;
    private float bottomBound;

    [SerializeField]
    Rigidbody2D rb;

    private void Start()
    {
        float zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zDistance));

        leftBound = bottomLeft.x;
        rightBound = topRight.x;
        topBound = topRight.y;
        bottomBound = bottomLeft.y;

    }
    private void Update()
    {
        if(transform.position.x < leftBound || transform.position.x > rightBound || transform.position.y > topBound || transform.position.y < bottomBound)
        {
            Destroy(gameObject);
        }

        if(FindFirstObjectByType<Rocket>().isGameOver)
        {
            Destroy(gameObject);
        }
    }


    public void setLocation(Vector3 loc)
    {
        transform.position = loc;
    }
    
    public void setVelocity(Vector3 vel)
    {
        rb.linearVelocity = vel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Rocket>().TakeDamage();
            Destroy(gameObject);
        }
    }
}

using System.Runtime.CompilerServices;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private float speed = 5.0f;
    private float leftBound = -8.0f;
    private float rightBound = 8.0f;
    private float topBound = 8.0f;
    private float bottomBound = -8.0f;

    [SerializeField]
    Rigidbody2D rb;

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

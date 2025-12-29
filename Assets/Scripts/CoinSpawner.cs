using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    private float leftBound, rightBound, topBound, bottomBound;


    private void Start()
    {
        float zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDistance));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zDistance));

        leftBound = bottomLeft.x;
        rightBound = topRight.x;
        topBound = topRight.y;
        bottomBound = bottomLeft.y;

        Relocate();

    }
    public void Relocate()
    {
        float xPos = Random.Range(leftBound, rightBound);
        float yPos = Random.Range(bottomBound, topBound);
        transform.position = new Vector3(xPos, yPos);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Collided");
            Relocate();
        }
    }


}

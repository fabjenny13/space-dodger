using UnityEngine;
using Unity.Mathematics;

public class CoinSpawner : MonoBehaviour
{
    private float leftBound, rightBound, topBound, bottomBound;

    [SerializeField] PointsTracker pointsTracker;
    [SerializeField] uint randomSeed = 12345;

    private Unity.Mathematics.Random rng;

    private void Start()
    {
        rng = new Unity.Mathematics.Random(randomSeed);

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
        float xPos = rng.NextFloat(leftBound, rightBound);
        float yPos = rng.NextFloat(bottomBound, topBound);

        transform.position = new Vector3(xPos, yPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Relocate();
            pointsTracker.AddBonusPoints(100);
        }
    }
}


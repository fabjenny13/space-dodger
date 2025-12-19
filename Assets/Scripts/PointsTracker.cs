using System.Collections;
using TMPro;
using UnityEngine;

public class PointsTracker : MonoBehaviour
{

    int points = 0;

    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(IncreaseScore());
    }

    // Update is called once per frame
    void Update()
    {
        if (FindAnyObjectByType<Rocket>().isGameOver)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator IncreaseScore()
    {
        while (true)
        {
            points += 10;
            text.text = "Score: " + points.ToString();
            yield return new WaitForSeconds(1);

        }
    }
}

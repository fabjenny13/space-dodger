using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    Vector3 mousePos;
    float moveSpeed = 5.0f;

    float maxHealth = 200.0f;
    float health = 200.0f;

    [SerializeField]
    private Image healthBar;
    [SerializeField] private GameObject GameOverText;

    public bool isGameOver = false;

    public void TakeDamage()
    {
        if (health <= 0)
        {
            isGameOver = true;
            GameOverText.SetActive(true);
        }

        else
        {
            health -= 20;
            healthBar.rectTransform.sizeDelta = new Vector2((health / maxHealth) * 200, healthBar.GetComponent<RectTransform>().rect.height);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;

        mousePos.z = -Camera.main.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        transform.position = Vector2.Lerp(transform.position, mousePos, moveSpeed);

    }


    
}

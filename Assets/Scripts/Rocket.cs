using UnityEngine;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    Vector3 mousePos;
    float moveSpeed = 5.0f;

    float maxHealth = 100.0f;
    float health = 100.0f;

    [SerializeField]
    private Image healthBar;

    public void TakeDamage()
    {
        if (health <= 0)
            Debug.Log("Game Lost!");
        else
        {
            health -= 20;
            healthBar.rectTransform.sizeDelta = new Vector2((health / maxHealth) * 100, healthBar.GetComponent<RectTransform>().rect.height);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

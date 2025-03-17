using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    
    public string playerID;
    public GameObject playerIDText;
    public TextMeshProUGUI playerIDUI;
    public GameObject playerImage;

    public void Initialize(string id, Color color)
    {
        playerID = id;
        playerImage.GetComponent<SpriteRenderer>().color = color;
        playerIDUI.text = playerID;
    }

    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        // 測試時使用鍵盤，未來可用手機替代
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;
    }

    // 當手機輸入時呼叫這個方法
    public void MoveByPhone(Vector2 direction)
    {
        movement = direction;
    }
}

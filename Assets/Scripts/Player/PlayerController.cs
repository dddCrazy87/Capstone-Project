using System.Collections;
using System.Collections.Generic;
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
    // 記錄玩家移動軌跡
    private Queue<Vector2> positionHistory = new Queue<Vector2>();
    // 記錄軌跡的長度
    private const int trailLength = 1000;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        for (int i = 0; i < trailLength; i++) {
            positionHistory.Enqueue(transform.position);
        }
    }

    void Update()
    {
        // 測試時使用鍵盤，未來可用手機替代
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        // 正規化移動向量，確保對角線移動不會更快
        if (movement.magnitude > 1) {
            movement.Normalize();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement * speed;

        // 更新位置歷史
        positionHistory.Enqueue(transform.position);
        if (positionHistory.Count > trailLength) {
            positionHistory.Dequeue();
        }
    }

    // 獲取某個時間點前的位置
    public Vector2 GetPositionInPast(int framesAgo) {
        framesAgo = Mathf.Clamp(framesAgo, 0, positionHistory.Count - 1);
        return positionHistory.ToArray()[positionHistory.Count - 1 - framesAgo];
    }

    // 當手機輸入時呼叫這個方法
    public void MoveByPhone(Vector2 direction)
    {
        movement = direction;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedItemsManager : MonoBehaviour
{
    public GameObject itemFollowerPrefab; // 跟隨玩家的道具預製體
    public float followerSpacing = 0.7f; // 跟隨者之間的距離
    
    private List<GameObject> collectedItems = new List<GameObject>();
    private PlayerController playerController;
    
    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    
    void Update()
    {
        // 更新每個收集到的道具的位置
        for (int i = 0; i < collectedItems.Count; i++)
        {
            // 計算每個道具應該在的位置
            // 每個道具都跟隨著玩家幾幀之前的位置
            int framesAgo = (i + 1) * 5; // 調整這個值來改變跟隨的緊密程度
            Vector2 targetPosition = playerController.GetPositionInPast(framesAgo);
            
            // 平滑移動道具到目標位置
            collectedItems[i].transform.position = Vector2.Lerp(
                collectedItems[i].transform.position,
                targetPosition,
                Time.deltaTime * 5f // 調整這個值來改變跟隨的速度
            );
        }
    }
    
    // 添加新收集的道具
    public void AddItem(Sprite itemSprite, Color itemColor, string itemName)
    {
        GameObject follower = Instantiate(itemFollowerPrefab, transform.position, Quaternion.identity);
        
        // 設置道具的外觀
        SpriteRenderer spriteRenderer = follower.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = itemSprite;
            spriteRenderer.color = itemColor;
        }
        
        // 確保道具在適當的層級
        spriteRenderer.sortingOrder = collectedItems.Count + 1;
        
        // 添加到收集列表
        collectedItems.Add(follower);
    }
}
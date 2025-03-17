using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemSprite;
    public Color itemColor = Color.white;
    
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = itemSprite;
            spriteRenderer.color = itemColor;
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 玩家碰到道具，通知道具管理器
            ItemManager itemManager = FindFirstObjectByType<ItemManager>();
            if (itemManager != null)
            {
                itemManager.ItemCollected(gameObject);
            }
            
            // 通知玩家收集到道具
            CollectedItemsManager collectedItemsManager = FindFirstObjectByType<CollectedItemsManager>();
            if (collectedItemsManager != null)
            {
                collectedItemsManager.AddItem(itemSprite, itemColor, itemName);
            }
            
            // 銷毀道具
            Destroy(gameObject);
        }
    }
}
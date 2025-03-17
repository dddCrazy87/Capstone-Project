using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] itemPrefabs; // 道具的預製體
    public int maxItems = 5; // 最大同時存在的道具數量
    public float itemRespawnTime = 5f; // 道具重生時間
    public float mapWidth = 10f; // 地圖寬度
    public float mapHeight = 10f; // 地圖高度
    
    private List<GameObject> activeItems = new List<GameObject>();
    
    void Start()
    {
        // 初始化道具
        for (int i = 0; i < maxItems; i++)
        {
            SpawnItem();
        }
    }
    
    void SpawnItem()
    {
        if (itemPrefabs.Length == 0) return;
        
        // 隨機選擇一個道具預製體
        GameObject itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        
        // 在地圖範圍內隨機位置生成道具
        Vector2 spawnPosition = new Vector2(
            Random.Range(-mapWidth/2, mapWidth/2),
            Random.Range(-mapHeight/2, mapHeight/2)
        );
        
        GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        activeItems.Add(item);
        
        // 設定道具會在一定時間後刷新
        StartCoroutine(RespawnItemAfterTime(item));
    }
    
    IEnumerator RespawnItemAfterTime(GameObject item)
    {
        yield return new WaitForSeconds(itemRespawnTime);
        
        // 如果道具還存在，則刷新它
        if (activeItems.Contains(item))
        {
            Vector2 newPosition = new Vector2(
                Random.Range(-mapWidth/2, mapWidth/2),
                Random.Range(-mapHeight/2, mapHeight/2)
            );
            
            item.transform.position = newPosition;
            
            // 重新啟動計時器
            StartCoroutine(RespawnItemAfterTime(item));
        }
    }
    
    // 當道具被玩家收集時，從列表中移除它
    public void ItemCollected(GameObject item)
    {
        if (activeItems.Contains(item))
        {
            activeItems.Remove(item);
            // 生成新的道具
            SpawnItem();
        }
    }
}
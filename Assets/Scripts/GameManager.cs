using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    public Transform[] spawnPoints; // 預先設定好的出生點
    public List<Team> teams = new List<Team>();

    private bool gameStarted = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        Debug.Log("等待玩家註冊...");
    }

    // 當從網頁接收到玩家資訊時，呼叫這個函式
    public void StartGame(List<Team> receivedTeams)
    {
        if (gameStarted) return;

        teams = receivedTeams;
        AssignSpawnPoints();  // 分配出生點
        SpawnPlayers();       // 生成玩家

        gameStarted = true;
        Debug.Log("遊戲開始!");
    }

    private void AssignSpawnPoints()
    {
        List<Transform> availablePoints = new List<Transform>(spawnPoints);
        System.Random rnd = new System.Random();

        foreach (var team in teams)
        {
            int index = rnd.Next(availablePoints.Count);
            team.spawnPoint = availablePoints[index];
            availablePoints.RemoveAt(index);
        }
    }

    private void SpawnPlayers()
    {
        foreach (var team in teams)
        {
            Color teamColor = Random.ColorHSV(); // 給該隊伍隨機顏色
            foreach (var playerID in team.players)
            {
                GameObject player = Instantiate(playerPrefab, team.spawnPoint.position, Quaternion.identity);
                player.GetComponent<SpriteRenderer>().color = teamColor;
                player.GetComponent<PlayerController>().Initialize(playerID);
            }
        }
    }
}

// 定義隊伍資料結構
[System.Serializable]
public class Team
{
    public string teamID;
    public List<string> players;
    public Transform spawnPoint;
}

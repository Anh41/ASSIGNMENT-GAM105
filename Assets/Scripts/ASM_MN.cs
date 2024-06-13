using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Threading;
using UnityEngine.UI;
using System;

[SerializeField]
public class Region 
{
    public int ID;
    public string Name;
    public Region(int ID, string Name)
    {
        this.ID = ID;
        this.Name = Name;
    }
}
[SerializeField]
public class Players
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public Region PlayerRegion { get; set; }
    ScoreKeeper sc;
    public Players(int id, string name, int score, Region region)
    {
        ID = id;
        Name = name;
        Score = score;
        PlayerRegion = region;
    }
}

public class ASM_MN : Singleton<ASM_MN>
{
    public List<Region> listRegion = new List<Region>();
    public List<Players> listPlayer = new List<Players>();

    private void Start()
    {
        createRegion();
    }

    public void createRegion()
    {
        listRegion.Add(new Region(0, "VN"));
        listRegion.Add(new Region(1, "VN1"));
        listRegion.Add(new Region(2, "VN2"));
        listRegion.Add(new Region(3, "JS"));
        listRegion.Add(new Region(4, "VS"));
    }

    public string calculate_rank(int score)
    {
        if (score < 100)
        {
            return "Dong";
        }
        else if (score >= 100 && score < 500)
        {
            return "bac";
        }
        else if (score >= 500 && score < 1000)
        {
            return "vang";
        }
        else if (score > 1000)
        {
            return "kim cuong";
        }
        return null;
    }

    public void YC1(ScoreKeeper scoreKeeper)
    {
        int playerID = scoreKeeper.GetID();
        string playerUserName = scoreKeeper.GetUserName();
        int playerScore = scoreKeeper.GetScore();
        int playerRegionID = scoreKeeper.GetIDregion();

        Region playerRegion = listRegion.FirstOrDefault(r => r.ID == playerRegionID);

        Players newPlayer = new Players(playerID, playerUserName, playerScore, playerRegion);
        listPlayer.Add(newPlayer);
    }
    public void YC2()
    {
       foreach (Players player in listPlayer)
        {
            Debug.Log("Player Name: " + player.Name + " - Score: " + player.Score + " - Region: " + player.PlayerRegion.Name + "-Rank: " + calculate_rank(ScoreKeeper.Instance.score));
        }
    }
    public void YC3(int currentPlayerScore)
    {
        Debug.Log("Danh sach player co diem it hon la:");
        var playersWithLessScore = listPlayer.Where(player => player.Score < currentPlayerScore);

        foreach (Players player in playersWithLessScore)

        {
            Debug.Log("Id:" + player.ID + " Name: " + player.Name + "score:" + player.Score + "hang:" + calculate_rank(player.Score));
        }
    }
    public void YC4()
    {
        int currentPlayerId = ScoreKeeper.Instance.GetID();
        var player = listPlayer.FirstOrDefault(p => p.ID == currentPlayerId);
        if (player != null)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log("Current Player: Name: " + player.Name + " - Score: " + player.Score + " - Region: " + player.PlayerRegion.Name + " - Rank: " + rank);
        }
        else
        {
            Debug.Log("Player not found.");
        }
    }
    public void YC5()
    {
        var sortedPlayers = listPlayer.OrderByDescending(p => p.Score).ToList();
        Debug.Log("Danh sách người chơi theo thứ tự score giảm dần:");
        foreach (var player in sortedPlayers)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log("Name: " + player.Name + " - Score: " + player.Score + " - Region: " + player.PlayerRegion.Name + " - Rank: " + rank);
        }
    }
    public void YC6()
    {
        var sortedPlayers = listPlayer.OrderBy(p => p.Score).Take(5).ToList();
        Debug.Log("Top 5 người chơi có score thấp nhất:");
        foreach (var player in sortedPlayers)
        {
            string rank = calculate_rank(player.Score);
            Debug.Log("Name: " + player.Name + " - Score: " + player.Score + " - Region: " + player.PlayerRegion.Name + " - Rank: " + rank);
        }
    }
    public void YC7()
    {
        Thread bxhThread = new Thread(CalculateAndSaveAverageScoreByRegion);
        bxhThread.Name = "BXH";
        bxhThread.Start();
    }
    void CalculateAndSaveAverageScoreByRegion()
    {
        var regionGroups = listPlayer.GroupBy(p => p.PlayerRegion);
        using (StreamWriter writer = new StreamWriter("bxhRegion.txt"))
        {
            foreach (var group in regionGroups)
            {
                string regionName = group.Key.Name;
                double averageScore = group.Average(p => p.Score);
                writer.WriteLine("Region: " + regionName + " - Average Score: " + averageScore);
            }
        }
        Debug.Log("Đã lưu thông tin vào bxhRegion.txt");
    }

}
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using YG;

public class ButtonUI : MonoBehaviour
{
    private int Score;

    public void addition(int record)
    {
        Score = record;
        Debug.Log(Score);
    }

    public void SendNewRecord()
    {
        YandexGame.NewLeaderboardScores("Record", Score);        
    }
}

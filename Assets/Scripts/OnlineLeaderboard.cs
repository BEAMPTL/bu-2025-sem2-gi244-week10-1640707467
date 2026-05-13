using UnityEngine;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;

public class OnlineLeaderboard : MonoBehaviour
{
    public string leaderboardId = "runner_score";
    public TextMeshProUGUI leaderboardText;

    async void Start()
    {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    public async void SubmitScore(int score)
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");

        playerName = playerName.Replace(" ", "_");

        await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);

        await LeaderboardsService.Instance.AddPlayerScoreAsync(
            leaderboardId,
            score
        );

        GetScores();
    }

    public async void GetScores()
    {
        var scores = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId);

        leaderboardText.text = "Top 10\n";

        foreach (var entry in scores.Results)
        {
            leaderboardText.text +=
                (entry.Rank + 1) + ". " +
                entry.PlayerName + " : " +
                entry.Score + "\n";
        }
    }
}
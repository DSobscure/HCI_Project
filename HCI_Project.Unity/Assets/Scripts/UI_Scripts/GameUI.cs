using HCI_Project.Library;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text gameInfoText;

    private void Start()
    {
        Game game = Global.Game;
        game.OnScoreChanged += UpdateGameInfo;
        game.OnWaveChanged += UpdateGameInfo;

        UpdateGameInfo(game);
    }
    private void OnDestroy()
    {
        Game game = Global.Game;
        game.OnScoreChanged -= UpdateGameInfo;
        game.OnWaveChanged -= UpdateGameInfo;
    }

    private void UpdateGameInfo(Game game)
    {
        gameInfoText.text = string.Format("Wave: {0} Score: {1}", game.Wave, game.Score);
    }
}

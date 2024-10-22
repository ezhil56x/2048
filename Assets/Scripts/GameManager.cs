using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    public void NewGame() {
        SetScore(0);
        highScoreText.text = LoadHighScore().ToString();
        
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver() {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay) {
        yield return new WaitForSeconds(delay);
        
        float elapsedTime = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while(elapsedTime < duration) {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points) {
        SetScore(score + points);
    }

    private void SetScore(int score) {
        this.score = score;
        scoreText.text = score.ToString();
        SaveHighScore();
    }

    private void SaveHighScore() {
        int highScore = LoadHighScore();
        if(score > highScore) {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    private int LoadHighScore() {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}

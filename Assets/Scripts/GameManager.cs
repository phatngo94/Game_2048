using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;

    public int score;
    
    public HoldScore poolScore;
    public Command command = new Command();
    public int prveScore=0;
    private void Start()
    {
        NewGame();
    }

    public void NewGame() 
    {
        SetScore(0);
        hiscoreText.text = LoadHiscore().ToString();
        gameOver.alpha = 0;
        gameOver.interactable = false;

        board.ClearBoard();
        board.StartGame();
        board.enabled = true;
        command.blockUnits.Clear();
        command.undoBlockUnits.Clear();
        
    }

    public void Undo()
    {
        if (command.blockUnits.Count > 0)
        {
            board.ClearBoard();
            board.Undo();           
        }
    }

    public void Redo()
    {
        if (command.undoBlockUnits.Count == 0) return;
        board.ClearBoard();        
        board.Redo();       
    }

    public void GameOver()
    {
        board.enabled = false;
        StartCoroutine(Fade(gameOver,1f,1f));
    }

    public IEnumerator Fade(CanvasGroup canvasGroup,float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
        canvasGroup.interactable = true;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
               
    }

    public void SetPrevScore()
    {
        prveScore =int.Parse(scoreText.text);
    }
    public void FadeScore()
    {
        int number = int.Parse(scoreText.text) - prveScore;
        if (number >0)
        {
            AddScore addScore = poolScore.GetAddScore();
            addScore.gameObject.SetActive(true);
            addScore.SetScore(number);
            addScore.HideFadeScore();
        }
        
    }

    public void SetScore(int score)
    {       
        this.score = score;
        scoreText.text = score.ToString();
        
    }

    public void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if(score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore",score);
        }
    }

    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore",0);
    }

    
}




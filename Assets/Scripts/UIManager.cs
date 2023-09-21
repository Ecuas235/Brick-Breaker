using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public int score;
    public int lives;
    public Text scoreText;
    public Text livesText;
    public Text highScoreText;
    public InputField highScoreInput;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevelIndex = 0;
    public Transform paddle;
    
    // Start is called before the first frame update
    void Start()
    {
        numberOfBricks = GameObject.FindGameObjectsWithTag("bricks").Length;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void IncrementScore()
    {
        score+=20;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLives(int changeLives)
    {

        lives += changeLives;
        livesText.text = "Lives: " + lives;
        if (lives <= 0){
            GameOver();
        } 

    }

    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;
        if(numberOfBricks <=0)
        {
            if(currentLevelIndex >= levels.Length - 1)
            {
            GameOver();
            }else 
            {
                UpdateLives(1);
                loadLevelPanel.SetActive (true);
                loadLevelPanel.GetComponentInChildren<Text> ().text = "Level " + (currentLevelIndex + 2);
                gameOver = true;
                Invoke("LoadLevel", 5f);
            }

        }

    }

    void LoadLevel(){

        UpdateLives(1);
        currentLevelIndex++;
        Instantiate(levels[currentLevelIndex],Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("bricks").Length;
        gameOver = false;
        loadLevelPanel.SetActive (false);

        


    }

    void GameOver(){
        gameOver = true;
        gameOverPanel.SetActive (true);
        int highScore = PlayerPrefs.GetInt ("HIGHSCORE");
        if (score > highScore)
        {
            PlayerPrefs.SetInt ("HIGHSCORE", score);
            
            highScoreText.text = "New High Score!  "+ "\n" + "Enter Your Name Below";
            highScoreInput.gameObject.SetActive (true);
        } else
        { 
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + " High Score was " + highScore + "\n" + "Can you beat it?";
        }

    }

    public void NewHighScore()
    {
        string highScoreName = highScoreInput.text;
        PlayerPrefs.SetString ("HIGHSCORENAME", highScoreName);
        highScoreInput.gameObject.SetActive (false);
        highScoreText.text = "Congratulations " + highScoreName + "\n" + "Your New High Score is " + score;
    } 



    public void PlayAgain(){
        SceneManager.LoadScene("Brick Breaker");

    }
    public void Quit(){
       SceneManager.LoadScene("Start Menu");
    }
}

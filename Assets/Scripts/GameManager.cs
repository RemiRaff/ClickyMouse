using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;

    public bool isGameActive;

    private int score;
    private int highScore = 0;
    private float spawnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("remi_unity_hs"))
        {
            highScore = PlayerPrefs.GetInt("remi_unity_hs");
        }
        // Debug.Log("high score"+highScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;

        if (highScore <= score)
        {
            highScore = score;
        }
        highScoreText.text = "High Score: " + highScore;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        PlayerPrefs.SetInt("remi_unity_hs", highScore);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float difficulty)
    {
        // gameover spawn gestion
        isGameActive = true;
        // set score to zero and update
        score = 0;
        // highScore = 0;
        UpdateScore(0);

        // wait and spawn target
        StartCoroutine(SpawnTarget());

        // Title screen deletion
        titleScreen.gameObject.SetActive(false);

        // for higher difficulty the spawn time is smaller
        spawnRate /= difficulty;
    }
}

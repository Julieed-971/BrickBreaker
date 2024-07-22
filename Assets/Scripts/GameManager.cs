using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public RollinPin rollinPin { get; private set; }

    public Bricks[] bricks { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 3;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        // to avoid error when finishing the game
        if (level > 10) {
            // TODO: add win screen
            SceneManager.LoadScene("WinScreen");
        } else {
            SceneManager.LoadScene("Level" + level);
        }
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindObjectOfType<Ball>();
        this.rollinPin = FindObjectOfType<RollinPin>();
        this.bricks = FindObjectsOfType<Bricks>();
    }

    private void ResetLevel()
    {
        this.ball.ResetBall();
        this.rollinPin.ResetRollinPin();
    }

    private void GameOver()
    {
        // SceneManager.LoadScene("GameOver");

        NewGame();
    }

    public void Miss()
    {
        this.lives--;

        if (this.lives > 0) {
            ResetLevel();
        } else {
            GameOver();
        }
    }

    public void Hit(Bricks bricks)
    {
        this.score += bricks.points;

        if (Cleared()) {
            LoadLevel(this.level + 1);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < this.bricks.Length; i++) 
        {
            if (this.bricks[i].gameObject.activeInHierarchy && !this.bricks[i].unbreakable) {
                return false;
            }
        }

        return true;
    }
}

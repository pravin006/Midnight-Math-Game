using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class RescueTimer : MonoBehaviour
{
    public static RescueTimer Instance { get; private set; }
    private float realSecondsPerGameHour = 180f; // 1 real minute = 1 game hour
    private float hoursUntilDawn = 6f;
    private float gameHoursRemaining;
    public TMP_Text timerText;
    public bool isGameActive = true;
    public EnemySpawner enemySpawnerScript;
    // private bool halfTriggered = false;
    private int lastTriggeredHour = -1;
    private float enemyRate = 1f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameHoursRemaining = hoursUntilDawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameActive) return;

        float hoursPerSecond = 1f / realSecondsPerGameHour;
        gameHoursRemaining -= Time.deltaTime * hoursPerSecond;

        if (gameHoursRemaining <= 0)
        {
            gameHoursRemaining = 0;
            YouWin("You've survived and been rescued!");
            return;
        }             
        
        // if (!halfTriggered && gameHoursRemaining <= hoursUntilDawn / 2f)
        // {

        //     halfTriggered = true;
        //     PlayerCanvas.Instance.ShowMessage("New Enemy Wave Incoming", 5f);
        //     enemySpawnerScript.SpawnEnemies(5f);
        // }

        int currentHour = Mathf.FloorToInt(gameHoursRemaining);
        float hourFraction = gameHoursRemaining - currentHour;

        // Trigger once when the current hour passes its halfway point
        if (hourFraction <= 0.5f && lastTriggeredHour != currentHour)
        {
            lastTriggeredHour = currentHour;
            PlayerCanvas.Instance.ShowMessage("New Enemy Wave Incoming", 5f);
            enemySpawnerScript.SpawnEnemies(enemyRate);
            enemyRate++;
        }

        
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int hours = Mathf.FloorToInt(gameHoursRemaining);
        int minutes = Mathf.FloorToInt((gameHoursRemaining - hours) * 60);

        timerText.text = $"Rescue in {hours:00}H:{minutes:00}M";
    }


    void YouWin(string message)
    {
        // Debug.Log("You Win!");
        isGameActive = false;
        PlayerCanvas.Instance.WinLose(1,"You Win!", message);
        AudioManager.Instance.YouWin();
        StartCoroutine(NextSceneRoutine("YouWin"));
    }

    public void YouLose(string message)
    {
        // Debug.Log("You Lose!");
        isGameActive = false;
        PlayerCanvas.Instance.WinLose(0,"You Lose!", message);
        AudioManager.Instance.YouLose();
        StartCoroutine(NextSceneRoutine("YouLose"));
    }

    private IEnumerator NextSceneRoutine(string nextScene)
    {
        yield return new WaitForSeconds(3f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(nextScene);
    }
    
}

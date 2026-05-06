using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header ("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource playerSource;

    [Header ("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip mouseClick;
    public AudioClip spawnEnemies;
    public AudioClip openGame;
    public AudioClip gameCorrect;
    public AudioClip gameIncorrect;
    public AudioClip shotCorrect;
    public AudioClip shotIncorrect;
    public AudioClip playerWalk;
    public AudioClip playerHurt;
    public AudioClip playerDied;
    public AudioClip beaconWarning;
    public AudioClip youLose;
    public AudioClip youWin;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlayMouseClick()
    {
        sfxSource.clip = mouseClick;
        sfxSource.Play();
    }

    public void PlaySpawnEnemies()
    {
        sfxSource.clip = spawnEnemies;
        sfxSource.Play();
    }

    public void OpenGame()
    {
        sfxSource.clip = openGame;
        sfxSource.Play();
    }

    public void CorrectAnswer()
    {
        sfxSource.clip = gameCorrect;
        sfxSource.Play();
    }

    public void IncorrectAnswer()
    {
        sfxSource.clip = gameIncorrect;
        sfxSource.Play();
    }

    public void CorrectShot()
    {
        sfxSource.clip = shotCorrect;
        sfxSource.Play();
    }

    public void IncorrectShot()
    {
        sfxSource.clip = shotIncorrect;
        sfxSource.Play();
    }

    public void Mine(AudioClip miningAudioClip)
    {
        sfxSource.clip = miningAudioClip;
        sfxSource.Play();
    }

    public void PlayerWalk()
    {
        playerSource.clip = playerWalk;
        playerSource.Play();
    }

    public void PlayerHurt()
    {
        playerSource.clip = playerHurt;
        playerSource.Play();
    }

    public void PlayerDied()
    {
        playerSource.clip = playerDied;
        playerSource.Play();
    }

    public void BeaconWarning()
    {
        sfxSource.clip = beaconWarning;
        sfxSource.Play();
    }

    public void YouLose()
    {
        sfxSource.clip = youLose;
        sfxSource.Play();
    }

    public void YouWin()
    {
        sfxSource.clip = youWin;
        sfxSource.Play();
    }
}

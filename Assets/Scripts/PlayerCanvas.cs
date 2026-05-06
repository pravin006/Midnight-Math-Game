using UnityEngine;
using TMPro;
using System.Collections;


public class PlayerCanvas : MonoBehaviour
{
    public static PlayerCanvas Instance { get; private set; }
    public TMP_Text screenText;
    public TMP_Text waveText;
    public TMP_Text winLoseText;
    public TMP_Text reasonToNextSceneText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetRightClickGuide();
    }

    public void RightClickGuide(string guideText)
    {
        screenText.text = guideText;
    }

    public void ResetRightClickGuide()
    {
        screenText.text = "";
    }


    public void ShowMessage(string message, float duration)
    {
        // StopAllCoroutines();
        StartCoroutine(ShowMessageRoutine(message, duration));
    }

    private IEnumerator ShowMessageRoutine(string message, float duration)
    {
        waveText.text = message;
        waveText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        waveText.gameObject.SetActive(false);
    }

    public void WinLose(int winLose, string message1, string message2)
    {
        if (winLose == 0)
        {
            winLoseText.color = Color.red;
        }
        else
        {
            winLoseText.color = Color.green;
        }
        winLoseText.text = message1;
        reasonToNextSceneText.text = message2;
        winLoseText.gameObject.SetActive(true);
        reasonToNextSceneText.gameObject.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Beacon : MonoBehaviour
{
    private float maxBeaconSeconds = 180f;   // max real seconds
    private float beaconSecondsRemaining;
    public RescueTimer rescueTimerScript;
    public Slider beaconBar;
    public Image fillImage;
    private bool warningPlayed = false;

    void Start()
    {
        beaconSecondsRemaining = maxBeaconSeconds;
        beaconBar.maxValue = maxBeaconSeconds;
        beaconBar.value = beaconSecondsRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rescueTimerScript.isGameActive)
        {
            return;
        }
        
        beaconSecondsRemaining -= Time.deltaTime;
        beaconBar.value = beaconSecondsRemaining;
        // Debug.Log("Beacon Time Remaining: " + beaconSecondsRemaining);
        if (beaconSecondsRemaining <= 0)
        {
            beaconSecondsRemaining = 0;
            RescueTimer.Instance.YouLose("Beacon has expired!");
            // Debug.Log("Beacon has expired!");
        }

        float percent = beaconSecondsRemaining / maxBeaconSeconds;
        if (percent < 0.3f)
        {
            fillImage.color = Color.red;
            if (!warningPlayed)
            {
                AudioManager.Instance.BeaconWarning();
                warningPlayed = true;
            }
        }
        else
        {
            fillImage.color = Color.green;
        }

    }

    public void addTime(float secondsToAdd)
    {
        beaconSecondsRemaining = Mathf.Clamp(beaconSecondsRemaining + secondsToAdd, 0f, maxBeaconSeconds);
        beaconBar.value = beaconSecondsRemaining;
        // Debug.Log("Added " + secondsToAdd + " seconds to the beacon. Time remaining: " + beaconSecondsRemaining);
    }
}

using UnityEngine;
using TMPro;

public class Option : MonoBehaviour
{
    private int optionID;
    private string optionText;
    public Enemy enemyScript;
    private TMP_Text tmpText;
    void Awake()
    {
        tmpText = GetComponentInChildren<TMP_Text>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print("you hit option " + optionText);
            enemyScript.CheckAnswer(optionID);
        }
    }

    public void RaycastHitOption()
    {
        // Debug.Log("you hit option " + optionText);
        enemyScript.CheckAnswer(optionID);
    }

    public void SetOptionID(int id)
    {
        optionID = id;
    }

    public void SetOptionText(string text)
    {
        optionText = text;
        tmpText.text = text;
        // Here you would typically update the visual representation of the option, e.g., a TextMesh or UI Text component
    }
}

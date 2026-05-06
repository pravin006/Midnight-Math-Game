using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardButton : MonoBehaviour
{
    public TMP_Text label;
    public Image background;
    private string cardValue;
    private int id;
    public CardMatchingGame gameScript;

    // private void Awake()
    // {
    //     background = GetComponent<Image>();
    // }

    public void SetCardValue(string value)
    {
        cardValue = value;
        // Debug.Log(gameObject.name + " set to value: " + cardValue);
        label.text = cardValue;
    }

    public void SetID(int newID)
    {
        id = newID;
        // Debug.Log(gameObject.name + " set to ID: " + id);
    }

    public int GetID()
    {
        return id;
    }

    public void SelectCard()
    {
        gameScript.AddCard(this);
    }

    public void SetSelectedColor()
    {
        background.color = Color.yellow;
    }

    public void SetCorrectColor()
    {
        background.color = Color.green;
    }

    public void SetDefaultColor()
    {
        background.color = Color.white;
    }
}

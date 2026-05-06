using System.Collections.Generic;
using UnityEngine;

public class CardMatchingGame : MonoBehaviour
{
    // private int numberOfTries;
    [SerializeField] private CardButton[] cardButtons;
    HashSet<float> decimals = new HashSet<float>();
    public Inventory inventoryScript;
    public CardButton selectedCard1;
    public CardButton selectedCard2;
    public Beacon beaconScript;
    
    private void OnEnable()
    {
        ResetGame();
        List<int> availableIndices = ShuffleCards();
        int index = 0;
        int id = 1;
        // Time.timeScale = 0f;
        // Cursor.lockState = CursorLockMode.None;

        while (id <= 10)
        {
            int numerator = Random.Range(1, 20);      // 1–20
            int denominator = Random.Range(1, 20);    // 1–20 (avoid 0)

            float decimalValue = (float)numerator / denominator;
            float rounded = Mathf.Round(decimalValue * 10f) / 10f;
            // Debug.Log(numerator + "/" + denominator + " = " + decimalValue);
            if (decimalValue == rounded && !decimals.Contains(decimalValue))
            {   
                int index1 = availableIndices[index];
                index++;

                int index2 = availableIndices[index];
                index++;
                
                cardButtons[index1].SetCardValue(decimalValue.ToString());
                cardButtons[index2].SetCardValue(numerator + "/" + denominator);
                cardButtons[index1].SetID(id);
                cardButtons[index2].SetID(id);

                id++;
                decimals.Add(decimalValue);
                // Debug.Log("Added card with value: " + decimalValue + ", " + numerator + "/" + denominator );
            }
        }

        // SetNumberOfTries();
    }

    private List<int> ShuffleCards()
    {
        List<int> indices = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19};
        for (int i = 0; i < indices.Count; i++)
        {
            int randomIndex = Random.Range(i, indices.Count);

            int temp = indices[i];
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }
        return indices;
    }

    // private void SetNumberOfTries()
    // {
    //     numberOfTries = inventoryScript.getCount("ore");
    //     if (numberOfTries > 5)
    //     {
    //         numberOfTries = 5;
    //     }
    //     Debug.Log("You have " + inventoryScript.getCount("ore") + " ore. Total tries: " + numberOfTries);
    // }

    public void useOre(int amount)
    {
        inventoryScript.use("ore", amount);
    }

    public void AddCard(CardButton card)
    {
        // if (numberOfTries <= 0)
        // {
        //     Debug.Log("No more tries left!");
        //     return;
        // }

        if (selectedCard1 && selectedCard1 == card)
        {
            selectedCard1.SetDefaultColor();
            selectedCard1 = null;
            return;
        }

        if (selectedCard1)
        {
            selectedCard2 = card;
            selectedCard2.SetSelectedColor();
            CheckMatch();
        }
        else
        {
            selectedCard1 = card;
            selectedCard1.SetSelectedColor();
        }


    }

    private void CheckMatch()
    {
        if (selectedCard1.GetID() == selectedCard2.GetID())
        {
            // Debug.Log("Match found!");
            AudioManager.Instance.CorrectAnswer();
            selectedCard1.SetCorrectColor();
            selectedCard2.SetCorrectColor();    
            selectedCard1 = null;
            selectedCard2 = null;
            beaconScript.addTime(60f);
        }
        else
        {
            // Debug.Log("No match.");
            AudioManager.Instance.IncorrectAnswer();
            selectedCard1.SetDefaultColor();
            selectedCard2.SetDefaultColor();
            selectedCard1 = null;
            selectedCard2 = null;
        }
        // numberOfTries--;
        // Debug.Log("Tries remaining: " + numberOfTries);
        useOre(1);
    }

    public void ResetGame()
    {
        foreach (var card in cardButtons)
        {
            card.SetDefaultColor();
        }
        selectedCard1 = null;
        selectedCard2 = null;
        decimals.Clear();
        // SetNumberOfTries();
    }
}

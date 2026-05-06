using UnityEngine;
using System.Collections.Generic;

public class AmmoBoxPuzzle : MonoBehaviour
{
    public List<int> sequence = new List<int>();
    public int missingIndex;
    private bool playerInside;

    void Start()
    {
        GenerateBasicAdditionSequence();
        missingIndex = Random.Range(1, 5);
    }

    private void GenerateBasicAdditionSequence()
    {
        sequence.Clear();
        
        int startingNumber = Random.Range(1,10);
        sequence.Add(startingNumber);
        int difference = Random.Range(1,10);
        for (int i=0; i < 5; i++)
        {
            sequence.Add(sequence[i] + difference);
        }
        
    }

    public void CheckAnswer(int userInput)
    {
        if (userInput == sequence[missingIndex])
        {
            // Debug.Log("Correct!");
            AudioManager.Instance.CorrectAnswer();
            Inventory.Instance.add("ammo", 10);
        }
        else
        {
            // Debug.Log("Incorrect. Answer was: " + sequence[missingIndex]);
            AudioManager.Instance.IncorrectAnswer();
            // Add code to penalize player
        }
        PlayerCanvas.Instance.ResetRightClickGuide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            PlayerCanvas.Instance.RightClickGuide("Right-Click To Open Ammo Box");
            // Debug.Log("Right click to open puzzle");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCanvas.Instance.ResetRightClickGuide();
            playerInside = false;
        }
    }

    private void Update()
    {
        if (playerInside && Input.GetMouseButtonDown(1))
        {
            OpenPatternSequenceGame();
        }
        
    }

    public void OpenPatternSequenceGame()
    {
        AudioManager.Instance.OpenGame();
        PatternSequenceGame.Instance.Open(this);
    }
    
}

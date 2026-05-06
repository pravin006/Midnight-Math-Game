using UnityEngine;
using TMPro;


public class PatternSequenceGame : MonoBehaviour
{
    public static PatternSequenceGame Instance { get; private set; }
    [SerializeField] private TMP_InputField[] textFields;
    private AmmoBoxPuzzle currentBox;
    public GameObject gameCanvas;

    public PlayerMovement playerMovementScript;
    public MouseMovement mouseMovementScript;
    private bool isGameActive = false;
    public GameObject raycastWeapon;

    private void Awake()
    {
        Instance = this;
    }

    public void Open(AmmoBoxPuzzle box)
    {
        // Debug.Log($"textFields.Length={textFields.Length}, sequence.Count={box.sequence.Count}, missingIndex={box.missingIndex}");
        isGameActive = true;
        currentBox = box;
        gameCanvas.SetActive(true);

        playerMovementScript.enabled = false;
        mouseMovementScript.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        raycastWeapon.SetActive(false);

        textFields[box.missingIndex].text = "";
        for (int i = 0; i < box.sequence.Count; i++)
        {
            if (i != box.missingIndex)
            {
                textFields[i].text = box.sequence[i].ToString();
            }
        }
        Debug.Log("Current Sequence: " + string.Join(", ", box.sequence));
    }

    private void Update()
    {
        if (isGameActive && Input.GetKeyDown(KeyCode.E))
        {
            Close();
        }
    }


    public void CheckAnswer()
    {
        int userInput = int.Parse(textFields[currentBox.missingIndex].text);
        currentBox.CheckAnswer(userInput);
        Destroy(currentBox.gameObject);

    }


    public void Close()
    {
        gameCanvas.SetActive(false);

        playerMovementScript.enabled = true;
        mouseMovementScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        raycastWeapon.SetActive(true);

        isGameActive = false;
        currentBox = null;
    }
}

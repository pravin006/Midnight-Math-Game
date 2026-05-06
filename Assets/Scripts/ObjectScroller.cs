using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectScroller : MonoBehaviour
{
    public GameObject[] objectIndexesToScroll;
    private int currentObjectIndex = 0;
    public ObjectPlacer objectPlacerScript;
    public Image scrollerImage;
    public TMP_Text scrollerTextHP;
    public TMP_Text scrollerTextWood;
    public TMP_Text scrollerTextMetal;

    private void Start()
    {
        UpdateScroller();
    }

    private void Update()
    {
        if (objectPlacerScript._inPlacementMode)
        {
            return;
        }
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0f) 
        {
            return;
        }

        if (scroll > 0f)
        {
            currentObjectIndex = ScrollObjectsIndex(-1);
            
        }
        else if (scroll < 0f )
        {
            currentObjectIndex = ScrollObjectsIndex(1);
        }
        objectPlacerScript.currentObjectIndex = currentObjectIndex;
        UpdateScroller();
    }

    private int ScrollObjectsIndex(int direction)
    {
        currentObjectIndex += direction;

        if (currentObjectIndex >= objectIndexesToScroll.Length)
            currentObjectIndex = 0;
        else if (currentObjectIndex < 0)
            currentObjectIndex = objectIndexesToScroll.Length - 1;

        // Debug.Log("Current Object Index: " + currentObjectIndex);
        return currentObjectIndex;
    }

    private void UpdateScroller()
    {
        scrollerImage.sprite = objectIndexesToScroll[currentObjectIndex].GetComponent<ObjectVariables>().icon;
        scrollerTextHP.text = objectIndexesToScroll[currentObjectIndex].GetComponent<ObjectVariables>().maxHP.ToString()+ "HP";
        scrollerTextWood.text = objectIndexesToScroll[currentObjectIndex].GetComponent<ObjectVariables>().pinePerHPUnit.ToString() + " pine per "+ objectIndexesToScroll[currentObjectIndex].GetComponent<ObjectVariables>().pineHPUnit.ToString()+" HP";
        scrollerTextMetal.text = objectIndexesToScroll[currentObjectIndex].GetComponent<ObjectVariables>().oakPerHPUnit.ToString() + " oak per "+ objectIndexesToScroll[currentObjectIndex].GetComponent<ObjectVariables>().oakHPUnit.ToString()+" HP";
    }
}
using UnityEngine;
using UnityEngine.UI;

public class AccordionElement : MonoBehaviour
{
    public GameObject body;
    private bool isExpanded = false;

    void Start()
    {
        // Ensure it starts collapsed
        body.SetActive(false);
    }

    public void ToggleAccordion()
    {
        isExpanded = !isExpanded;
        body.SetActive(isExpanded);
        
        
    }
}
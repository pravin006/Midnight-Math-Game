using UnityEngine;

public class ObjectVariables : MonoBehaviour
{
    public int maxHP;
    public int pinePerHPUnit;
    public int pineHPUnit;
    public int oakPerHPUnit;
    public int oakHPUnit;
    public int pineCost;
    public int oakCost;
    public Sprite icon;

    private void Start()
    {
        CalculateCosts();
    }
    private void OnValidate()
    {
        CalculateCosts();
    }

    public void CalculateCosts()
    {
        pineCost = Mathf.CeilToInt((float)maxHP / pineHPUnit) * pinePerHPUnit;
        oakCost = Mathf.CeilToInt((float)maxHP / oakHPUnit) * oakPerHPUnit;
        // Debug.Log($"{gameObject.name} - Pine Cost: {pineCost}, Oak Cost: {oakCost}");
    }
}
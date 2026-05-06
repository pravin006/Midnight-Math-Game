using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public int pineCount = 100;
    public int oreCount = 10;
    public int oakCount = 100;
    public int ammoCount = 10;

    private int maxPine = 100;
    private int maxOre = 20;
    private int maxOak = 100;
    private int maxAmmo = 20;

    public TMP_Text pineCountText;
    public TMP_Text oakCountText;
    public TMP_Text oreCountText;
    public TMP_Text ammoCountText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateInventoryUI();
    }

    public void use(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "pine":
                pineCount -= amount;
                pineCountText.color = Color.black;
                if (pineCount <= 0) 
                {
                    pineCount = 0; 
                    pineCountText.color = Color.red;
                }
                break;
            case "ore":
                oreCount -= amount;
                oreCountText.color = Color.black;
                if (oreCount <= 0) 
                {
                    oreCount = 0;
                    oreCountText.color = Color.red;
                }
                break;
            case "oak":
                oakCount -= amount;
                oakCountText.color = Color.black;
                if (oakCount <= 0) 
                {
                    oakCount = 0;
                    oakCountText.color = Color.red;
                }
                break;
            case "ammo":
                ammoCount -= amount;
                ammoCountText.color = Color.black;
                if (ammoCount <= 0) 
                {
                    ammoCount = 0;
                    ammoCountText.color = Color.red;
                }
                break;
            default:
                Debug.Log("Invalid resource type: " + resourceType);
                break;
        }
        // Debug.Log("pine: " + pineCount + ", ore: " + oreCount + ", oak: " + oakCount + ", ammo: " + ammoCount);
        UpdateInventoryUI();
    }

    public void add(string resourceType, int amount)
    {
        switch (resourceType)
        {
            case "pine":
                pineCount += amount;
                pineCountText.color = Color.black;
                if (pineCount >= maxPine) 
                {
                    pineCount = maxPine;
                    pineCountText.color = Color.green;
                }
                break;
            case "ore":
                oreCount += amount;
                oreCountText.color = Color.black;
                if (oreCount >= maxOre)
                {
                    oreCount = maxOre;
                    oreCountText.color = Color.green;
                }
                break;
            case "oak":
                oakCount += amount;
                oakCountText.color = Color.black;
                if (oakCount >= maxOak)
                {
                    oakCount = maxOak;
                    oakCountText.color = Color.green;
                }
                break;
            case "ammo":
                ammoCount += amount;
                ammoCountText.color = Color.black;
                if (ammoCount >= maxAmmo)
                {
                    ammoCount = maxAmmo;
                    ammoCountText.color = Color.green;
                }
                break;
            default:
                Debug.Log("Invalid resource type: " + resourceType);
                break;
        }
        // Debug.Log("pine: " + pineCount + ", ore: " + oreCount + ", oak: " + oakCount + ", ammo: " + ammoCount);
        UpdateInventoryUI();
    }

    public int getCount(string resourceType)
    {
        switch (resourceType)
        {
            case "pine":
                return pineCount;
            case "ore":
                return oreCount;
            case "oak":
                return oakCount;
            case "ammo":
                return ammoCount;
            default:
                Debug.Log("Invalid resource type: " + resourceType);
                return -1;
        }
    }

    private void UpdateInventoryUI()
    {
        pineCountText.text = "x" + pineCount;
        oreCountText.text = "x" + oreCount;
        ammoCountText.text = "x" + ammoCount;
        oakCountText.text = "x" + oakCount;
    }


}

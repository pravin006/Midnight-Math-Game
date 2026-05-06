using UnityEngine;

public class TreeNode : MonoBehaviour
{
    public int hitsToChop = 3;
    public int woodGiven = 5;

    private int hitsLeft;
    private bool playerInside;

    private void Start()
    {
        hitsLeft = hitsToChop;
    }

    private void Update()
    {
        if (!playerInside) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            HitTree();
        }
    }

    private void HitTree()
    {
        hitsLeft--;
        // Debug.Log("Tree hit! Hits left: " + hitsLeft);

        if (hitsLeft <= 0)
        {
            ChopTree();
        }
    }

    private void ChopTree()
    {
        // Debug.Log("Tree chopped!");

        Inventory.Instance.add("wood", woodGiven);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            // Debug.Log("Press E to chop tree");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}

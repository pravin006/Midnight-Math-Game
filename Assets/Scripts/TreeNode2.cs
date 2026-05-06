using UnityEngine;

public class MineableObject : MonoBehaviour
{
    public int hitsToMine;
    public int quantityGiven;
    private int hitsLeft;
    private bool playerInside;
    public string resourceType;
    public AudioClip miningAudioClip;

    private void Start()
    {
        hitsLeft = hitsToMine;
    }

    private void Update()
    {
        if (!playerInside) return;

        if (Input.GetMouseButtonDown(1))
            HitMine();
    }

    private void HitMine()
    {
        AudioManager.Instance.Mine(miningAudioClip);
        hitsLeft--;
        // Debug.Log("Object hit! Hits left: " + hitsLeft);
        PlayerCanvas.Instance.RightClickGuide("Right-Click " + hitsLeft + " Times To Mine");
        if (hitsLeft <= 0)
            Mined();
    }

    private void Mined()
    {
        // Debug.Log("Object mined! +" + quantityGiven + " resources");
        PlayerCanvas.Instance.ResetRightClickGuide();
        Inventory.Instance.add(resourceType, quantityGiven);

        // Destroy the whole Tree root (parent), not just the trigger
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            PlayerCanvas.Instance.RightClickGuide("Right-Click " + hitsLeft + " Times To Mine");
            // Debug.Log("Press E to mine object");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            PlayerCanvas.Instance.ResetRightClickGuide();
            playerInside = false;
    }
}
using UnityEngine;


public class GeneratorBoxOpenClose : MonoBehaviour
{
    public GameObject cardMatchingGameObject;
    public PlayerMovement playerMovementScript;
    public MouseMovement mouseMovementScript;
    private bool playerInside = false;
    public GameObject raycastWeapon;


    private void Update()
    {
        if (!playerInside) return;

        if (Input.GetMouseButtonDown(1))
        {
            if (cardMatchingGameObject.activeSelf)
            {
                Resume();
            }
            else
            {
                AudioManager.Instance.OpenGame();
                cardMatchingGameObject.SetActive(true);
                raycastWeapon.SetActive(false);
                // Time.timeScale = 0f;
                // Cursor.lockState = CursorLockMode.None;

                playerMovementScript.enabled = false;
                mouseMovementScript.enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCanvas.Instance.RightClickGuide("Right-Click To Open Generator");
            playerInside = true;
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

    public void Resume()
    {
        cardMatchingGameObject.SetActive(false);
        raycastWeapon.SetActive(true);
        // Time.timeScale = 1f;
        // Cursor.lockState = CursorLockMode.Locked;

        playerMovementScript.enabled = true;
        mouseMovementScript.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

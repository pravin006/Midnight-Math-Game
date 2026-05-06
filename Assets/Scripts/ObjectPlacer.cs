using UnityEngine;
using System.Collections.Generic;

public class ObjectPlacer : MonoBehaviour
{
    [Header("Placement Parameters")]
    [SerializeField] public GameObject[] placeableObjectPrefabs;
    [SerializeField] public GameObject[] previewObjectPrefabs;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private LayerMask placementSurfaceLayerMask;
    public int currentObjectIndex = 0;

    [Header("Preview Material")]
    [SerializeField] private Material previewMaterial;
    [SerializeField] private Color validColor;
    [SerializeField] private Color invalidColor;

    [Header("Raycast Parameters")]
    [SerializeField] private float objectDistanceFromPlayer;
    [SerializeField] private float raycastStartVerticalOffset;
    [SerializeField] private float raycastDistance;

    [Header("Disable Weapon")]
    // [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject raycastWeapon;


    private GameObject _previewObject = null;
    private Vector3 _currentPlacementPosition = Vector3.zero;
    public bool _inPlacementMode = false;
    private bool _validPreviewState = false;

    private void Update()
    {
        UpdateInput();
        if (_inPlacementMode)
        {
            UpdateCurrentPlacementPosition();

            if (CanPlaceObject())
            {
                SetValidPreviewState();
            }
            else
            {
                SetInvalidPreviewState();
            }
        }
    }

    private void UpdateCurrentPlacementPosition()
    {
        Vector3 cameraForward = new Vector3(playerCamera.transform.forward.x, 0f, playerCamera.transform.forward.z);
        cameraForward.Normalize();

        Vector3 startPos = playerCamera.transform.position + (cameraForward * objectDistanceFromPlayer);
        startPos.y += raycastStartVerticalOffset;

        RaycastHit hitInfo;
        if (Physics.Raycast(startPos, Vector3.down, out hitInfo, raycastDistance, placementSurfaceLayerMask))
        {
            _currentPlacementPosition = hitInfo.point;
        }

        Quaternion rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
        _previewObject.transform.position = _currentPlacementPosition;
        _previewObject.transform.rotation = rotation;
    }

    private void UpdateInput()
    {
        if (Input.GetMouseButtonDown(2) && !_inPlacementMode)
        {
            EnterPlacementMode();
        }
        else if (Input.GetMouseButtonDown(2) && _inPlacementMode)
        {
            ExitPlacementMode();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
    }

    private void EnterPlacementMode()
    {
        // Debug.Log("Entered placement mode");
        // weapon.SetActive(false);
        raycastWeapon.SetActive(false);
        Quaternion rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
        _previewObject = Instantiate(previewObjectPrefabs[currentObjectIndex], _currentPlacementPosition, rotation, transform);
        // Debug.Log($"Preview Object: {_previewObject.name}");
        _inPlacementMode = true;
        
    }

    private void ExitPlacementMode()
    {
        // Debug.Log("Exited placement mode");
        // weapon.SetActive(true);
        raycastWeapon.SetActive(true);
        Destroy(_previewObject);
        _previewObject = null;
        _inPlacementMode = false;
    }

    private void PlaceObject()
    {
        if (!_inPlacementMode || !_validPreviewState)
        {
            return;
        }
        // Debug.Log("Attempting to place object");
        // Debug.Log("You have "+ Inventory.Instance.pineCount +" pine. You need "+ placeableObjectPrefabs[currentObjectIndex].GetComponent<ObjectVariables>().pineCost);
        // Debug.Log("You have "+ Inventory.Instance.oakCount +" oak. You need " + placeableObjectPrefabs[currentObjectIndex].GetComponent<ObjectVariables>().oakCost );
        if (placeableObjectPrefabs[currentObjectIndex].GetComponent<ObjectVariables>().pineCost <= Inventory.Instance.pineCount && placeableObjectPrefabs[currentObjectIndex].GetComponent<ObjectVariables>().oakCost <= Inventory.Instance.oakCount)
        {
            Quaternion rotation = Quaternion.Euler(0f, playerCamera.transform.eulerAngles.y, 0f);
            Instantiate(placeableObjectPrefabs[currentObjectIndex], _currentPlacementPosition, rotation, transform);
            // Debug.Log($"Placed Object: {placeableObjectPrefabs[currentObjectIndex].name}");
            Inventory.Instance.use("pine", placeableObjectPrefabs[currentObjectIndex].GetComponent<ObjectVariables>().pineCost);
            Inventory.Instance.use("oak", placeableObjectPrefabs[currentObjectIndex].GetComponent<ObjectVariables>().oakCost);
        }
        else
        {
            
            Debug.Log("Not enough resources to place object");
        }
        ExitPlacementMode();
    }

    private void SetValidPreviewState()
    {
        previewMaterial.color = validColor;
        _validPreviewState = true;
    }

    private void SetInvalidPreviewState()
    {
        previewMaterial.color = invalidColor;
        _validPreviewState = false;
    }

    private bool CanPlaceObject()
    {
        if (_previewObject == null)
            return false;

        return _previewObject.GetComponentInChildren<PreviewObjectValidChecker>().IsValid;
    }
}

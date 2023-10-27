using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;

public class RaycastHandler : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] float raycastLength = 5f;
    [SerializeField] Image reticle;
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color interactColor = Color.green;
    [SerializeField] TextMeshProUGUI promptTextM;
    [SerializeField] TextMeshProUGUI promptTextE;
    [SerializeField] PlayerInput brainCamera;

    [Header("Material Selection UI")]
    [SerializeField] GameObject materialCanvas;
    [SerializeField] Transform contentTransform;
    [SerializeField] GameObject materialSelectionPrefab;


    private RaycastHit hit;
    private GameObject currentTarget;

    private void Start()
    {
        if (promptTextE != null)
            promptTextE.enabled = false;

        if (promptTextM != null)
            promptTextM.enabled = false;

        if (reticle != null)
            reticle.color = normalColor;

        if (materialCanvas != null)
            materialCanvas.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        bool isTargeted = false;

        if (Physics.Raycast(ray, out hit, raycastLength))
        {
            if (hit.collider.CompareTag("Switch"))
            {
                HandleSwitchInteraction();
                isTargeted = true;
            }
            else if (hit.collider.CompareTag("Material"))
            {
                HandleMaterialInteraction();
                isTargeted = true;
            }
            else if (hit.collider.CompareTag("Drawer"))
            {
                HandleDrawerInteraction();
                isTargeted = true;
            }

        }

        if (!isTargeted)
            ResetInteractions();
    }
    void HandleDrawerInteraction()
    {
        currentTarget = hit.collider.gameObject;

        if (promptTextE != null)
        {
            promptTextE.enabled = true;
        }

        if (reticle != null)
        {
            reticle.color = interactColor;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DrawerController drawerController = currentTarget.GetComponent<DrawerController>();
            if (drawerController != null)
            {
                drawerController.ToggleDrawer();
            }
        }
    }

    void HandleSwitchInteraction()
    {
        currentTarget = hit.collider.gameObject;

        if (promptTextE != null)
            promptTextE.enabled = true;

        if (reticle != null)
            reticle.color = interactColor;

        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchController switchController = currentTarget.GetComponent<SwitchController>();
            if (switchController != null)
                switchController.ToggleLight();
        }
    }

    void HandleMaterialInteraction()
    {
        currentTarget = hit.collider.gameObject;

        if (promptTextM != null)
            promptTextM.enabled = true;

        if (reticle != null)
            reticle.color = interactColor;

        if (Input.GetKeyDown(KeyCode.M))
        {
            brainCamera.enabled = false;
            MaterialInfo materialInfo = currentTarget.GetComponent<MaterialInfo>();
            if (materialInfo != null)
            {
                ShowMaterialSelection(materialInfo.AllowedMaterials);
            }
        }
    }

    void ShowMaterialSelection(MaterialsList list)
    {
        if (materialCanvas != null)
        {
            materialCanvas.SetActive(true);

            foreach (Transform child in contentTransform)
                Destroy(child.gameObject);

            foreach (MaterialChoice choice in list.materials)
            {
                GameObject item = Instantiate(materialSelectionPrefab, contentTransform);
                item.GetComponent<MaterialSelectionItem>().Initialize(choice, currentTarget);
            }
        }
    }

    void ResetInteractions()
    {
        if (promptTextE != null)
            promptTextE.enabled = false;

        if (promptTextM != null)
            promptTextM.enabled = false;

        if (reticle != null)
            reticle.color = normalColor;

        currentTarget = null;

        if (materialCanvas != null)
            materialCanvas.SetActive(false);

        if (brainCamera != null)
            brainCamera.enabled = true;
    }
}
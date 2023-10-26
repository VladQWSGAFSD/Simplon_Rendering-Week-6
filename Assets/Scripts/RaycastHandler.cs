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

    [Header("Material Data")]
    [SerializeField] MaterialsList materialsList;

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
        }

        if (!isTargeted)
            ResetInteractions();
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
            ShowMaterialSelection();
    }

    void ShowMaterialSelection()
    {
        if (materialCanvas != null)
        {
            brainCamera.enabled = false;
            materialCanvas.SetActive(true);

            foreach (Transform child in contentTransform)
                Destroy(child.gameObject);

            foreach (MaterialChoice choice in materialsList.materials)
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

    //each object with a material tag will have to have a component MaterialInfo, cause based on each one certain materials will be able
    // to be applied and not just one list     [SerializeField] MaterialsList materialsList; like here.
}

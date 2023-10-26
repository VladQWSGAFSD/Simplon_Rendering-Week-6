using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialSelectionItem : MonoBehaviour
{
    [SerializeField] Image materialImage;
    [SerializeField] TextMeshProUGUI materialNameText;
    [SerializeField] Button applyButton;

    private MaterialChoice materialChoice;
    private GameObject target;

    public void Initialize(MaterialChoice choice, GameObject targetObject)
    {
        materialChoice = choice;
        target = targetObject;

        materialImage.sprite = choice.materialSprite;
        materialNameText.text = choice.materialName;

        applyButton.onClick.AddListener(ApplyMaterial);
    }

    void ApplyMaterial()
    {
        if (target != null)
        {
            Renderer rend = target.GetComponent<Renderer>();
            if (rend != null)
                rend.material = materialChoice.material;
        }
    }
}

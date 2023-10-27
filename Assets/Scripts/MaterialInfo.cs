using UnityEngine;

public class MaterialInfo : MonoBehaviour
{
    [SerializeField] MaterialsList allowedMaterials;

    public MaterialsList AllowedMaterials => allowedMaterials;
}

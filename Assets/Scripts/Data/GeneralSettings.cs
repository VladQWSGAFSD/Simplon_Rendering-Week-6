using UnityEngine;

[CreateAssetMenu(fileName = "GeneralSettings", menuName = "ScriptableObjects/GeneralSettings", order = 2)]
public class GeneralSettings : ScriptableObject
{
    [Header("General Settings")]
    public float raycastLength = 5f;
    public Color normalColor = Color.white;
    public Color interactColor = Color.green;
}

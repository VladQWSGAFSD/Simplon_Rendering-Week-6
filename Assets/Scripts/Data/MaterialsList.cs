using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MaterialsList", menuName = "ScriptableObjects/MaterialsList", order = 1)]
public class MaterialsList : ScriptableObject
{
    public MaterialChoice[] materials;
}

[Serializable]
public class MaterialChoice
{
    public string materialName;
    public Sprite materialSprite;
    public Material material;

}
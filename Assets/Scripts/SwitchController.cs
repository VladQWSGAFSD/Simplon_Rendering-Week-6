using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject[] controlledLights;

    public void ToggleLight()
    {
        if (controlledLights != null && controlledLights.Length > 0)
        {
            foreach (GameObject light in controlledLights)
            {
                if (light != null)
                {
                    light.SetActive(!light.activeSelf);
                }
            }
        }
    }
}

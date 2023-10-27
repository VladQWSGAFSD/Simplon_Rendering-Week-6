using UnityEngine;
using UnityEngine.SceneManagement;

public class KeysInputManager : MonoBehaviour
{
    [SerializeField] GameObject torch;
    [SerializeField] GameObject materialsCanvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("0 ressed");
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("1 pressed");
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("2 pressed");
            SceneManager.LoadScene(2);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            torch.SetActive(!torch.activeSelf);
        }
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    materialsCanvas.SetActive(false);  
    }
}

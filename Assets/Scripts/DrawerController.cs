using UnityEngine;
using System.Collections;

public class DrawerController : MonoBehaviour
{
    [SerializeField] private Vector3 openPosition; 
    private Vector3 closePosition; 
    private Vector3 targetPosition; 

    [SerializeField] private float openSpeed = 0.2f; 

    private bool isOpen = false;
    private bool isMoving = false; 

    private void Start()
    {
        closePosition = transform.position;
    }

#if UNITY_EDITOR
    
    public void SetOpenOffset()
    {
        openPosition = transform.position - closePosition;
    }
#endif

    public void ToggleDrawer()
    {
        if (isMoving) 
            return;

        if (isOpen)
        {
            targetPosition = closePosition;
            StartCoroutine(MoveDrawer());
        }
        else
        {
            targetPosition =  openPosition;
            StartCoroutine(MoveDrawer());
        }
    }

    private IEnumerator MoveDrawer()
    {
        isMoving = true;
        float timeStartedMoving = Time.time;
        float journeyLength = Vector3.Distance(transform.position, targetPosition);

        float distanceCovered = (Time.time - timeStartedMoving) * openSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - timeStartedMoving) * openSpeed;
            fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(transform.position, targetPosition, fractionOfJourney);
            yield return null;
        }

        transform.position = targetPosition;

        isOpen = !isOpen; 
        isMoving = false;
    }
}

using UnityEngine;

public class TriggerPart : MonoBehaviour
{
    private bool entered = false;
    public TriggerCapsula parent;

    private void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("Trigger");
        if (other.CompareTag("Player") & !entered)
        {
            Debug.Log("Trigger no Player");
            parent.OnPartTriggerEnter();
            entered = true;
        }
    }
}

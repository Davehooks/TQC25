using UnityEngine;
using UnityEngine.UI;

public class SetPauseCamera : MonoBehaviour
{
    public static SetPauseCamera instance;

    private Canvas canva;
    [SerializeField] private Camera[] cameras;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canva = GetComponent<Canvas>();
    }
    public void SetCamera(Camera camera)
    {
        canva.worldCamera = camera;
    }
}

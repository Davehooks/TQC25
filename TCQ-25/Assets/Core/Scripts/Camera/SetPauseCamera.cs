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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        canva = GetComponent<Canvas>();
    }
    public void SetCamera()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].gameObject.activeInHierarchy)
            {
                canva.worldCamera = cameras[i];
            }
            else
            {
                return;
            }
        }
    }
}

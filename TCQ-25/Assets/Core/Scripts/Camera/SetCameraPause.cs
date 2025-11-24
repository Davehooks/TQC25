using UnityEngine;

public class SetCameraPause : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        SetPauseCamera.instance.SetCamera(camera);
    }

}

using UnityEngine;

public class CameraControl : MonoBehaviour
{ 
    private void Start()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f , 0f);
    }
}

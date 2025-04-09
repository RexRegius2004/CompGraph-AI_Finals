using UnityEngine;

public class MovableObstacle : MonoBehaviour
{
    [SerializeField] private float SpeedForward;
    [SerializeField] private float SpeedBackward;

    private bool isSwitchDir;
    void Start()
    {
        
    }

    void Update()
    {
        if (!isSwitchDir)
        {
            transform.Translate(Vector3.forward * SpeedForward * Time.deltaTime);
        }
        else 
        { 
            transform.Translate(Vector3.back * SpeedBackward * Time.deltaTime);
        }

        if (transform.position.z >= 20)
        {
            isSwitchDir = true;
        }
        else if (transform.position.z <= -20)
        {
            isSwitchDir = false;
        }

    }
}

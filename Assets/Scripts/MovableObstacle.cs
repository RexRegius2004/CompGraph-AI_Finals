using UnityEngine;

public class MovableObstacle : MonoBehaviour
{
    [SerializeField] private float SpeedForward;
    [SerializeField] private float SpeedBackward;

    private bool isSwitchDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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

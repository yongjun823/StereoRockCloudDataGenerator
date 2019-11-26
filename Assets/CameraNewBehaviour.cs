using UnityEngine;

public class CameraNewBehaviour : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 30 * Time.deltaTime);
    }
}

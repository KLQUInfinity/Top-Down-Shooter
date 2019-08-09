using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private float smooth = 8f;

    void Update()
    {
        Vector3 offset = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.Lerp(transform.position, offset, Time.deltaTime * smooth);
    }
}

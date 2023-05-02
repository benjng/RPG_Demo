using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] private float height;
    private float offset;
    [SerializeField] private float distance;
    [SerializeField] private float rotateX;
    [SerializeField] private float rotateY;
    [SerializeField] private float rotateZ;

    // Start is called before the first frame update
    void Start()
    {
        offset = height;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = playerTransform.position + new Vector3(offset, height, -distance);
        transform.rotation = Quaternion.Euler(rotateX, rotateY, rotateZ);
    }
}

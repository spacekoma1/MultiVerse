using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEx : MonoBehaviour
{
    Transform transform;
    Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rotation = transform.localRotation;
        rotation *= Quaternion.Euler(60, 0, 0);
        transform.localRotation = rotation;

        Debug.Log(rotation.w);
        Debug.Log(rotation.x);
        Debug.Log(rotation.y);
        Debug.Log(rotation.z);
        Debug.Log(transform.localEulerAngles.x);
        Debug.Log(Mathf.Acos(rotation.w) * 2.0f * Mathf.Rad2Deg);
        float qx = rotation.x / rotation.w;
        Debug.Log(2.0f * Mathf.Rad2Deg * Mathf.Atan(qx));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

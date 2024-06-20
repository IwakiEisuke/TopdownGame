using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CameraTargetController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            TranslateCamera(Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            TranslateCamera(Vector3.down);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            TranslateCamera(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TranslateCamera(Vector3.right);
        }
    }

    void TranslateCamera(Vector3 direction)
    {
        transform.Translate(direction);
    }
}

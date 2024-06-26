using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    public float swingDuration, swingAngle;
    public float startTime, basisAngle;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var t = (Time.time - startTime) / swingDuration;
        if (t <= 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(basisAngle - swingAngle / 2, basisAngle + swingAngle / 2, t));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

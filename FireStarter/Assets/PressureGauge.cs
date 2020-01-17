using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureGauge : MonoBehaviour
{
    GameObject pointer;
    public float start_angle;
    public float end_angle;
    private float ratio = 0f;

    // Start is called before the first frame update
    void Start()
    {
        pointer = transform.Find("Cylinder").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Lerp(start_angle, end_angle, ratio);
        pointer.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void SetValue(float inAngle)
    {
        ratio = inAngle;
    }
}

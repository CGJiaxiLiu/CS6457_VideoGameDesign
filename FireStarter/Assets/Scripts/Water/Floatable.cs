using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floatable : MonoBehaviour
{
    private GameObject waterSurface;
    private ConfigurableJoint cj;
    public GameObject anchor_prefab;
    private GameObject anchor;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = Mathf.Max(1.0f, rb.drag);
        rb.angularDrag = Mathf.Max(1.0f, rb.angularDrag);

        waterSurface = GameObject.FindGameObjectsWithTag("Water")[0];

        if(anchor)
        {
            anchor = Instantiate(anchor_prefab);
        }
        else
        {
            anchor = Instantiate(Misc.Get().anchor_prefab);
        }

        Vector3 anchorPosition = new Vector3();
        anchorPosition.y = waterSurface.transform.position.y;
        anchorPosition.x = gameObject.transform.position.x;
        anchorPosition.z = gameObject.transform.position.z;
        anchor.transform.SetParent(waterSurface.transform);
        anchor.transform.position = anchorPosition;

        cj = gameObject.AddComponent(typeof(ConfigurableJoint)) as ConfigurableJoint;
        cj.autoConfigureConnectedAnchor = false;
        JointDrive x_drive = new JointDrive();
        x_drive.maximumForce = 10 * rb.mass;
        x_drive.positionSpring = 100000.0f * rb.mass;
        x_drive.positionDamper = 0.1f * rb.mass;
        cj.xDrive = x_drive;
        cj.yDrive = x_drive;
        cj.zDrive = x_drive;
        cj.anchor = Vector3.zero;
        cj.connectedAnchor = Vector3.zero; ;
        cj.connectedBody = anchor.GetComponent<Rigidbody>();
    }
}

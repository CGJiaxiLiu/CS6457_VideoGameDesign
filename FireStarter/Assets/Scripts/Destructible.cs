using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject FragmentPrefab;
    public bool GenerateBreakEvent = false;
    public string BreakEventName;
    public bool fix = false;


    private void Start()
    {
        
    }

    virtual public void OnDestruct()
    {
        if(GenerateBreakEvent)
        {
            InteractiveEventListener.Get().DispatchEvent(BreakEventName);
        }

        var temp = GameObject.Instantiate(FragmentPrefab, transform.position, transform.rotation);

        if(fix)
        {
            Vector3 scale = temp.transform.localScale;
            scale.y = gameObject.transform.localScale.y / 5.2f;
            scale.x = gameObject.transform.localScale.x / 4.3f;
            scale.z = gameObject.transform.localScale.z / 0.1f;
            temp.transform.localScale = scale;
        }

        Destroy(gameObject);
    }
}

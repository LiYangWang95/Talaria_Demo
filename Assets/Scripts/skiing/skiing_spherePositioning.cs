using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skiing_spherePositioning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.transform.position + new Vector3(0, 5, 0), Vector3.down, out hit, Mathf.Infinity, 1<<10))
               this.transform.position = hit.point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skiing_buildRoute : MonoBehaviour
{
    [HideInInspector]public List<Transform> allPoints;
    // Start is called before the first frame update
    private void Awake()
    {
        allPoints = new List<Transform>();
        for(int i = 0; i < this.transform.childCount; i++){
            allPoints.Add(this.transform.GetChild(i).transform);
        }
    }

}

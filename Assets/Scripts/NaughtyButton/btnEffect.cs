using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnEffect : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(other.tag == "Btn"){
            print("Enter");
            btnEffectSetting btnSetting = other.transform.parent.gameObject.GetComponent<btnEffectSetting>();
            btnSetting.changeButton();
        }
    }

    void OnTriggerExit(Collider other){
        if(other.tag == "Btn"){
            print("Exit");
            btnEffectSetting btnSetting = other.transform.parent.gameObject.GetComponent<btnEffectSetting>();
            btnSetting.revert();
        }
    }
}

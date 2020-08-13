using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnEffectSettingV2 : MonoBehaviour
{
    public deformAPI Deform;
    [Header("Deformation Setting")]
    public float minimumY = 1f;
    public float maximumY = 2f;
    public float transformTime = 0.5f;
    [Header("Bounce Setting")]
    public float bounceDuration = 3f;
    // ButtonStates
    public enum BtnType{
        Bounce,
        LRShake,
        FBShake,
        MainTilt,
        SubTilt,
    }

    private bool transformed;
    private bool functionRunning;
    // Start is called before the first frame update
    void Start()
    {
        transformed = functionRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        settingUpdate();
        if(Input.GetKeyDown(KeyCode.Space) && !functionRunning){
            StartCoroutine(Bounce(bounceDuration));
        }
    }

    IEnumerator Bounce(float time){
        float functionTime = 0;
        functionRunning = true;
        while(functionTime < 1){
            functionTime += Time.deltaTime/time;
            if(!Deform.transforming){
                if(!transformed){
                    Deform.Form = deformAPI.deformType.All;
                    transformed = true;
                }
                else{
                    Deform.Form = deformAPI.deformType.Recover;
                    transformed = false;
                }
            }
            yield return null;
        }
        yield return null;
    }

    void settingUpdate(){
        Deform.minimumY = minimumY;
        Deform.maximumY = maximumY;
        Deform.transformTime = transformTime;
    }
}

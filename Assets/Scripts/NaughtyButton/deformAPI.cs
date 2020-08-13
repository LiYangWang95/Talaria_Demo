using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deformAPI : MonoBehaviour
{
    public deformController deformCore;
    [HideInInspector] public float minimumY = 1f;
    [HideInInspector] public float maximumY = 2f;
    [HideInInspector] public float transformTime = 0.5f;
    [HideInInspector] public bool transforming;
    private float fullLerpValue;
    private float halfLerpValue;
    public enum deformType{
        None = 0,
        Front = 1,
        FrontRight = 2,
        Right = 3,
        BackRight = 4,
        Back = 5,
        BackLeft = 6,
        Left = 7,
        FrontLeft = 8,
        All = 9,
        Recover = 10,
    }
    [HideInInspector] public deformType Form;
    [HideInInspector] public deformType prevForm;
    void Start()
    {
        Form = prevForm = deformType.None;
        transforming = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* Testing */
        if(Input.GetKeyDown(KeyCode.Keypad1)){
            Form = deformType.Front;
        }
        if(Input.GetKeyDown(KeyCode.Keypad2)){
            Form = deformType.FrontRight;
        }
        if(Input.GetKeyDown(KeyCode.Keypad3)){
            Form = deformType.Right;
        }
        if(Input.GetKeyDown(KeyCode.Keypad4)){
            Form = deformType.BackRight;
        }
        if(Input.GetKeyDown(KeyCode.Keypad5)){
            Form = deformType.Back;
        }
        if(Input.GetKeyDown(KeyCode.Keypad6)){
            Form = deformType.BackLeft;
        }
        if(Input.GetKeyDown(KeyCode.Keypad7)){
            Form = deformType.Left;
        }
        if(Input.GetKeyDown(KeyCode.Keypad8)){
            Form = deformType.FrontLeft;
        }
        if(Input.GetKeyDown(KeyCode.Keypad9)){
            Form = deformType.All;
        }
        if(Input.GetKeyDown(KeyCode.Keypad0)){
            Form = deformType.Recover;
        }
        deform();
    }

    void deform(){
        if(Form != deformType.None){
            // Obtain transform value
            if(!transforming){
                // Transform
                if(Form != deformType.Recover){
                    StartCoroutine(transformTimer(transformTime, minimumY, maximumY, false));
                }
                // Recover
                else{
                    StartCoroutine(transformTimer(transformTime, maximumY, minimumY, true));
                }
            }
            // Set the transform values
            switch(Form){
                case deformType.Front:
                    prevForm = deformType.Front;
                    deformCore.leftTopFront.y = fullLerpValue;
                    deformCore.rightTopFront.y = fullLerpValue;
                    break;
                case deformType.FrontRight:
                    prevForm = deformType.FrontRight;
                    deformCore.leftTopFront.y = halfLerpValue;
                    deformCore.rightTopFront.y = fullLerpValue;
                    deformCore.rightTopBack.y = halfLerpValue;
                    break;
                case deformType.Right:
                    prevForm = deformType.Right;
                    deformCore.rightTopFront.y = fullLerpValue;
                    deformCore.rightTopBack.y = fullLerpValue;
                    break;
                case deformType.BackRight:
                    prevForm = deformType.BackRight;
                    deformCore.rightTopFront.y = halfLerpValue;
                    deformCore.rightTopBack.y = fullLerpValue;
                    deformCore.leftTopBack.y = halfLerpValue;
                    break;
                case deformType.Back:
                    prevForm = deformType.Back;
                    deformCore.rightTopBack.y = fullLerpValue;
                    deformCore.leftTopBack.y = fullLerpValue;
                    break;
                case deformType.BackLeft:
                    prevForm = deformType.BackLeft;
                    deformCore.rightTopBack.y = halfLerpValue;
                    deformCore.leftTopBack.y = fullLerpValue;
                    deformCore.leftTopFront.y = halfLerpValue;
                    break;
                case deformType.Left:
                    prevForm = deformType.Left;
                    deformCore.leftTopFront.y = fullLerpValue;
                    deformCore.leftTopBack.y = fullLerpValue;
                    break;
                case deformType.FrontLeft:
                    prevForm = deformType.FrontLeft;
                    deformCore.leftTopBack.y = halfLerpValue;
                    deformCore.leftTopFront.y = fullLerpValue;
                    deformCore.rightTopFront.y = halfLerpValue;
                    break;
                case deformType.All:
                    prevForm = deformType.All;
                    deformCore.leftTopFront.y = fullLerpValue;
                    deformCore.leftTopBack.y = fullLerpValue;
                    deformCore.rightTopFront.y = fullLerpValue;
                    deformCore.rightTopBack.y = fullLerpValue;
                    break;
                case deformType.Recover:
                    Form = prevForm;
                    break;
                }
            // Update transform
            deformCore.btnDeform();
        }
    }

    IEnumerator transformTimer(float time, float minValue, float maxValue, bool recover){
        transforming = true;
        float normalizedTime = 0;
        fullLerpValue = halfLerpValue = minValue;
        while(normalizedTime < 1){
            normalizedTime += Time.deltaTime/time;
            fullLerpValue = Mathf.Lerp(minValue, maxValue, normalizedTime);
            halfLerpValue = recover? Mathf.Lerp(minValue/2, maxValue, normalizedTime): Mathf.Lerp(minValue, maxValue/2, normalizedTime);
            yield return null;
        }
        fullLerpValue = maxValue;
        halfLerpValue = maxValue/2;
        transforming = false;
        Form = deformType.None;
        yield return null;
    }
}

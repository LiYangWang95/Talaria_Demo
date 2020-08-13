using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class btnEffectSetting : MonoBehaviour
{
    #region Enum Declare
    public enum BtnType{
        Tilt,
        Deform,
        Shake,
        Decline
    }
    public enum TiltType{
        PositiveX = 1,
        NegativeX = 5,
        PositiveZ = 2,
        NegativeZ = 6
    }
    public enum DeformType{
        PositiveForward = 0,
        NegativeForward = 1,
        PositiveRight = 2,
        NegativeRight = 3
    }
    public enum ShakeType{
        X = 1,
        Z = 2,
        XZ = 3
    }
    #endregion
    #region Common
    [Header("Common Setting")]
    public BtnType btnType = BtnType.Deform;
    private bool _isReverting = false;
    public bool isReverting{
        get{
            return _isReverting;
        }
        set{
            _isReverting = value;
        }
    }
    private bool _isChanging = false;
    public bool isChanging{
        get{
            return _isChanging;
        }
        set{
            _isChanging = value;
        }
    }
    Vector3 originPosition;
    Quaternion originRotation;

    #endregion

    #region Tilt 

    [Header("Tilt Setting")]
    [Range(0.0f,10.0f)]public float tiltDuration = 5.0f;
    public TiltType tiltState = TiltType.PositiveX;
    [Range(0.0f, 10.0f)]public float tiltDegree = 5f;
    Quaternion targetRotation;

    #endregion

    #region Deform 

    [Header("Deform Setting")]
    public deformController deformController;
    [Range(0.0f,10.0f)]public float deformDuration = 5.0f;
    public DeformType deformState = DeformType.PositiveForward;
    [Range(0.0f, 1f)]public float deformValue = 0.5f;
    public float currentDeformValue = 0.0f;
    int[] direction;
    #endregion

    #region Decline

    [Header("Decline Setting")]
    [Range(0.0f,10.0f)]public float declineDuration = 5.0f;
    [Range(0.0f, 5.0f)]public float declineDistance = 1f;
    Vector3 targetPosition;

    #endregion

    #region Shake

    [Header("Shake Setting")]
    public ShakeType shakeState = ShakeType.X;
    [Range(0.0f, 30.0f)]public float shakeDuration = 10f;
    [Range(0.0f, 30.0f)]public float shakeStrength = 1f;
    public int shakeVibrato = 10;
    [Range(0.0f, 180.0f)]public float shakeRandomness = 90f;
    public bool shakeSnapping = false;
    public bool shakeFadeOut = false;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            revert();
        }
        if(Input.GetKeyDown(KeyCode.C)){
            changeButton();
        }
    }

    public void changeButton(){
        if(isChanging) return;
        switch (btnType)
        {
            case BtnType.Tilt:{
                tilt();
                break;
            }
            case BtnType.Deform:{
                deform();
                break;
            }
            case BtnType.Decline:{
                decline();
                break;
            }
            case BtnType.Shake:{
                shake();
                break;
            }
            default:{
                break;
            }
        }
        isReverting = false;
        isChanging = true;
    }


    public void revert(){
        if(isReverting) return;
        switch (btnType)
        {
            case BtnType.Tilt:{
                transform.DORotateQuaternion(originRotation, tiltDuration).OnComplete(delegate{isReverting = false;});
                break;
            }
            case BtnType.Deform:{
                DOTween.To(deformMethod, currentDeformValue, 0, deformDuration).OnComplete(delegate{isReverting = false;});
                break;
            }
            case BtnType.Decline:{
                transform.DOMoveY(originPosition.y, declineDuration).OnComplete(delegate{isReverting = false;});
                break;
            }
            case BtnType.Shake:{
                break;
            }
            default:{
                break;
            }
        }
        isReverting = true;
        isChanging = false;
    }

    void tilt(){
        String tiltBitnum = Convert.ToString((int)tiltState,2).PadLeft(3, '0');
        int[] tiltBits =  tiltBitnum.ToCharArray().Select(c=>c-48).ToArray();
        Vector3 targetEuler = new Vector3(tiltBits[2], 0, tiltBits[1]) * (float)(Math.Pow(-1, tiltBits[0]) * tiltDegree);
        targetRotation = Quaternion.Euler(targetEuler);
        transform.DORotateQuaternion(targetRotation, tiltDuration).OnComplete(delegate{isChanging = false;});
    }

    void deform(){
        String deformBitnum = Convert.ToString((int)deformState,2).PadLeft(2, '0');
        int[] deformBits =  deformBitnum.ToCharArray().Select(c=>c-48).ToArray();
        direction = new int[4];
        direction[0] = -(deformBits[0] ^ deformBits[1]) + 1;    //left front
        direction[1] = (deformBits[1]);                         //left back
        direction[2] = -(deformBits[1]) + 1;                    //right front
        direction[3] = (deformBits[0] ^ deformBits[1]);         //right back
        DOTween.To(deformMethod, currentDeformValue, deformValue, deformDuration).OnComplete(delegate{isChanging = false;});
    }
    void decline(){
        targetPosition = originPosition - new Vector3(0, declineDistance, 0);
        transform.DOMoveY(originPosition.y - declineDistance, declineDuration).OnComplete(delegate{isChanging = false;});
    }

    void shake(){
        String shakeBitnum = Convert.ToString((int)shakeState,2).PadLeft(2, '0');
        int[] shakeBits =  shakeBitnum.ToCharArray().Select(c=>c-48).ToArray();
        Vector3 shakeDirection = new Vector3(shakeBits[1], 0, shakeBits[0]).normalized * shakeStrength;
        transform.DOShakePosition(shakeDuration, shakeDirection, shakeVibrato, shakeRandomness, shakeSnapping, shakeFadeOut).OnComplete(delegate{isChanging = false;});
    }

    public void deformMethod(float input)
    {
        deformController.leftTopFront = new Vector3(-1,1,1) + new Vector3(0, input, 0) * direction[0];
        deformController.leftTopBack = new Vector3(-1,1,-1) + new Vector3(0, input, 0) * direction[1];
        deformController.rightTopFront = new Vector3(1,1,1) + new Vector3(0, input, 0) * direction[2];
        deformController.rightTopBack = new Vector3(1,1,-1) + new Vector3(0, input, 0) * direction[3];
        deformController.btnDeform();
        currentDeformValue = input;
    }
}

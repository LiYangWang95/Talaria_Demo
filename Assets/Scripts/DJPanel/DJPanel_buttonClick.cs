using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DJPanel_buttonClick : MonoBehaviour{
    #region UI SETTING
    [Header("UI SETTING")]
    public Color imageFadeColor;
    private Color imageOriColor;
    public float fadeDuration;
    #endregion

    #region INTERNAL CONTROL
    [HideInInspector] public bool state;
    private Image image;
    private bool fadingOut;
    private bool fadingIn;
    #endregion

    private void Start() {
        fadingIn = false;
        fadingOut = true;
        image = GetComponent<Image>();
        imageOriColor = new Color(255f/255f, 255/255f, 255/255f, 200/255f);
        imageFadeColor = new Color(255/255f, 192/255f, 0f/255f, 200/255f);
        image.color = imageOriColor;
        /* To avoid 0 fading time causes error */
        fadeDuration = fadeDuration == 0? Time.deltaTime: fadeDuration;
    }
    private void Update() {
        if(state && !fadingIn){    
            if(fadingOut){
                StopAllCoroutines();
                fadingOut = false;
            }
            fadingIn = true;
            StartCoroutine(fade(true, 0));
        }
        if(!state && !fadingOut){
            if(fadingIn){
                StopAllCoroutines();
                fadingIn = false;
            }
            fadingOut = true;
            StartCoroutine(fade(false, 0));
        }
    }

    /* Color fade function */
    private IEnumerator fade(bool fadeIn, int mode){
        Color oriColor;
        Color newColor;
        float timer = 0;
        oriColor = image.color;
        newColor = image.color;
        newColor = fadeIn? imageFadeColor: imageOriColor;
        while(timer < fadeDuration){
            timer += Time.deltaTime;
            image.color = Color.Lerp(oriColor, newColor, timer/fadeDuration);        
            yield return null;
        } 
    }
}

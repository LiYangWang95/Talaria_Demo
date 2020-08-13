using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake_shoesControl : MonoBehaviour
{
    public EarthquakeController earthquake;
    public simpleControl_command control;

    /* Frequency Setting*/
    [Header("Frequency Setting")]
    public float lowStrengthCoolTime = 3f;
    public float highStrengthCoolTime = 1.5f;

    private bool earthquaking;
    private bool switchPattern;
    private int[] allPattern;
    private int patternNo;
    private bool patternRunning;
    private int patternIndex = 0;
    void Start()
    {
        earthquaking = false;
        patternRunning = false;
        switchPattern = true;
        allPattern = new int[8];
        for(int i = 0; i < 8; i++){
            allPattern[i] = i;
        }
        patternIndex = 8;
        patternNo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(earthquake.isRunning){
            earthquaking = true;
            earthquaker();
        }
        else{
            if(earthquaking == true){
                for(int i = 0; i < 4; i++){
                    control.rightMuscle[i] = false;
                    control.leftMuscle[i] = false;
                }
                earthquaking = false;
            }
        }
    }

    private void earthquaker(){
        if(switchPattern){
            switch(earthquake.currentStage){
                case 0:
                    control.leftPressure = control.rightPressure = 20;
                    if(patternNo == 3){
                        patternNo = 2;
                    }
                    else{
                        patternNo = 3;
                    }
                    patternIndex = 1;
                    break;
                case 1:
                    // if(patternNo == 8 || patternNo == 9 || patternNo == 1){
                    //     patternNo = 0;
                    // }
                    // else{
                    //     patternNo = 1;
                    // }
                    control.leftPressure = control.rightPressure = 0;
                    patternNo = 9;
                    // patternIndex = 8;
                    patternIndex = 4;
                    break;
                default:
                    control.leftPressure = control.rightPressure = 40;
                    // if(patternIndex == 8){
                    if(patternIndex == 4){
                        patternRandom();
                        patternIndex = 0;
                    }
                    patternNo = allPattern[patternIndex];
                    break;
            }
            print(patternNo);
            switch(patternNo){
                /* Left */
                case 0:
                    control.rightMuscle[0] = false;
                    control.rightMuscle[1] = false;
                    control.rightMuscle[2] = true;
                    control.rightMuscle[3] = true;

                    control.leftMuscle[0] = false;
                    control.leftMuscle[1] = false;
                    control.leftMuscle[2] = true;
                    control.leftMuscle[3] = true;
                    break;

                /* Right */
                case 1:
                    control.rightMuscle[0] = true;
                    control.rightMuscle[1] = true;
                    control.rightMuscle[2] = false;
                    control.rightMuscle[3] = false;

                    control.leftMuscle[0] = true;
                    control.leftMuscle[1] = true;
                    control.leftMuscle[2] = false;
                    control.leftMuscle[3] = false;
                    break;
                
                /* Front */
                case 2:
                    control.rightMuscle[0] = false;
                    control.rightMuscle[1] = true;
                    control.rightMuscle[2] = true;
                    control.rightMuscle[3] = false;

                    control.leftMuscle[0] = false;
                    control.leftMuscle[1] = true;
                    control.leftMuscle[2] = true;
                    control.leftMuscle[3] = false;
                    break;
                
                /* Back */
                case 3:
                    control.rightMuscle[0] = true;
                    control.rightMuscle[1] = false;
                    control.rightMuscle[2] = false;
                    control.rightMuscle[3] = true;

                    control.leftMuscle[0] = true;
                    control.leftMuscle[1] = false;
                    control.leftMuscle[2] = false;
                    control.leftMuscle[3] = true;
                    break;
                
                /* Front-Right */
                case 4:
                    control.rightMuscle[0] = false;
                    control.rightMuscle[1] = true;
                    control.rightMuscle[2] = false;
                    control.rightMuscle[3] = false;

                    control.leftMuscle[0] = false;
                    control.leftMuscle[1] = true;
                    control.leftMuscle[2] = false;
                    control.leftMuscle[3] = false;
                    break;
                
                /* Back-Right */
                case 5:
                    control.rightMuscle[0] = true;
                    control.rightMuscle[1] = false;
                    control.rightMuscle[2] = false;
                    control.rightMuscle[3] = false;

                    control.leftMuscle[0] = true;
                    control.leftMuscle[1] = false;
                    control.leftMuscle[2] = false;
                    control.leftMuscle[3] = false;
                    break;

                /* Front-Left */
                case 6:
                    control.rightMuscle[0] = false;
                    control.rightMuscle[1] = false;
                    control.rightMuscle[2] = true;
                    control.rightMuscle[3] = false;

                    control.leftMuscle[0] = false;
                    control.leftMuscle[1] = false;
                    control.leftMuscle[2] = true;
                    control.leftMuscle[3] = false;
                    break;
                
                /* Back-Left */
                case 7:
                    control.rightMuscle[0] = false;
                    control.rightMuscle[1] = false;
                    control.rightMuscle[2] = false;
                    control.rightMuscle[3] = true;

                    control.leftMuscle[0] = false;
                    control.leftMuscle[1] = false;
                    control.leftMuscle[2] = false;
                    control.leftMuscle[3] = true;
                    break;
                
                /* Lift */
                case 8:
                    control.rightMuscle[0] = true;
                    control.rightMuscle[1] = true;
                    control.rightMuscle[2] = true;
                    control.rightMuscle[3] = true;

                    control.leftMuscle[0] = true;
                    control.leftMuscle[1] = true;
                    control.leftMuscle[2] = true;
                    control.leftMuscle[3] = true;
                    break;
                
                /* Drop */
                case 9:
                    control.rightMuscle[0] = false;
                    control.rightMuscle[1] = false;
                    control.rightMuscle[2] = false;
                    control.rightMuscle[3] = false;

                    control.leftMuscle[0] = false;
                    control.leftMuscle[1] = false;
                    control.leftMuscle[2] = false;
                    control.leftMuscle[3] = false;
                    break;

                default:
                    break;
            }
            switchPattern = false;
            if(earthquake.currentStage > 1){
                patternIndex += 1;
            }
        }
        if(!patternRunning){
            float patternTime;
            switch(earthquake.currentStage){
                case 0:
                    // patternTime = Random.Range(lowStrengthCoolTime, 1.5f * lowStrengthCoolTime);
                    patternTime = Random.Range(lowStrengthCoolTime, lowStrengthCoolTime);
                    break;
                case 1:
                    // patternTime = Random.Range(lowStrengthCoolTime, 1.5f * lowStrengthCoolTime);
                    patternTime = Random.Range(lowStrengthCoolTime, lowStrengthCoolTime);
                    break;
                case 2:
                    // patternTime = Random.Range(highStrengthCoolTime, 1.2f * highStrengthCoolTime);
                    patternTime = Random.Range(highStrengthCoolTime, highStrengthCoolTime);
                    break;
                default:
                    patternTime = 0;
                    break;
            }
            StartCoroutine(patternLengthTimer(patternTime));
        }
    }

    // private void patternRandom(){
    //     for(int i = 0; i < 8; i++){
    //         int swapIndex = Random.Range(0, 8);
    //         int tmp = allPattern[i];
    //         allPattern[i] = allPattern[swapIndex];
    //         allPattern[swapIndex] = tmp;
    //     }
    // }

    private void patternRandom(){
        for(int i = 0; i < 4; i++){
            int swapIndex = Random.Range(0, 4);
            int tmp = allPattern[i];
            allPattern[i] = allPattern[swapIndex];
            allPattern[swapIndex] = tmp;
        }
    }

    IEnumerator patternLengthTimer(float time){
        patternRunning = true;
        float accuTime = 0;
        while(accuTime < 1){
            accuTime += (Time.deltaTime/time);
            yield return null;
        }
        patternRunning = false;
        switchPattern = true;
        yield return null;
    }
}
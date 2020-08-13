using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skiing_shoesControlv2 : MonoBehaviour
{
    [Header("Control Reference")]
    public simpleControl_command control;
    public skiing_routeControl player;
    private skiing_routeControl.muscleStates prevState;
    private float airDuration;
    private float initialPressure;

    private void Start() {
        initialPressure = control.rightPressure;
    }
    void Update()
    {
        shoeCommand();
    }
    private void shoeCommand(){
        switch(player.state){
            /* Hit mountain */
            case skiing_routeControl.muscleStates.moveForward:
                control.rightMuscle[0] = true;
                control.rightMuscle[1] = false;
                control.rightMuscle[2] = false;
                control.rightMuscle[3] = true;
                player.rightBotRight.state = true;
                player.rightTopRight.state = false;  
                player.rightTopLeft.state = false;                
                player.rightBotLeft.state = true;

                control.leftMuscle[0] = true;
                control.leftMuscle[1] = false;
                control.leftMuscle[2] = false;
                control.leftMuscle[3] = true;
                player.leftBotRight.state = true;                
                player.leftTopRight.state = false;                 
                player.leftTopLeft.state = false;                
                player.leftBotLeft.state = true;
                prevState = skiing_routeControl.muscleStates.moveForward;
                break;
            /* Turning big left */
            case skiing_routeControl.muscleStates.bigLeft:
                control.rightMuscle[0] = true;
                control.rightMuscle[1] = true;    
                control.rightMuscle[2] = false;
                control.rightMuscle[3] = false;
                player.rightBotRight.state = true;
                player.rightTopRight.state = true;  
                player.rightTopLeft.state = false;                
                player.rightBotLeft.state = false;

                control.leftMuscle[0] = true;
                control.leftMuscle[1] = true;   
                control.leftMuscle[2] = false;
                control.leftMuscle[3] = false;
                player.leftBotRight.state = true;                
                player.leftTopRight.state = true;                 
                player.leftTopLeft.state = false;                
                player.leftBotLeft.state = false;
                prevState = skiing_routeControl.muscleStates.bigLeft;
                break;
            /* Turning small left */
            case skiing_routeControl.muscleStates.smallLeft:
                control.rightMuscle[0] = false;
                control.rightMuscle[1] = true;    
                control.rightMuscle[2] = false;
                control.rightMuscle[3] = false;
                player.rightBotRight.state = false;
                player.rightTopRight.state = true;  
                player.rightTopLeft.state = false;                
                player.rightBotLeft.state = false;

                control.leftMuscle[0] = false;
                control.leftMuscle[1] = true;   
                control.leftMuscle[2] = false;
                control.leftMuscle[3] = false;
                player.leftBotRight.state = false;                
                player.leftTopRight.state = true;                 
                player.leftTopLeft.state = false;                
                player.leftBotLeft.state = false;
                prevState = skiing_routeControl.muscleStates.smallLeft;
                break;
            /* Turning bigright */
            case skiing_routeControl.muscleStates.bigRight:
                control.rightMuscle[0] = false;
                control.rightMuscle[1] = false;    
                control.rightMuscle[2] = true;
                control.rightMuscle[3] = true;
                player.rightBotRight.state = false;
                player.rightTopRight.state = false;  
                player.rightTopLeft.state = true;                
                player.rightBotLeft.state = true;

                control.leftMuscle[0] = false;
                control.leftMuscle[1] = false;     
                control.leftMuscle[2] = true;
                control.leftMuscle[3] = true;
                player.leftBotRight.state = false;                
                player.leftTopRight.state = false;                 
                player.leftTopLeft.state = true;                
                player.leftBotLeft.state = true;
                prevState = skiing_routeControl.muscleStates.bigRight;
                break;
            /* Turning bigright */
            case skiing_routeControl.muscleStates.smallRight:
                control.rightMuscle[0] = false;
                control.rightMuscle[1] = false;    
                control.rightMuscle[2] = true;
                control.rightMuscle[3] = false;
                player.rightBotRight.state = false;
                player.rightTopRight.state = false;  
                player.rightTopLeft.state = true;                
                player.rightBotLeft.state = false;

                control.leftMuscle[0] = false;
                control.leftMuscle[1] = false;     
                control.leftMuscle[2] = true;
                control.leftMuscle[3] = false;
                player.leftBotRight.state = false;                
                player.leftTopRight.state = false;                 
                player.leftTopLeft.state = true;                
                player.leftBotLeft.state = false;
                prevState = skiing_routeControl.muscleStates.smallRight;
                break;
            /* Flying in air */
            case skiing_routeControl.muscleStates.airTime:
                control.rightMuscle[0] = false;
                control.rightMuscle[1] = false;    
                control.rightMuscle[2] = false;
                control.rightMuscle[3] = false;
                player.rightBotRight.state = false;
                player.rightTopRight.state = false;  
                player.rightTopLeft.state = false;                
                player.rightBotLeft.state = false;

                control.leftMuscle[0] = false;
                control.leftMuscle[1] = false;     
                control.leftMuscle[2] = false;
                control.leftMuscle[3] = false;
                player.leftBotRight.state = false;                
                player.leftTopRight.state = false;                 
                player.leftTopLeft.state = false;                
                player.leftBotLeft.state = false;
                // control.rightMuscle[0] = true;
                // control.rightMuscle[1] = true;    
                // control.rightMuscle[2] = true;
                // control.rightMuscle[3] = true;
                // player.rightBotRight.state = true;
                // player.rightTopRight.state = true;  
                // player.rightTopLeft.state = true;                
                // player.rightBotLeft.state = true;

                // control.leftMuscle[0] = true;
                // control.leftMuscle[1] = true;     
                // control.leftMuscle[2] = true;
                // control.leftMuscle[3] = true;
                // player.leftBotRight.state = true;                
                // player.leftTopRight.state = true;                 
                // player.leftTopLeft.state = true;                
                // player.leftBotLeft.state = true;
                // if(prevState != skiing_routeControl.muscleStates.airTime){
                //     airDuration = player.airTime;
                //     StartCoroutine(decreasePressure());
                // }
                prevState = skiing_routeControl.muscleStates.airTime;
                break;
            /* Prelanding */
            case skiing_routeControl.muscleStates.preLanding:
                // if(prevState == skiing_routeControl.muscleStates.airTime){
                //     StopAllCoroutines();
                //     control.rightPressure = control.leftPressure = initialPressure;
                // }
                control.rightMuscle[0] = true;
                control.rightMuscle[1] = true;    
                control.rightMuscle[2] = true;
                control.rightMuscle[3] = true;
                player.rightBotRight.state = true;
                player.rightTopRight.state = true;  
                player.rightTopLeft.state = true;                
                player.rightBotLeft.state = true;

                control.leftMuscle[0] = true;
                control.leftMuscle[1] = true;    
                control.leftMuscle[2] = true;
                control.leftMuscle[3] = true;
                player.leftBotRight.state = true;                
                player.leftTopRight.state = true;                 
                player.leftTopLeft.state = true;                
                player.leftBotLeft.state = true;
                prevState = skiing_routeControl.muscleStates.preLanding;
                break;
            
            /* Landing */
            case skiing_routeControl.muscleStates.landing:
                control.rightMuscle[0] = false;
                control.rightMuscle[1] = false;    
                control.rightMuscle[2] = false;
                control.rightMuscle[3] = false;
                player.rightBotRight.state = false;
                player.rightTopRight.state = false;  
                player.rightTopLeft.state = false;                
                player.rightBotLeft.state = false;

                control.leftMuscle[0] = false;
                control.leftMuscle[1] = false;    
                control.leftMuscle[2] = false;
                control.leftMuscle[3] = false;
                player.leftBotRight.state = false;                
                player.leftTopRight.state = false;                 
                player.leftTopLeft.state = false;                
                player.leftBotLeft.state = false;
                prevState = skiing_routeControl.muscleStates.landing;
                break;

            /* Climbing ramp */
            case skiing_routeControl.muscleStates.climbRamp:
                control.rightMuscle[0] = false;
                control.rightMuscle[1] = true;    
                control.rightMuscle[2] = true;
                control.rightMuscle[3] = false;
                player.rightBotRight.state = false;
                player.rightTopRight.state = true;  
                player.rightTopLeft.state = true;                
                player.rightBotLeft.state = false;

                control.leftMuscle[0] = false;
                control.leftMuscle[1] = true;    
                control.leftMuscle[2] = true;
                control.leftMuscle[3] = false;
                player.leftBotRight.state = false;                
                player.leftTopRight.state = true;                 
                player.leftTopLeft.state = true;                
                player.leftBotLeft.state = false;
                prevState = skiing_routeControl.muscleStates.climbRamp;
                break;
            /* Start */
            case skiing_routeControl.muscleStates.start:
                control.rightMuscle[0] = true;
                control.rightMuscle[1] = false;    
                control.rightMuscle[2] = false;
                control.rightMuscle[3] = true;
                player.rightBotRight.state = true;
                player.rightTopRight.state = false;  
                player.rightTopLeft.state = false;                
                player.rightBotLeft.state = true;

                control.leftMuscle[0] = true;
                control.leftMuscle[1] = false;    
                control.leftMuscle[2] = false;
                control.leftMuscle[3] = true;
                player.leftBotRight.state = true;                
                player.leftTopRight.state = false;                 
                player.leftTopLeft.state = false;                
                player.leftBotLeft.state = true;
                prevState = skiing_routeControl.muscleStates.start;
                break;
            /* Flat */
            case skiing_routeControl.muscleStates.flat:
                control.rightMuscle[0] = false;
                control.rightMuscle[1] = false;    
                control.rightMuscle[2] = false;
                control.rightMuscle[3] = false;
                player.rightBotRight.state = false;
                player.rightTopRight.state = false;  
                player.rightTopLeft.state = false;                
                player.rightBotLeft.state = false;

                control.leftMuscle[0] = false;
                control.leftMuscle[1] = false;    
                control.leftMuscle[2] = false;
                control.leftMuscle[3] = false;
                player.leftBotRight.state = false;                
                player.leftTopRight.state = false;                 
                player.leftTopLeft.state = false;                
                player.leftBotLeft.state = false;
                prevState = skiing_routeControl.muscleStates.flat;
                break; 
            default:
                control.rightMuscle[0] = false;
                control.rightMuscle[1] = false;    
                control.rightMuscle[2] = false;
                control.rightMuscle[3] = false;
                player.rightBotRight.state = false;
                player.rightTopRight.state = false;  
                player.rightTopLeft.state = false;                
                player.rightBotLeft.state = false;

                control.leftMuscle[0] = false;
                control.leftMuscle[1] = false;    
                control.leftMuscle[2] = false;
                control.leftMuscle[3] = false;
                player.leftBotRight.state = false;                
                player.leftTopRight.state = false;                 
                player.leftTopLeft.state = false;                
                player.leftBotLeft.state = false;
                break;     
        }
    }

    IEnumerator decreasePressure(){
        float curTime = 0;
        while(curTime < 1){
            curTime += Time.deltaTime/airDuration;
            control.rightPressure = control.leftPressure = Mathf.Lerp(initialPressure, 0, curTime);
            yield return null;
        }
        yield return null;
    }
}

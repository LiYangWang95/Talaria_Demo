using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleControl_keyBoard : MonoBehaviour
{
    public simpleControl_command command;
    private float accumulateTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if(accumulateTime < 1f){
            accumulateTime += Time.deltaTime;
        }
        else{
            for(int i = 0; i < 4; i++){
                command.rightMuscle[i] = !command.rightMuscle[i]; 
                command.leftMuscle[i] = !command.leftMuscle[i];
            }
            accumulateTime = 0;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            command.rightMuscle[0] = !command.rightMuscle[0];
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            command.rightMuscle[1] = !command.rightMuscle[1];
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            command.rightMuscle[2] = !command.rightMuscle[2];
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            command.rightMuscle[3] = !command.rightMuscle[3];
        }

        if(Input.GetKeyDown(KeyCode.Alpha7)){
            command.leftMuscle[0] = !command.leftMuscle[0];
        }
        if(Input.GetKeyDown(KeyCode.Alpha8)){
            command.leftMuscle[1] = !command.leftMuscle[1];
        }
        if(Input.GetKeyDown(KeyCode.Alpha9)){
            command.leftMuscle[2] = !command.leftMuscle[2];
        }
        if(Input.GetKeyDown(KeyCode.Alpha0)){
            command.leftMuscle[3] = !command.leftMuscle[3];
        }

        /*video input*/

        if(Input.GetKeyDown(KeyCode.I)){
            command.leftMuscle[0] = !command.leftMuscle[0];
            command.leftMuscle[3] = !command.leftMuscle[3];
            command.rightMuscle[0] = !command.rightMuscle[0];
            command.rightMuscle[3] = !command.rightMuscle[3];
        }
        if(Input.GetKeyDown(KeyCode.J)){
            command.leftMuscle[0] = !command.leftMuscle[0];
            command.leftMuscle[1] = !command.leftMuscle[1];
            command.rightMuscle[0] = !command.rightMuscle[0];
            command.rightMuscle[1] = !command.rightMuscle[1];
        }
        if(Input.GetKeyDown(KeyCode.S)){
            command.leftMuscle[0] = !command.leftMuscle[0];
            command.leftMuscle[1] = !command.leftMuscle[1];
            command.leftMuscle[2] = !command.leftMuscle[2];
            command.leftMuscle[3] = !command.leftMuscle[3];
            command.rightMuscle[0] = !command.rightMuscle[0];
            command.rightMuscle[1] = !command.rightMuscle[1];
            command.rightMuscle[2] = !command.rightMuscle[2];
            command.rightMuscle[3] = !command.rightMuscle[3];
        }
    }
}

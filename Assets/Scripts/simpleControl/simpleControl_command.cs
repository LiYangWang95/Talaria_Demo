using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleControl_command : MonoBehaviour
{
    [HideInInspector] public int rightCommand = 0;
    [Header("Command")]
    public bool[] rightMuscle;
    private int internalRightCommand = 0;
    [HideInInspector] public int leftCommand = 0;
    public bool[] leftMuscle;
    private int internalLeftCommand = 0;

    [Header("System Setting")]
    public float coolDownTime = 5f;
    public float rightPressure = 40f;
    public float leftPressure = 40f;


    private void Start() {
        rightMuscle = new bool[4];
        leftMuscle = new bool[4];
        for(int i = 0; i < 4; i++){
            rightMuscle[i] = false;
            leftMuscle[i] = false;
        }
    }

    void Update()
    {   
        commandFormation();
    }

    void commandFormation(){
        for(int i = 3; i >= 0; i--){
            if(i == 3){
                internalRightCommand = 0;
                internalLeftCommand = 0;
            }
            internalRightCommand <<= 1; 
            internalRightCommand += rightMuscle[i]? 1: 0;
            internalLeftCommand <<= 1; 
            internalLeftCommand += leftMuscle[i]? 1: 0;
        }

        rightCommand = ++internalRightCommand;
        leftCommand = ++internalLeftCommand;
    }
}

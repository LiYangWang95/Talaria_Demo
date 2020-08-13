using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class simpleControl_systemCore : MonoBehaviour
{
    [Header("Communication")]
    /* USB Port Communication */
    public dataTransmission usbTransmitter;

    /* Control */
    [Header("Talaria System Control")]
    private float coolDownTime = 10f;
    private float rightAccumulatedCoolDown = 0;
    private float leftAccumulatedCoolDown = 0;
    private float accumulatedTransmissionDelay = 0;
    private bool rightOn = false;
    private bool leftOn = false;
    private string command;
    private string prevCommand;

    [Header("External references")]
    public simpleControl_command system;

    // Start is called before the first frame update
    void Start(){
    }
    void Update(){
        /* Retrieve Setting */
        coolDownTime = system.coolDownTime;

        /* Set system status */
        systemStatus();
        
        /* Ensure enough transmission time */
        if(accumulatedTransmissionDelay >= usbTransmitter.sendTime){
            controlCommand(system);

            /* Transmission to hardware */
            if(command != prevCommand){
                Debug.Log(command);
                usbTransmitter.outputMessage = command;
                prevCommand = command;
            }
            accumulatedTransmissionDelay = 0;
        }
        else{
            accumulatedTransmissionDelay += Time.deltaTime;
        }
    }
    void OnApplicationQuit(){
        if(usbTransmitter.portIOStream.IsOpen){
            usbTransmitter.closeAll("0 0 0 0");
            usbTransmitter.portIOStream.BaseStream.Flush();
            usbTransmitter.portIOStream.Close();
        }
    }

    private void systemStatus(){
        /* ---------------------- Right Foot ---------------------- */
        /* System start up */
        if(system.rightCommand > 1 && !rightOn){
            rightOn = true;
            rightAccumulatedCoolDown = 0;
        }
        /* Goes to sleep when no command*/
        if(system.rightCommand == 1){
            rightAccumulatedCoolDown += Time.deltaTime;
        }
        if(rightAccumulatedCoolDown >= coolDownTime){
            rightOn = false;
            rightAccumulatedCoolDown = 0;
        }

        /* ---------------------- Left Foot ---------------------- */
        if(system.leftCommand > 1 && !leftOn){
            leftOn = true;
            leftAccumulatedCoolDown = 0;
        }
        /* Goes to sleep when no command*/
        if(system.leftCommand == 1){
            leftAccumulatedCoolDown += Time.deltaTime;
        }
        if(leftAccumulatedCoolDown >= coolDownTime){
            leftOn = false;
            leftAccumulatedCoolDown = 0;
        }
    }

    /* Control command formation */
    private void controlCommand(simpleControl_command system){
        string internalCommand = "";
        /* Form left command */
        if(leftOn){
            internalCommand += system.leftPressure.ToString() + " " + system.leftCommand.ToString() + " ";
        }
        else{
            internalCommand += "0 0 ";
        }
        /* Form right command */
        if(rightOn){
            internalCommand += system.rightPressure.ToString() + " " + system.rightCommand.ToString();
        }
        else{
            internalCommand += "0 0";
        }
        command = internalCommand;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.IO.Ports;

public class dataTransmission : MonoBehaviour
{
    /* Communication APIs */
    #region APIs
    /* Allows reading the input message externally */
    public string inputMessage{
        get{
            return _inputMessage;
        }
    }
    /* Allows sending the output message externally */
    public string outputMessage{
        set{
            _outputMessage = value;
        }
    }
    /* Allow access of I/O stream */
    public SerialPort portIOStream{
        get{
            return _portIOStream;
        }
        set{
            _portIOStream = value;
        }
    }
    /* Sending function */
    IEnumerator sendToPort(){
        while(true){
            if(_outputMessage != ""){
                if(_portIOStream.IsOpen && (_outputMessage != "")){
                    try{
                        // Debug.Log("[Talaria][Port] Sending command [" + _outputMessage + "] ...");
                        _portIOStream.WriteLine(_outputMessage);
                        _portIOStream.BaseStream.Flush();
                        _outputMessage = outputMessage = "";
                        // Debug.Log("[Talaria][Port] Sending completed.");
                    }
                    catch (IOException e){
                        Debug.Log("[Talaria][Port] Sending failed. Error: " + e.Message);
                    }
                }
            }
            yield return new WaitForSeconds(sendTime);
        }
    }
    /* Reading function */

    IEnumerator readFromPort(){
        while(true){
            if(readPort){
                if(_portIOStream != null && _portIOStream.IsOpen){
                    try{
                        // Debug.Log("[Talaria][Port] Reading...");
                        _inputMessage = _portIOStream.ReadLine();
                        Debug.Log("[Talaria][Port] Reading complete. Message: " + _inputMessage);
                        _inputMessage = "";
                    }
                    catch(System.TimeoutException e){
                        Debug.Log("[Talaria][Port] Reading time out. Error message: " + e.Message);
                    }
                    catch(IOException e){
                        Debug.Log(e.Message);
                    }
                    /* Set back */
                    readPort = false;
                }
            }
            yield return new WaitForSeconds(readTime);            
        }
    }
    #endregion
    public string port;
    public bool isMac = false;
    public bool readPort = false;
    public float readTime = 0.2f;
    public float sendTime = 0.2f;

    private SerialPort _portIOStream;
    private string _inputMessage = "";
    private string _outputMessage = "";

    /* Build up connection */
    void Awake(){
        try{
            _portIOStream = isMac? new SerialPort("/dev/tty." + port, 57600): 
                new SerialPort("\\\\.\\" + port, 57600);
            /* DO NOT MODIFY READ TIME OUT */
            _portIOStream.ReadTimeout = 8;
            _portIOStream.WriteTimeout = 100;
            /* Define the character that indicates a new line when reading input/writing output */
            _portIOStream.NewLine = "\n";
            _portIOStream.Open();
        }
        /* If current port doesn't work, indicate which ports are available */
        catch(IOException e){
            Debug.Log(e.Message);
            string[] allActivePorts = SerialPort.GetPortNames();
            string listAllActivePorts = "";
            for(int portNum = 0; portNum < allActivePorts.Length; portNum++){
                listAllActivePorts += allActivePorts[portNum] + ", ";
            }
            Debug.Log("[Talaria][Port connection] Fail to build connection. Currently the active ports are " + listAllActivePorts);
        /* Disable connection trial */
            this.gameObject.SetActive(false);
        }
    }

    void Start(){
        /* Set up coroutines */
        StartCoroutine(sendToPort());
        StartCoroutine(readFromPort());
    }
    void OnApplicationQuit(){
        StopAllCoroutines();
    }

    /* Function for closing all relays */
    public void closeAll(string command){
        if(_portIOStream.IsOpen && (command != "")){
            try{
                _portIOStream.WriteLine(command);
                _portIOStream.BaseStream.Flush();
                command = "";
                // Debug.Log("[Talaria][Port] Sending completed.");
            }
            catch (IOException e){
                Debug.Log("[Talaria][Port] Sending failed. Error: " + e.Message);
            }
        }
    }
}

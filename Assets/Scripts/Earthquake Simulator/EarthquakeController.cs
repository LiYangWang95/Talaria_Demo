using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeController : MonoBehaviour
{
    [System.Serializable]
    public class EarthquakeStage{
        public string name;
        [UnityEngine.SerializeField, Range(1f, 50f)]
        public float magtitude = 1.0f;
        [UnityEngine.SerializeField, Range(0f, 100f)]
        public float shakingSpeed;
        [UnityEngine.SerializeField, Range(0f, 100f)]
        public float duration;
        public Vector3 force;
	    public AnimationCurve forceOverTime;
    }
    public EarthQuake earthQuake;

    public List<EarthquakeStage> stages;
    [UnityEngine.SerializeField]
    public bool isRunning = false;
    [UnityEngine.SerializeField]
    public int currentStage = 0;
    [Header("Stage Delay")]
    public float stageDelay = 3f;
    
    void Update()
    {
        if(isRunning){
            if(!earthQuake.Running){
                if(changeStage()){
                    startShake();
                }
                else{
                    isRunning = false;
                }
            }
        }
    }
    public void startEarthquake(){
        isRunning = true;
        currentStage = -1;
    }

    bool changeStage(){
        currentStage++;
        if(currentStage == stages.Count) return false;
        return true;
    }

    void startShake(){
        EarthquakeStage stage = stages[currentStage];
        earthQuake.magnitude = stage.magtitude;
        earthQuake.shakingSpeed = stage.shakingSpeed;
        earthQuake.duration = stage.duration;
        earthQuake.forceByAxis = stage.force;
        earthQuake.forceOverTime = stage.forceOverTime;
        earthQuake.Running = true;
    }
}

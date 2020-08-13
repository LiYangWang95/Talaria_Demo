using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class skiing_routeControl : MonoBehaviour
{
    [Header("Particle")]
    public ParticleSystem leftParticle;
    public ParticleSystem rightParticle;
    public ParticleSystem frontParticle;
    public ParticleSystem backParticle;

    [Header("UI")]
    public DJPanel_buttonClick rightTopLeft;
    public DJPanel_buttonClick rightTopRight;
    public DJPanel_buttonClick rightBotLeft;
    public DJPanel_buttonClick rightBotRight;
    public DJPanel_buttonClick leftTopLeft;
    public DJPanel_buttonClick leftTopRight;
    public DJPanel_buttonClick leftBotLeft;
    public DJPanel_buttonClick leftBotRight;
    public GameObject startPanel;
    public Text startText;
    public float startDelay = 5f;
    private float countDown;
    private bool paused;
    /* Path */
    public LineRenderer pathLine;
    public int routeSphereNumber = 86;
    public int lineSphereNumber = 76;

    /* DoTWEEN */
    public skiing_buildRoute route;
    public float timeBetweenSpheres = 1f;
    public float airTime = 2f;
    private int routeIndex;
    private List<Transform> routeSpheres;
    private Quaternion lastRotation;

    /* Positioning */
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    /* Muscle states */
    public enum muscleStates{
        moveForward,
        bigRight,
        smallRight,
        bigLeft,
        smallLeft,
        landing,
        airTime,
        climbRamp,
        preLanding,
        start,
        flat
    }
    [HideInInspector] public muscleStates state;

    void Start()
    {
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;

        routeSpheres = route.allPoints;

        pathLine.positionCount = lineSphereNumber;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 0.6f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 0.6f) }
        );
        pathLine.colorGradient = gradient;
        pathLine.widthMultiplier = 2f;

        /* Path drawing */
        var points = new Vector3[lineSphereNumber];
        for(int i = 0; i < lineSphereNumber; i++){
            RaycastHit hit;
            if(Physics.Raycast(routeSpheres[i].transform.position + new Vector3(0, 5, 0), Vector3.down, out hit, Mathf.Infinity, 1<<10)){
                points[i] = hit.point;
            }
        }
        pathLine.SetPositions(points);

        
        if(frontParticle.isPlaying){frontParticle.Stop();}
        if(!backParticle.isPlaying){backParticle.Play();}
        if(leftParticle.isPlaying){leftParticle.Stop();}
        if(rightParticle.isPlaying){rightParticle.Stop();}
        
        state = muscleStates.landing;
        paused = false;
        startText.text = "GET READY!";
        countDown = startDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            DOTween.KillAll();
            this.transform.position = initialPosition;
            this.transform.rotation = initialRotation;
            state = muscleStates.landing;
            paused = false;
            startPanel.SetActive(true);
            startText.text = "GET READY!";
            countDown = startDelay;
            if(frontParticle.isPlaying){frontParticle.Stop();}
            if(!backParticle.isPlaying){backParticle.Play();}
            if(leftParticle.isPlaying){leftParticle.Stop();}
            if(rightParticle.isPlaying){rightParticle.Stop();}
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            if(!paused){
                DOTween.PauseAll();
            }
            else{
                DOTween.PlayAll();
            }
            paused = !paused;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            StartCoroutine(startCountDown());
            state = muscleStates.start;
        }
    }
    void moveRoute(){
        Vector3[] targets = new Vector3[routeSpheres.Count]; 
        for(int i = 0; i< routeSpheres.Count;i++){
            targets[i] = routeSpheres[i].position;
        }
        routeIndex = 0;
        transform.DOPath(targets,timeBetweenSpheres * routeSpheres.Count, PathType.CatmullRom, PathMode.Ignore, 10, Color.yellow).OnWaypointChange(p => uponSphereChange());
    }

    void uponSphereChange(){
        if(routeSpheres[routeIndex].tag != "air" && routeSpheres[routeIndex].tag != "preLanding"){
            this.transform.DOLookAt(routeSpheres[routeIndex + 1].transform.position, 1f, AxisConstraint.None);
            lastRotation = this.transform.rotation;
        }
        switch(routeSpheres[routeIndex].tag){
            case "mountain":
                state = muscleStates.moveForward;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            case "smallLeft":
                state = muscleStates.smallLeft;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(!rightParticle.isPlaying){rightParticle.Play();}
                break;
            case "smallRight":
                state = muscleStates.smallRight;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(!leftParticle.isPlaying){leftParticle.Play();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            case "bigRight":
                state = muscleStates.bigRight;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(!leftParticle.isPlaying){leftParticle.Play();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            case "bigLeft":
                state = muscleStates.bigLeft;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(!rightParticle.isPlaying){rightParticle.Play();}
                break;
            case "ramp":
                state = muscleStates.climbRamp;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            case "air":
                state = muscleStates.airTime;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            case "landingPoint":
                state = muscleStates.landing;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            case "preLanding":
                state = muscleStates.preLanding;
                if(!frontParticle.isPlaying){frontParticle.Play();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            case "flatGround":
                state = muscleStates.flat;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            case "slowDown":
                state = muscleStates.climbRamp;
                if(!frontParticle.isPlaying){frontParticle.Play();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            case "endPoint":
                state = muscleStates.flat;
                DOTween.KillAll();
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
            default:
                state = muscleStates.flat;
                if(frontParticle.isPlaying){frontParticle.Stop();}
                if(leftParticle.isPlaying){leftParticle.Stop();}
                if(rightParticle.isPlaying){rightParticle.Stop();}
                break;
        }
        routeIndex ++;
    }

    IEnumerator startCountDown(){
        while(countDown > 0){
            countDown -= Time.deltaTime;
            startText.text = countDown.ToString("F0");
            yield return null;
        }
        startPanel.SetActive(false);
        state = muscleStates.moveForward;
        moveRoute();
        yield return null;
    }
}

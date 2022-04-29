using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    //distance at which the sheep will begin to run 

    public float RunDistanceThreshold;
    public float ChaseDistanceAfterCollideThreshold;
    public int maxSwitchTime;
    public Color GhostColor;
    public AudioClip sheepClip;
    public AudioClip ghostClip;
    private Color SheepColor;
    private Vector3 m_movement;
    private Vector3 attractForce;
    private Vector3 repForce;
    private AudioSource sheepSound;
    private AudioSource ghostSound;
    private bool hasCollided = false; 


    public void Start(){
        //invoke randomly the switching of states 
        Invoke("switchState", Random.Range(0, maxSwitchTime));

        //

        //SheepColor = agent.Color();
        //SheepColor = 
        //creating the sounds 




        sheepSound = gameObject.AddComponent<AudioSource>();
        sheepSound.clip = sheepClip;
        ghostSound = gameObject.AddComponent<AudioSource>();
        ghostSound.clip = ghostClip;
    }
    public override Steering GetSteering()
    {
        //scan the game environnement 
        scanEnnemies();
        //update the movement in function of the state
        if (tag == "sheep")
        {
            m_movement = repForce;
        }
        if (tag == "ghost")
        {
            m_movement = attractForce;
        }
        
        //move the cellulo
        Steering steering = new Steering();
        steering.linear = m_movement * agent.maxAccel;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
        return steering;
    }

    //scal all the ennemies, change the attractive and repulsive force 
    private void scanEnnemies()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("dog");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        repForce = Vector3.zero; 
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            repForce = repForce - diff / curDistance;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        //add a minimum distance 
        if(distance >= RunDistanceThreshold)
        {
            repForce = Vector3.zero;
        } else
        {
            repForce = repForce.normalized; 
        }

        if(hasCollided && distance < ChaseDistanceAfterCollideThreshold)
        {
            attractForce = Vector3.zero;
        } else
        {
            hasCollided = false;
            attractForce = closest.transform.position - position;

        }
    }

    //one chance over 2 to switch
    private void switchState()
    {
        if(tag == "sheep")
        {
           transform.gameObject.tag = "ghost";
            //change led make noise
            agent.SetVisualEffect(0, GhostColor, 0);

            //

            //Change The PLAYERS feeling (enable back drivability):

            // 1. Clear the previous sensibility
            // 2. set the new sensibility

            //

            ghostSound.Play();

        }
        else if(tag == "ghost")
        {
           transform.gameObject.tag = "sheep";
            //change led make noise
            agent.SetVisualEffect(0, SheepColor, 0);

            //

            //Change The PLAYERS feeling (move on stone feeling):

            // 1. Clear the previous sensibility
            // 2. set the new sensibility

            //

            sheepSound.Play();

        }
        Invoke("switchState", Random.Range(0, maxSwitchTime));
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "dog" && tag == "ghost")
        {
            other.gameObject.GetComponent<PointSystem>().DecreasePoints();
            hasCollided = true; 
        }
    }

}

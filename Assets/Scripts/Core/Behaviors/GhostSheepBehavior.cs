using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    //distance at which the sheep will begin to run 
    public float R;
    public float G;
    public float B;
    public float RunDistanceThreshold;
    public int maxSwitchTime; 
    private Vector3 m_movement;
    private Vector3 attractForce; 
    private Vector3 repForce; 

    public void Start(){
        //invoke randomly the switching of states 
        Invoke("switchState", Random.Range(0, maxSwitchTime));
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
        attractForce = closest.transform.position - position; 
        if(attractForce.sqrMagnitude > 1) attractForce = attractForce.normalized;
    }

    //one chance over 2 to switch
    private void switchState()
    {
        print("switch");
        if(tag == "sheep")
        {
           transform.gameObject.tag = "ghost";
           //change led make noise
        }
        else if(tag == "ghost")
        {
           transform.gameObject.tag = "sheep";
           //change led make noise

        }
        Invoke("switchState", Random.Range(0, maxSwitchTime));
    }
}

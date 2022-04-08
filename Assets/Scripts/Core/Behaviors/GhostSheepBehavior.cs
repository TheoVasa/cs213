using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    //distance at which the sheep will begin to run 
    public float RunThreshold;
    public float ChaseThreshold;
    public float switchTime; 
    private Vector3 m_movement;
    private Vector3 attractForce; 
    private Vector3 repForce; 

    public void Start(){
    }
    public override Steering GetSteering()
    {
        //invoke all switchTime a random switch of state
        Invoke("RandomSwitchState", switchTime);
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
            repForce += diff; 
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        repForce = -repForce.normalized;
        attractForce = closest.transform.position - position; 
    }

    //one chance over 2 to switch
    private void RandomSwitchState()
    {
        bool hasSwitched = Random.Range(-1.0f, 1.0f) > 0;
        if (hasSwitched)
        {
            if(tag == "sheep")
            {
                tag = "ghost";
                //change led make noise
            }
            if(tag == "ghost")
            {
                //change led make noise
                tag = "sheep";
            }
            
        }
    }


    



}

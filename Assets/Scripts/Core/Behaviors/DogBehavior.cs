using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputKeyboard
{
    arrows = 0,
    wasd = 1
}

public class DogBehavior : AgentBehaviour 
{
    //TODO ces attributs doivent pouvoir etre modifi�s par les utilisateurs depuis le menu 
    public Color DogColor;
    public int DogIndex;
    public InputKeyboard inputKeyboard;
    private bool WantToStart;
    private bool WasInSheepMode;
    private bool HasGem;


    void Start()
    {
        WantToStart = false; 
        WasInSheepMode = false;
        HasGem = false;

        //Set dog color depending on index (P1 or P2 ?)
        if (DogIndex == 1){
            DogColor = Settings.colorP1;
            inputKeyboard = Settings.inputKeyP1;
        } else {
            DogColor = Settings.colorP2;
            inputKeyboard = Settings.inputKeyP2;
        }

        //the color 
        agent.SetVisualEffect(0, DogColor, 0);
        //clear the haptic feedback 
        agent.ClearHapticFeedback();

    }

    public override Steering GetSteering()
    {
        if(PlayersWantToStart())
        {
            Timer.hasInitOnLongTouch = true;
        }

        //handling the haptic feedbacks (logic working fine)
        if (OnSheepMode() && !WasInSheepMode)
        {
            //feedback haptique quand sheep mode 
            agent.SetCasualBackdriveAssistEnabled(true);
            agent.ClearHapticFeedback();
            WasInSheepMode = true;
        } 
        else if (!OnSheepMode() && WasInSheepMode) 
        { 
            //feedback haptique quand ghost mode 
            agent.MoveOnStone();
            WasInSheepMode = false;
        }


        //handle the movement with the keyboard (working fine)
        float vertical;
        float horizontal;
        if (inputKeyboard == InputKeyboard.arrows)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            vertical = Input.GetAxis("VerButt");
            horizontal = Input.GetAxis("HorButt");
        }

        Steering steering = new Steering();
        //moving the cellulo
        if (!Timer.pauseGame){
            steering.linear = new Vector3(horizontal, 0, vertical) * agent.maxAccel;
            steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
        }
        return steering;
    }

    //use to communicate other player if the current player want to start the game 
    public bool WantToStartTheGame()
    {
        return WantToStart;
    }
    //make the player earn a gem 
    public void EarnGem()
    {
        HasGem = true;
    }
    
    public override void OnCelluloTouchReleased(int key)
    {
        WantToStart = false;

    }
    public override void OnCelluloLongTouch(int key)
    {
        WantToStart = true; 
    }

    void OnCollisionEnter(Collision collision)
    {
     if(HasGem && collision.gameObject.tag == "dog")
        {
            //steal the points
            collision.gameObject.GetComponent<PointSystem>().DecreasePoints(2);
            gameObject.GetComponent<PointSystem>().IncreasePoints(2);
            HasGem = false;
        }
    }

    //==============================================================//

    //check if we are in sheep mode (is working fine) 
    private bool OnSheepMode()
    {
        return GameObject.FindGameObjectsWithTag("sheep").Length != 0;

    }

    //not tested yet 
    private bool PlayersWantToStart()
    {
        //iterate one all GameObject with a dog tag and check if they all want to start the game
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("dog");
        bool str = WantToStartTheGame();
        foreach (GameObject go in gos)
        {
            str = str && go.GetComponent<DogBehavior>().WantToStartTheGame();
        }
        return str;
    }


}

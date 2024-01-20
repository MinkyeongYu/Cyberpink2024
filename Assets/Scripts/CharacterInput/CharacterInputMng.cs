using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class CharacterInputMng : MonoBehaviour
{
    public string axisX = "Vertical";
    public string axisY = "Horizontal";
    public string runButtonName = "Run";
    public string jumpTrigger = "Jump";
    public string PickupName = "Pickup";
    public string fireTriggerName = "Fire1";
    public string reloadButtonName = "Reload";
    public float move {get; private set;}
    public float rotate {get; private set;}
    public bool run {get; private set;}
    public bool jump {get; private set;}
    // pickup 구현해야함.
    public bool pickUp {get; private set;}
    public bool fire {get; private set;}
    public bool reload {get; private set;}
    private void Awake() 
    {
        CharacterInputMng[] obj = FindObjectsOfType<CharacterInputMng>();
        if(obj.Length == 1) {
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }
    }
    private void Update() 
    {
        if(GameManager.GetInstance() != null && (GameManager.GetInstance().currentGameState == GameState.ISGAMEOVER))
        {
            move = 0;
            rotate = 0;
            run = false;
            jump = false;
            pickUp = false;
            fire = false;
            reload = false;
        }

        move = Input.GetAxis(axisX);
        rotate = Input.GetAxis(axisY);
        run = Input.GetButton(runButtonName);
        jump = Input.GetButton(jumpTrigger);
        pickUp = Input.GetButton(PickupName);
        fire = Input.GetButton(fireTriggerName);
        reload = Input.GetButton(reloadButtonName);
    }
}

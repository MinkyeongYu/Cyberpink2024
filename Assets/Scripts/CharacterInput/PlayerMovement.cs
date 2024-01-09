using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float rotSpeed;
    public static PlayerMovement instance;
    public float moveSpeed;
    public float jumpForce;
    public float runSpeed;
    // bool isJumping = false;
    bool isGround = true;
    public PlayerState playerState;
    public CharacterInputMng characterInput;
    public Rigidbody characterRigidbody;
    public Animator characterAnimator;
    
    private void InitValuable(float mspeed, float rspeed, float jspeed, float RunSpeed) {
        moveSpeed = mspeed;
        rotSpeed = rspeed;
        jumpForce = jspeed;
        runSpeed = RunSpeed;
    }
    private void Awake() 
    {
        PlayerMovement[] obj = FindObjectsOfType<PlayerMovement>();
        if(obj.Length == 1) {
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }

        InitValuable(5, 180, 0.1f, 7);
    }
    private void Start()
    {
        playerState = PlayerState.IDLE;
        characterInput = GetComponent<CharacterInputMng>();
        characterRigidbody = GetComponent<Rigidbody>();
        characterAnimator = GetComponent<Animator>();
        characterAnimator.SetFloat("Move", 0);
    }
    private void FixedUpdate() 
    {
        Rotate();
        Move();
        Jump();
        // Debug.Log(characterInput.run);
        // Debug.Log(characterInput.move);
        Debug.Log("playerState: " + playerState);
    }
    private void Move()
    {
        if(characterInput.move == 0) {
            playerState = PlayerState.IDLE;
        }
        else {
            characterAnimator.SetBool("Idle", false);
            if(characterInput.run && characterInput.move > 0) {
                playerState = PlayerState.RUN;
            }
            else {
                playerState = PlayerState.WALK;
            }
        }
    }
    private void Rotate() 
    {
        float turn = characterInput.rotate * rotSpeed * Time.deltaTime;
        characterRigidbody.rotation = characterRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);
    }
    private void Jump()
    {
        if(characterInput.jump && isGround == true)
        {
            playerState = PlayerState.JUMP;
            // isJumping = true;
            isGround = false;
        }
    }
    private void OnCollisionStay(Collision other) {
        if(other.gameObject.tag == "Ground") {
            // isJumping = false;
            isGround = true;
        }
    }
    private void PickUpItem() {
        if(characterInput.pickUp)
        {
            playerState = PlayerState.PICKUP;
        }
    }
}

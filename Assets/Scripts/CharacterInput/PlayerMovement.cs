using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float rotSpeed;
    public GameObject rightHand;
    static PlayerMovement s_instance;
    public static PlayerMovement Instance { get { return s_instance; } }
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

        InitValuable(5, 180, 0.2f, 7);
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
    private void Rotate() // Slerp, Lerp도 사용가능하지만 그러려면 move시에도 LookRotation해줘야함.
    {
        float turn = characterInput.rotate * rotSpeed * Time.deltaTime;
        characterRigidbody.rotation = characterRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);
    }
    private void Jump()
    {
        if(characterInput.jump && isGround)
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
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemOverlap : MonoBehaviour
{
    [SerializeField] Collider[] hitCollider;
    public PlayerMovement pm;
    public CharacterInputMng playerInput;
    Rigidbody rb;
    Vector3 drawSize;
    string objName;
    private void Start() {
        drawSize =  new Vector3(2, 1, 2);
        rb = GetComponent<Rigidbody>();
    }

    public void Update() {
        hitCollider = Physics.OverlapBox(transform.position, drawSize);
        foreach(Collider s in hitCollider) {
            if(gameObject.transform.parent) break;
            if(s.gameObject.tag == "Player")
            {
                if(playerInput.pickUp) {
                    pm.playerState = PlayerState.PICKUP;
                    gameObject.transform.parent = s.gameObject.transform;
                    objName = s.gameObject.name;
                    rb.useGravity = false;
                    // 손안에서 아이템이 회전하는거 고정
                    rb.isKinematic = true;
                }
            }
        }
        if(gameObject.transform.parent) {
            gameObject.transform.position = pm.rightHand.transform.position;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.localScale = new Vector3(0.09f,0.09f,0.09f);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, drawSize * 2);
    }
}

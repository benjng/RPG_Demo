using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    
    //Animation States
    //const string PLAYER_JUMP = "Jump";
    const string PLAYER_IDLE = "isIdling";
    //const string PLAYER_GATHER = "Gather";
    const string PLAYER_ATTACK = "triggerAttack";
    const string PLAYER_RUN = "isSprinting";
    const string PLAYER_WALK = "isWalking";

    private float nextAtkTime = 0f;
    private float atkRate;
    private string currentState = PLAYER_IDLE;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        atkRate = GetComponent<PlayerCombat>().weaponData.attackRate;
    }

    // Change animations with transition
    public void ChangeAnimationState(string newState)
    {
        // STOP current state immediately
        if (currentState != newState)
        {
            animator.SetBool(currentState, false);
        }
        animator.SetBool(newState, true);
        currentState = newState;
    }

    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool shiftPressed = Input.GetKey("left shift");
        //bool spacePressed = Input.GetButtonDown("Jump");
        bool leftMouseClicked = Input.GetMouseButtonDown(0);

        if (!Input.anyKey) // No key pressed case
        {
            ChangeAnimationState(PLAYER_IDLE);
            return;
        }

        if (leftMouseClicked && Time.time > nextAtkTime) // ATTACK
        {
            animator.SetTrigger(PLAYER_ATTACK);
            nextAtkTime = Time.time + atkRate;
            return;
        }

        if (forwardPressed || backwardPressed || leftPressed || rightPressed) // WALK/RUN
        {
            if (shiftPressed)
            {
                ChangeAnimationState(PLAYER_RUN);
                return;
            }
            ChangeAnimationState(PLAYER_WALK);
            return;
        }

        //if (spacePressed) // JUMP
        //{
        //    //animator.SetBool(isJumpingHash, true);
        //    ChangeAnimationState(PLAYER_JUMP);
        //    return;
        //}

        // Any other unexpected key combinations
        if (currentState != PLAYER_IDLE)
            ChangeAnimationState(PLAYER_IDLE);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    
    //Animation States
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_GATHER = "Gather";
    const string PLAYER_ATTACK = "Attack";
    const string PLAYER_RUN = "Sprint";
    const string PLAYER_WALK = "Walk";

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
            animator.ResetTrigger(currentState); 
        }

        // No other change during transition
        if (animator.IsInTransition(0))
        {
            return;
        }

        animator.SetTrigger(newState);
        currentState = newState;
    }

    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool shiftPressed = Input.GetKey("left shift");
        bool spacePressed = Input.GetButtonDown("Jump");
        bool leftMouseClicked = Input.GetMouseButtonDown(0);

        if (!Input.anyKey) // No key pressed case
        {
            ChangeAnimationState(PLAYER_IDLE);
            return;
        }

        if (leftMouseClicked && Time.time > nextAtkTime) // ATTACK
        {
            ChangeAnimationState(PLAYER_ATTACK);
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
        ChangeAnimationState(PLAYER_IDLE);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform spriteTransform;
    public Rigidbody PlayerRigidbody;
    public Animator animator;
    private Vector2 curMovementInput;
    public int Speed = 7;
    public int JumpPower = 4;
    
    private bool IsGround = false;
    private void FixedUpdate() 
    {
        if (GameManager.Instance.Player.canMove)
        {
            Move();
        }
        else
        {
            curMovementInput = Vector2.zero;
            PlayerRigidbody.velocity = Vector3.zero; 
            animator.SetBool(KeyWordManager.str_WalkTxt, false); 
        }
    }

    private void Move()
    {
        Vector3 direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        direction *= Speed;
        direction.y = PlayerRigidbody.velocity.y;

        PlayerRigidbody.velocity = direction;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.Player.canMove) return;

        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            if(curMovementInput.x >= 0)
            {
                spriteTransform.rotation = KeyWordManager.Quat_Zeros; 
            }
            else
            {
                spriteTransform.rotation = KeyWordManager.Quat_flip;

            }
            animator.SetBool(KeyWordManager.str_WalkTxt,true);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            animator.SetBool(KeyWordManager.str_WalkTxt, false);

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGround)
        {
            PlayerRigidbody.AddForce(Vector2.up * JumpPower, ForceMode.Impulse);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == KeyWordManager.int_CanStandLayer)
        {
            IsGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == KeyWordManager.int_CanStandLayer)
        {
            IsGround = false;
        }
    }
}

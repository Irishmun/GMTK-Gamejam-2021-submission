using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementScript : MonoBehaviour
{
    [SerializeField] private float PlayerSpeed = 10;
    [SerializeField] private float JumpForce = 1, FallMultiplier = 2.5f, LowJumpMultiplier = 2f;
    [SerializeField] private Vector2 GroundCheckSize, GroundCheckOffset;
    [SerializeField] private LayerMask GroundCheckLayer;

    private Vector2 InputValue;
    private bool StartJump, StartFall;
    private bool Moving;
    public bool IsHopping { get; set; }
    public bool IsBall { get; set; }

    private Rigidbody2D Rb;
    private Animator animator;
    private SpriteRenderer CharacterSprite;
    private Quaternion startRotate;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        CharacterSprite = GetComponentInChildren<SpriteRenderer>();
        startRotate = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Moving = InputValue.x != 0;
        animator.SetBool("Moving", Moving);
        if (InputValue.x > 0)
        {
            CharacterSprite.flipX = false;
        }
        else if (InputValue.x < 0)
        {
            CharacterSprite.flipX = true;
        }

        if (IsBall)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Rb.constraints = RigidbodyConstraints2D.None;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
            Rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            transform.rotation = startRotate;
        }

        if (CanJump() && Rb.gravityScale > 1)
        {
            Rb.gravityScale = 1;
        }

    }
    private void FixedUpdate()
    {
        //if (Moving)
        //{
        if (!IsBall)
        {
            Rb.velocity = new Vector2(InputValue.x * PlayerSpeed * Time.fixedDeltaTime, Rb.velocity.y);
        }
        else
        {
            Rb.AddForce((Vector2.right * InputValue.x * PlayerSpeed * Time.fixedDeltaTime));
            GetComponent<PlayerAudioScript>().playRollSound();
        }
        //}

        if (Moving && IsHopping)
        {
            GetComponent<PlayerAudioScript>().playWalkSoundDelayed();
        }

        if (StartJump)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            StartJump = false;
        }

        if (Rb.velocity.y < 0)
        {
            Rb.gravityScale = FallMultiplier;
        }
        else if (StartFall && Rb.velocity.y > 0)
        {
            Rb.gravityScale = LowJumpMultiplier;
            StartFall = false;
        }

    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        InputValue.x = ctx.ReadValue<Vector2>().x;
        InputValue.y = ctx.ReadValue<Vector2>().y;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {

        if (ctx.started)
        {
            if (CanJump())
            {
                StartJump = true;
            }
        }

        if (ctx.canceled)
        {
            if (!CanJump())
            {
                StartFall = true;
            }
        }
    }

    private bool CanJump()
    {
        return Physics2D.OverlapBox((Vector2)transform.position + GroundCheckOffset, GroundCheckSize, 0, GroundCheckLayer) && !IsBall;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(((Vector2)transform.position + GroundCheckOffset), GroundCheckSize);
    }

}

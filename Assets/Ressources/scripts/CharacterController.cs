using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Transform m_Transform;
    public Transform groundCheck;
    public Transform headCheck;
    public float jumpSpeed;
    private FollowerOfTheRhythm tempo;
    private float verticalVelocity;
    public float jumpDeceleration;
    public float maxFallSpeed;
    public float fallAcceleration;
    private bool grounded;
    private bool m_FacingRight = true;
    public float runSpeed = 10f;
    public float airSpeedMultiplier = 1.5f;
    public Transform sprite;
    private float slideSpeed = -1;
    private float beatDuration = 0;

    void Awake()
    {
        grounded = false;
        verticalVelocity = 0;
        tempo = GetComponent<FollowerOfTheRhythm>();
        m_Transform = GetComponent<Transform>();
    }

    void Update()
    {
    }

    public void Move(float horizontalMovement, bool hop, bool up, bool down)
    {
        horizontalMovement = slide(runSpeed * horizontalMovement, down);
        MoveHorizontal(runSpeed * horizontalMovement);
        VerticalMovement(hop, up);
    }

    public void MoveHorizontal(float horizontalMovement)
    {
        if (horizontalMovement > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (horizontalMovement < 0 && m_FacingRight)
        {
            Flip();
        }
        if (grounded)
            m_Transform.Translate(new Vector3(horizontalMovement * Time.deltaTime, 0));
        if (!grounded)
            m_Transform.Translate(new Vector3(airSpeedMultiplier * horizontalMovement * Time.deltaTime, 0));
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void VerticalMovement(bool hop, bool up)
    {
        if (grounded && hop && beatDuration <= 0)
        {
            grounded = false;
            verticalVelocity = jumpSpeed;
        }
        RaycastHit2D hitFloor = Physics2D.Raycast(groundCheck.position, Vector2.down);
        if (hitFloor.collider == null || hitFloor.distance > Mathf.Abs(verticalVelocity) + 0.1f)
        {
            grounded = false;
        }
        if (!grounded)
        {
            if (verticalVelocity > 0)
            {
                verticalVelocity -= jumpDeceleration * Time.deltaTime;
                RaycastHit2D hitCeil = Physics2D.Raycast(headCheck.position, Vector2.up);
                if (hitCeil.collider != null && hitCeil.distance < Mathf.Abs(verticalVelocity) + 0.1f)
                {
                    m_Transform.Translate(new Vector3(0, hitCeil.distance));
                    verticalVelocity = 0;
                }
            }
            if (verticalVelocity <= 0)
            {
                verticalVelocity -= fallAcceleration * Time.deltaTime;
                if (verticalVelocity < -maxFallSpeed)
                    verticalVelocity = maxFallSpeed;
                hitFloor = Physics2D.Raycast(groundCheck.position, Vector2.down);
                if (hitFloor.collider != null && hitFloor.distance < Mathf.Abs(verticalVelocity) + 0.1f)
                {
                    m_Transform.Translate(new Vector3(0, -hitFloor.distance));
                    grounded = true;
                    verticalVelocity = 0;
                }
            }
        }
        m_Transform.Translate(new Vector3(0, verticalVelocity));
    }

    public float slide(float horizontalMovement, bool down) 
    {
        if (down && beatDuration <= 0 && grounded)
        {
            slideSpeed = horizontalMovement * 2;
            beatDuration = 50 / tempo.getBpm();
            sprite.Translate(new Vector3(0, -0.2f, 0));
            sprite.Rotate(new Vector3(0, 0, 90));
        }
        if (beatDuration > 0)
        {
            beatDuration -= Time.deltaTime;
            if (beatDuration <= 0)
            {
                slideSpeed = -1;
                sprite.Rotate(new Vector3(0, 0, -90));
                sprite.Translate(new Vector3(0, 0.2f, 0));
            }
        }
        return slideSpeed == -1 ? horizontalMovement: slideSpeed ;

    }

    void jump() 
    {
        if (!grounded)
        {
            if (verticalVelocity > 0)
            {
                verticalVelocity -= jumpDeceleration * Time.deltaTime;
                RaycastHit2D hitCeil = Physics2D.Raycast(headCheck.position, Vector2.up);
                if (hitCeil.collider != null && hitCeil.distance < Mathf.Abs(verticalVelocity) + 0.1f)
                {
                    m_Transform.Translate(new Vector3(0, hitCeil.distance));
                    verticalVelocity = 0;
                }
            }
            if (verticalVelocity <= 0)
            {
                verticalVelocity -= fallAcceleration * Time.deltaTime;
                if (verticalVelocity < -maxFallSpeed)
                    verticalVelocity = maxFallSpeed;
                RaycastHit2D hitFloor = Physics2D.Raycast(groundCheck.position, Vector2.down);
                if (hitFloor.collider != null && hitFloor.distance < Mathf.Abs(verticalVelocity) + 0.1f)
                {
                    m_Transform.Translate(new Vector3(0, -hitFloor.distance));
                    grounded = true;
                    verticalVelocity = 0;
                }
            }
        }
    }

}

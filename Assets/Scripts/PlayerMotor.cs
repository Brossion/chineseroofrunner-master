using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using DG.Tweening;


public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    

    private float forwardSpeed = 10;
    public float increasingSpeed;
    private float maxSpeed = 20;
    private int desiredLane = 1;
    public float laneDistance = 4;
    private float jumpForce = 20;
    private float getDown = -40;
    private float gravity = -35;

    private bool jumpCancel = false;
    private bool slidingCancel =false;


    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator.SetBool("IsGrounded", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100)
        {
            Die();
        }
        if (!PlayerManager.isGameStarted)
        {
            return;
        }
        direction.z = forwardSpeed;

        if (forwardSpeed < maxSpeed)
            forwardSpeed += increasingSpeed * Time.deltaTime;

        if (controller.isGrounded && jumpCancel == false)
        {
            if (SwipeManager.swipeUp)
            {
                Jump();
            }

        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }
        if (SwipeManager.swipeDown && slidingCancel == false)
        {
            StartCoroutine(Slide());
            GetDown();
        }
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        Vector3 targetPosition = (transform.position.z * transform.forward) + (transform.position.y * transform.up);
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        //transform.position = targetPosition;
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }
    private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted)
        {
            return;
        }
        controller.Move(direction * Time.fixedDeltaTime);
    }
    private void Jump()
    {
        direction.y = jumpForce;
        animator.SetBool("IsGrounded", false);
        Invoke("StopJumping", 1);
    }
    private void GetDown()
    {
        direction.y = getDown;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Engeller")
        {
            PlayerManager.gameOver = true;
        }
        if (hit.transform.tag == "Finish")
        {
            PlayerManager.isFinished = true;
        }
        if (hit.transform.tag == "FlyPlane")
        {
            StartCoroutine(CancelJump());
            StartCoroutine(CancelSlide());
        }
        if (hit.transform.tag == "SlidingPlanes")
        {
            StartCoroutine(SlidingPlaneCR());
        }
    }
    public void Die()
    {
        PlayerManager.gameOver = true;
    }
    private IEnumerator Slide()
    {
        animator.SetBool("IsSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1.0f);
        controller.center = new Vector3(0, 3, 0);
        controller.height = 6;
        animator.SetBool("IsSliding", false);
    }
    void StopJumping()
    {
        animator.SetBool("IsGrounded", true);
    }
    private IEnumerator CancelJump()
    {
        jumpCancel = true;
        yield return new WaitForSeconds(10.0f);
        jumpCancel = false;
    }
    private IEnumerator CancelSlide()
    {
        slidingCancel = true;
        yield return new WaitForSeconds(10.0f);
        slidingCancel = false;
    }
    private IEnumerator JumpHigh()
    {
        slidingCancel = true;
        animator.SetBool("IsGrounded", false);
        direction.y = jumpForce * 3;
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("IsGrounded", true);
        slidingCancel = false; 
    }
    private IEnumerator JumpUpper()
    {
        slidingCancel = true;
        animator.SetBool("IsGrounded", false);
        direction.y = jumpForce * 1.5f;
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("IsGrounded", true);
        slidingCancel = false; 
    }
    private IEnumerator Fly()
    {
        slidingCancel = true;
        animator.SetBool("IsGrounded", false);
        animator.SetBool("IsFlying", true);
        direction.y = jumpForce * 5;
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("IsGrounded", true);
        slidingCancel = false;
    }
    private IEnumerator SlidingPlaneCR()
    {
        animator.SetBool("IsSliding", true);
        jumpCancel = true;
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("IsSliding", false);
        jumpCancel = false;
    }
    private IEnumerator FCandP()
    {
        animator.SetBool("IsFlying", true);
        jumpForce = 0;
        Jump();
        gravity = -6;
        forwardSpeed += 10;
        yield return new WaitForSeconds(4.5f);
        animator.SetBool("IsFlying", false);
        jumpForce = 20;
        forwardSpeed -= 10;
        gravity = -35;
    }
    private IEnumerator Planor()
    {
        slidingCancel = true;
        animator.SetBool("IsFlying", true);
        Jump();
        gravity = -25;
        forwardSpeed += 10;
        yield return new WaitForSeconds(3.0f);
        animator.SetBool("IsFlying", false);
        jumpForce = 20;
        forwardSpeed -= 10;
        gravity = -35;
        slidingCancel = false;
    }
    private IEnumerator Planor1()
    {
        animator.SetBool("IsFlying", true);
        Jump();
        gravity = -16;
        forwardSpeed += 10;
        yield return new WaitForSeconds(3.0f);
        animator.SetBool("IsFlying", false);
        jumpForce = 20;
        forwardSpeed -= 10;
        gravity = -35;
    }
    public void CallJumpHigh()
    {
        StartCoroutine(JumpHigh());
    }
    public void CallJumpUpper()
    {
        StartCoroutine(JumpUpper());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FlyBox")
        {
            StartCoroutine(Fly());
            GetComponent<Renderer>().enabled = false;
            animator.SetBool("IsFlying", true);
        }
        if (other.gameObject.tag == "FlyCancel")
        {
            StartCoroutine(FCandP());
            GetComponent<Renderer>().enabled = false;            
        }
        if (other.gameObject.tag == "Planor")
        {
            StartCoroutine(Planor());
            GetComponent<Renderer>().enabled = false;
        }
        if (other.gameObject.tag == "Planor1")
        {
            StartCoroutine(Planor1());
            GetComponent<Renderer>().enabled = false;
        }
    }
}

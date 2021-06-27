using System.Collections;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    #region dashing Variables
    /// <summary>
    /// Variables to use for dashing
    /// </summary>
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    private float dashTimer;
    private float dashCooldownTimer;
    private bool dashEh = false;
    #endregion

    #region character movements
    /// <summary>
    /// Variables to use for regular movement
    /// </summary>
    public Rigidbody2D rb;
    public Animator anim;
    public float speed;
    public bool shakeCamEh = false;

    private Vector2 movement;
    private Vector2 lastMovement;
    private bool canMoveEh = false;
    #endregion

    private void Start() {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashTimer = dashTime;
        StartCoroutine(startMoving());
    }

    public void Update() {
        if (canMoveEh)
            getInputs();
        animations();

        if (shakeCamEh) shakeCam();
        else unshakeCam();
    }

    public void FixedUpdate() {
        movePlayer();
    }
     
    void getInputs() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump") && dashCooldownTimer <= 0)
        {
            dashTimer = dashTime;
            dashEh = true;
        }
        if (dashCooldownTimer > 0) dashCooldownTimer -= Time.deltaTime;
        
        if(movement != Vector2.zero) lastMovement = movement;

    }

    void movePlayer() {
        Debug.Log("X move: " + movement.x + " Y move: " + movement.y);
        movement.Normalize();

        rb.velocity = movement * speed;
        if (dashEh) executeDash();
    }

    void executeDash() {
        if(dashTimer <= 0)
        {
            dashEh = false;
            rb.velocity = Vector2.zero;
            anim.SetBool("Dash", false);
            dashCooldownTimer = dashCooldown;
        }
        else
        {
            dashTimer -= Time.deltaTime;
            rb.velocity = movement.normalized * dashSpeed;
        }
    }

    void animations()
    {
        if (dashEh && rb.velocity.magnitude > 0.1f) anim.SetBool("Dash", true);

        if(lastMovement.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        }

        if(rb.velocity.magnitude > 0.1f) anim.SetBool("Idle", false);   
        else anim.SetBool("Idle", true);
        
        if (lastMovement.y > 0)
        {
            anim.SetBool("Up", true);
            anim.SetBool("Down", false);
        }
        else if (lastMovement.y < 0)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
        }
        else if (lastMovement.y == 0)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }

    }

    void shakeCam()
    {
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1;
    }

    void unshakeCam()
    {
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }

    IEnumerator startMoving()
    {
        yield return new WaitForSecondsRealtime(1f);
        canMoveEh = true;
    }

}

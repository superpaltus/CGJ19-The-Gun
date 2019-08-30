using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] [Range(0.01f, 10f)] float moveSpeed = 5f;

    [Header("Dash")]
    [SerializeField] [Range(0.01f, 10f)] float dashTime = 0.5f;
    [SerializeField] [Range(0.01f, 10f)] float dashForce = 3f;

    int floorMask;

    bool isDashing = false;

    Vector2 sumMove;

    Vector3 lookPos;

    Animation swordHit;

    ParticleSystem gunParticleSystem;

    Rigidbody rb;

    private void Start()
    {
        floorMask = LayerMask.GetMask("Floor");
        gunParticleSystem = transform.GetComponentInChildren<ParticleSystem>();
        swordHit = transform.GetComponentInChildren<Animation>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, 0, 0);
        ProcessTranslation();   // WASD moving
        ProcessRotation();  // look at cursor
        //ProcessHitting();   // melee attack
        //ProcessShooting();  // range attack
        StartCoroutine(ProcessDash());  // boosting if 'Jump' pressed

    }

    private void ProcessTranslation()
    {
        // get input
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        // creating normalized(it means that diagonal movement NOT faster) movement vector2 
        if (!isDashing)
        {
            sumMove = new Vector2(xMove, zMove);
        }
        sumMove.Normalize();
        sumMove = sumMove * moveSpeed;

        // calculating translation
        float newXPos = transform.position.x + sumMove.x * Time.deltaTime;
        float newZPos = transform.position.z + sumMove.y * Time.deltaTime;

        // applying translition
        transform.position = new Vector3(newXPos, transform.position.y, newZPos);
    }

    private void ProcessRotation()
    {
        // make player look at raycast hit point Vector3 (without 'y' component)
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, floorMask))
        {
            lookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(lookPos);
        }
    }

    private void ProcessHitting()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            swordHit.Play();
        }
    }

    private void ProcessShooting()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gunParticleSystem.Play();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            gunParticleSystem.Stop();
        }
    }

    IEnumerator ProcessDash()
    {

        if (Input.GetButtonDown("Jump") && !isDashing)
        {
            if (sumMove != Vector2.zero) // if player is moving then dashing at movement direction
            {
                isDashing = true;
                moveSpeed = moveSpeed * dashForce;
                yield return new WaitForSeconds(dashTime);
                moveSpeed = moveSpeed / dashForce;
                isDashing = false;
            }
            else if (sumMove == Vector2.zero) // if player doesn't move at this moment then dashing at mouse position
            {
                sumMove = new Vector2(lookPos.x - transform.position.x, lookPos.z - transform.position.z);
                sumMove.Normalize();
                isDashing = true;
                moveSpeed = moveSpeed * dashForce;
                yield return new WaitForSeconds(dashTime);
                moveSpeed = moveSpeed / dashForce;
                isDashing = false;
            }
        }
    }
}

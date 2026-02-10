using UnityEngine;
using System.Collections;

public class ScripController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runMultiplier = 1.5f;
    public float proneMultiplier = 0.25f;
    public float rotationSpeed = 10f;

    [Header("References")]
    public Rigidbody rb;
    public Animator animator;

    [Header("States")]
    public bool isGrounded;
    public bool isRun;
    public bool isProne;
    public bool isAttack;

    Vector3 movement;
    Coroutine attackRoutine;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        HandleInput();
        HandleAnimation();
        HandleAttack();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    // =========================
    // INPUT
    // =========================
    void HandleInput()
    {
        if (isAttack)
        {
            movement = Vector3.zero;
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        movement = camForward * v + camRight * h;

        if (movement.magnitude > 1f)
            movement.Normalize();
    }

    // =========================
    // MOVEMENT
    // =========================
    void HandleMovement()
    {
        if (!isGrounded || isAttack)
            return;

        float speed = moveSpeed;

        if (isProne)
            speed *= proneMultiplier;
        else if (isRun)
            speed *= runMultiplier;

        Vector3 targetPos = rb.position + movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);
    }

    // =========================
    // ROTATION
    // =========================
    void HandleRotation()
    {
        if (movement.sqrMagnitude < 0.001f || isProne || isAttack)
            return;

        Quaternion targetRot = Quaternion.LookRotation(movement);
        rb.MoveRotation(
            Quaternion.Slerp(
                rb.rotation,
                targetRot,
                rotationSpeed * Time.fixedDeltaTime
            )
        );
    }

    // =========================
    // ANIMATION
    // =========================
    void HandleAnimation()
    {
        bool isMoving = movement.sqrMagnitude > 0.01f;

        if (Input.GetKeyDown(KeyCode.C) && isGrounded && !isAttack)
            isProne = true;

        if (Input.GetKeyDown(KeyCode.V) && isGrounded && isProne)
            isProne = false;

        isRun = Input.GetKey(KeyCode.E)
                && isMoving
                && isGrounded
                && !isProne
                && !isAttack;

        animator.SetBool("isProne", isProne);
        animator.SetBool("isProneMove", isProne && isMoving);
        animator.SetBool("isRun", isRun);
        animator.SetBool("isWalking", isMoving && !isRun && !isProne);

        // 🔥 GỬI BOOL isAttack CHO ANIMATOR
        animator.SetBool("isAttack", isAttack);
    }

    // =========================
    // ATTACK (TRIGGER + BOOL)
    // =========================
    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0)
            && !isAttack
            && isGrounded
            && !isProne)
        {
            if (attackRoutine != null)
                StopCoroutine(attackRoutine);

            attackRoutine = StartCoroutine(AttackRoutine());
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Da");
        }
    }

    IEnumerator AttackRoutine()
    {
        // 🔥 BẬT CẢ HAI
        isAttack = true;
        animator.SetBool("isAttack", true);
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1f); // ⏱ 1 GIÂY

        // 🔥 TẮT CẢ HAI
        isAttack = false;
        animator.SetBool("isAttack", false);
        animator.ResetTrigger("Attack");
    }

    // =========================
    // GROUND CHECK
    // =========================
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
    private bool isMoving;
    private Vector2 moveInput;

    private Animator animator;
    private int animatorParameterMoveXId,
                animatorParameterMoveYId, animatorParameterIsMovingId;

    [SerializeField]
    private LayerMask solidObjectsLayer;
    
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animatorParameterMoveXId = Animator.StringToHash("moveX");
        animatorParameterMoveYId = Animator.StringToHash("moveY");
        animatorParameterIsMovingId = Animator.StringToHash("isMoving");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            if (moveInput.x != 0) moveInput.y = 0;

            if(moveInput != Vector2.zero)
            {
                animator.SetFloat(animatorParameterMoveXId, moveInput.x);
                animator.SetFloat(animatorParameterMoveYId, moveInput.y);

                var targetPos = transform.position;
                targetPos.x += moveInput.x;
                targetPos.y += moveInput.y;

                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool(animatorParameterIsMovingId, isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        return !Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer);
    }
}

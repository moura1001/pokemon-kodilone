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
    [SerializeField]
    private LayerMask grassLayer;

    [SerializeField] private EncounterTransitionRender battleTransition;
    private bool onBattleTransition;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animatorParameterMoveXId = Animator.StringToHash("moveX");
        animatorParameterMoveYId = Animator.StringToHash("moveY");
        animatorParameterIsMovingId = Animator.StringToHash("isMoving");
    }

    private void Start()
    {
        battleTransition.AddOnFinishTransitionListener(OnFinishTransition);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && !onBattleTransition)
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

            yield return CheckForEncounters();
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        return !Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer);
    }

    private IEnumerator CheckForEncounters()
    {
        if (IsGrass())
        {
            int encounterRate = Random.Range(1, 101);

            if (encounterRate <= 2)
            {
                onBattleTransition = true;
                Debug.Log("Encountered a wild Pokémon");
                yield return battleTransition.BattleTransition();
            }
        }
    }

    private bool IsGrass()
    {
        return Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer);
    }

    private void OnFinishTransition()
    {
        onBattleTransition = false;
    }
}

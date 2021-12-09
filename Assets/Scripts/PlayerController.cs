using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;
    [SerializeField] private Transform movePoint;

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
        movePoint.parent = null;
    }

    void Update()
    {
        if (!onBattleTransition)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= Mathf.Epsilon)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (IsWalkable(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f)))
                    {
                        animator.SetFloat(animatorParameterMoveXId, Input.GetAxisRaw("Horizontal"));
                        animator.SetFloat(animatorParameterMoveYId, 0f);
                        movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    }

                }
                else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (IsWalkable(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f)))
                    {
                        animator.SetFloat(animatorParameterMoveYId, Input.GetAxisRaw("Vertical"));
                        animator.SetFloat(animatorParameterMoveXId, 0f);
                        movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    }
                }

                animator.SetBool(animatorParameterIsMovingId, false);
            }
            else
            {
                animator.SetBool(animatorParameterIsMovingId, true);
                StartCoroutine(CheckForEncounters());
            }
        }
        else
        {
            animator.SetBool(animatorParameterIsMovingId, false);
            transform.position = movePoint.position;
        }
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        return !Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer);
    }

    private IEnumerator CheckForEncounters()
    {
        if (IsGrass())
        {
            int tileEncounterRate = Random.Range(15, 26);

            if (Random.Range(0, 256 * 16) < tileEncounterRate)
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

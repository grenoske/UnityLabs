using Core.Services.Updater;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UndeadController : MonoBehaviour, IDisposable
{
    public bool movingAnimation = false;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    public float attackDelay = 2f;
    private bool isAttacking = false;
    private bool faceRight = true;
    private Transform player;
    private Rigidbody2D rb;
    private Animator _animator;
    private NavMeshAgent navMeshAgent;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackDuration = 1f;
    private bool isMoving = false;
    private Vector2 initialPosition;
    public Sprite deadSprite;

    private void Start()
    {
        ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        initialPosition = transform.position;
    }

    private void OnUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if(gameObject.tag == "Dead")
        {
            Die();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            RotateTowardsPlayer();

            if (movingAnimation && !isAttacking)
            {
                isMoving = true;
                _animator.SetBool("isMoving", isMoving);
            }

            if (distanceToPlayer > attackRange)
            {
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                if (!isAttacking)
                {
                    Attack();
                }
            }
        }
        else if (!isAtInitialPosition())
        {
            if (movingAnimation)
            {
                isMoving = true;
                _animator.SetBool("isMoving", isMoving);
            }
            ReturnToInitialPosition();
        }
        else if (isMoving == true)
        {
            isMoving = false;
            _animator.SetBool("isMoving", isMoving);
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) >= 90f && faceRight)
        {
            rb.transform.Rotate(0, 180, 0);
            faceRight = false;
        }
        else if (Mathf.Abs(angle) < 90f && !faceRight)
        {
            rb.transform.Rotate(0, 180, 0);
            faceRight = true;
        }
    }

    private bool isAtInitialPosition()
    {
        float distanceToInitialPosition = Vector2.Distance(transform.position, initialPosition);
        return distanceToInitialPosition <= 1f;
    }

    private void ReturnToInitialPosition()
    {
        navMeshAgent.SetDestination(initialPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
/*            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRigidbody != null)
            {
                Vector2 pushDirection = collision.contacts[0].normal; // ???????? ?????????????
                float pushForce = 10f; // ???? ?????????????

                Debug.Log("Push");
                playerRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }*/
        }
    }

    private void Attack()
    {
        navMeshAgent.isStopped = true;
        if (movingAnimation)
        {
            isMoving = false;
            _animator.SetBool("isMoving", isMoving);
        }
        isAttacking = true;
        _animator.SetBool("isAttacking", isAttacking);
        Invoke(nameof(DelayAttack), attackDuration);
    }

    private void DelayAttack()
    {
        float attackDuration = _animator.GetCurrentAnimatorStateInfo(0).length;
        _animator.SetBool("isAttacking", false);
        Invoke(nameof(ResetAttack), attackDelay);
    }
    private void ResetAttack()
    {
        isAttacking = false;
        navMeshAgent.isStopped = false;
    }

    private void Die()
    {
        _animator.SetBool("isAttacking", false);
        if (movingAnimation == true)
            _animator.SetBool("isMoving", false);
        _animator.SetBool("isDying", true);

        // ?????? ??????? ?? deadSprite ????? ?????????? ???????? ??????
        StartCoroutine(ChangeSpriteAfterDeathAnimation());

        // ????????? ??? ????????? ???????????
        navMeshAgent.isStopped = true;
        rb.simulated = false;
        // ????????? ??????????
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    private IEnumerator ChangeSpriteAfterDeathAnimation()
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length); // ?????????? ?????????? ????????

        // ?????? ??????? ?? deadSprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = deadSprite;
    }

    public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
    private void OnDestroy()
    {
        Dispose();
    }
}





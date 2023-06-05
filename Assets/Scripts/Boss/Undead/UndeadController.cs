using UnityEngine;
using UnityEngine.AI;

public class UndeadController : MonoBehaviour
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
    NavMeshAgent navMeshAgent;
    public float detectionRange = 10f;
    private bool isMoving = false;

    public float MinDistanceToPlayer = 2f; 
    private Vector2 initialPosition;  

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;


        initialPosition = transform.position;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= detectionRange)
            {
                RotateTowardsPlayer();

                if (movingAnimation)
                {
                    isMoving = true;
                    _animator.SetBool("isMoving", isMoving);
                }

                if (distanceToPlayer > MinDistanceToPlayer)
                {
                    navMeshAgent.SetDestination(player.position);
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

    private void MoveTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        rb.velocity = direction.normalized * movementSpeed;
    }

    private void ReturnToInitialPosition()
    {
        navMeshAgent.SetDestination(initialPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        navMeshAgent.isStopped = true;
        isAttacking = true;
        if (movingAnimation)
        {
            isMoving = false;
            _animator.SetBool("isMoving", isMoving);
        }
        _animator.SetBool("isAttacking", isAttacking);

        Invoke(nameof(ResetAttack), attackDelay);
    }

    private void ResetAttack()
    {
        navMeshAgent.isStopped = false;
        isAttacking = false;
        _animator.SetBool("isAttacking", isAttacking);
    }


    private void Die()
    {
        transform.position = initialPosition;
    }
}




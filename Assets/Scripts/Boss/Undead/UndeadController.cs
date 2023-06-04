using UnityEngine;
using UnityEngine.AI;

public class UndeadController : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
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

    public float MinDistanceToPlayer = 2f; // ?????????? ????????? ?? ??????
    private Vector2 initialPosition;  // ????????? ??????? ????

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        // ???????? ????????? ???????
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
                if (distanceToPlayer > MinDistanceToPlayer)
                {
                    MoveTowardsPlayer();
                }
            }
            else if (!isAtInitialPosition())
            {
                ReturnToInitialPosition();
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
        return distanceToInitialPosition <= 1f; // ???????????, ?? ???????? ?? ?????????? ??????? ????? ??? ???????? 0.1
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        rb.velocity = direction.normalized * movementSpeed;
    }

    private void ReturnToInitialPosition()
    {
        Vector2 direction = initialPosition - (Vector2)transform.position;
        rb.velocity = direction.normalized * movementSpeed;
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
        rb.velocity = Vector2.zero;
        isAttacking = true;
        _animator.SetBool("isAttacking", isAttacking);

        Invoke(nameof(ResetAttack), attackDelay);
    }

    private void ResetAttack()
    {
        isAttacking = false;
        _animator.SetBool("isAttacking", isAttacking);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // ??????????? ????? ??????????? ?? ?????????? ??????? ????? ?????? ????
        transform.position = initialPosition;
    }
}




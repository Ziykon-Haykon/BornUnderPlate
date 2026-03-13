using UnityEngine;


public class Cube : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float defaultSpeed;
    private Vector2 moveInput;
    [SerializeField] private int maxHP = 100;
    private int currentHP;
    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;
    private Rigidbody2D rb;
    [SerializeField] private Transform altarSpawnPoint;
    private Transform player;
    [SerializeField] private float chaseDistance = 5f;
    [SerializeField] private float attackDistance = 1.5f;
    private float lastAttackTime;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private int damage = 50;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        defaultSpeed = speed;
    }

    void Update()
    {
        Vector2 input = GameInput.Instance.GetMoveVector();
        Vector2 movement = input * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bush"))
        {
            speed = 1.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bush"))
        {
            speed = defaultSpeed;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    
        if (currentHP <= 0)
        {
            Respawn();
        }
    }

    public void Attack()
    {
        player = GameObject.FindGameObjectWithTag("Ghost")?.transform;
        var distance = Vector2.Distance(transform.position, player.position);
        if (distance <= attackDistance)
        {
            if (Time.time - lastAttackTime > attackCooldown)
            {
                player.GetComponent<Ghost>().TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }

    private void Respawn()
    {
        currentHP = maxHP;
        rb.position = altarSpawnPoint.position;

    }

}

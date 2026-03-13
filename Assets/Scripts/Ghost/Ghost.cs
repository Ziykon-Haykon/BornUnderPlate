using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float chaseDistance = 5f;
    [SerializeField] private float attackDistance = 1.5f;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int maxHP = 10;
    private int currentHP;
    public int CurrentHP => currentHP;
    [SerializeField] private Transform SpawnPoint;

    private Transform player;
    private Rigidbody2D rb;
    private float lastAttackTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHP = maxHP;              
        if (player == null)
        {
            Debug.LogWarning("Player not found! Make sure the player has the tag 'Player'.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        var distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            if (Time.time - lastAttackTime > attackCooldown)
            {
                player.GetComponent<Cube>().TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
        else if (distance <= chaseDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = rb.position + direction * speed * Time.deltaTime;
            rb.MovePosition(newPosition);
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

    private void Respawn()
    {
        currentHP = maxHP;
        rb.position = SpawnPoint.position;

    }
}

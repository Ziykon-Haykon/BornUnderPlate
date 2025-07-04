using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float chaseDistance = 5f; // Дистанция, на которой начинает преследовать
    [SerializeField] private float attackDistance = 1.5f; // Дистанция атаки
    [SerializeField] private float attackCooldown = 1.0f; // Задержка между атаками
    [SerializeField] private int damage = 1;

    private Transform player;
    private Rigidbody2D rb;
    private float lastAttackTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning("Player not found! Make sure the player has the tag 'Player'.");
        }
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            // Атака
            if (Time.time - lastAttackTime > attackCooldown)
            {
                player.GetComponent<Cube>().TakeDamage(damage);
                lastAttackTime = Time.time;
            }
            // НЕ двигаем призрака!
        }
        else if (distance <= chaseDistance)
        {
            // Преследование через физику
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 newPosition = rb.position + direction * speed * Time.deltaTime;
            rb.MovePosition(newPosition);
        }
        // Если игрок далеко — ничего не делаем
    }
}

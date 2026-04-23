using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Calcula a direção do inimigo para o player
        Vector3 direction = (player.position - transform.position).normalized;
        // Aplica força para perseguir
        rb.AddForce(direction * speed);
    }
}
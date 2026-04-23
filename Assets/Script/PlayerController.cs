using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TMP_Text contagem;
    public GameObject imagemVitoria;
    public GameObject imagemDerrota;

    public AudioClip somColeta;
    public AudioClip somVitoria;
    public AudioClip somDerrota;
    private AudioSource audioSource;

    public float speed = 10.0f;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
        SetContagem();
        rb = GetComponent<Rigidbody>();
        imagemVitoria.SetActive(false);
        imagemDerrota.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            // Ativa o som de coleta
            if(somColeta != null)
            {
                audioSource.PlayOneShot(somColeta);
            }

            SetContagem();
        }
    }

    void SetContagem()
    {
        contagem.text = "Contagem: " + count.ToString();

        // Condição de vitória
        if(count >= 25)
        {   
            // Verifica se a imagem já está ativa para o som tocar apenas 1 vez
            if(!imagemVitoria.activeSelf)
            {
                if(somVitoria != null)
                {
                    audioSource.PlayOneShot(somVitoria);
                }
            }

            imagemVitoria.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision) // Usamos OnCollision para detectar toque físico
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Toca o som de derrota apenas se a tela de derrota ainda não estiver ativa
            if(!imagemDerrota.activeSelf)
            {
                if(somDerrota != null)
                {
                    audioSource.PlayOneShot(somDerrota);
                }
            }
            
            // Ativa a UI de derrota
            imagemDerrota.SetActive(true);

            // Desativa o movimento do player
            this.enabled = false; 
        }
    }
}

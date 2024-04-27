using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class main_character : MonoBehaviour
{
    public float velocidad = 5.0f;
    public float multiplicador = 1.5f;
    public float fuerzaSalto = 2.0f;
    public float gravedad = 9.8f;
    public float sensibilidadMouse = 800.0f;
    public GameObject target; 
    public Color[] colors;

    public Camera camara;
    public TextMeshProUGUI puntosText;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool enPiso;
    private int puntos = 0;

    private float rotacionX = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        UpdatePuntosText();

    }

    void Update()
    {
        enPiso = controller.isGrounded;
        if (enPiso && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 movimiento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movimiento = transform.TransformDirection(movimiento);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movimiento *= velocidad * multiplicador;
        }
        else
        {
            movimiento *= velocidad;
        }

        controller.Move(movimiento * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && enPiso)
        {
            playerVelocity.y += Mathf.Sqrt(fuerzaSalto * 2.0f * gravedad);
        }

        playerVelocity.y -= gravedad * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        if (camara != null)
        {
            camara.transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        }

        transform.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeTargetColor();
        }
    }


    void ChangeTargetColor()
    {

        if (target != null && colors.Length > 0)
        {
            Renderer targetRenderer = target.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                Color newColor = colors[Random.Range(0, colors.Length)];
                targetRenderer.material.color = newColor;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            puntos++;
            UpdatePuntosText();
        }
    }

    void UpdatePuntosText()
    {
        puntosText.text = "Puntos: " + puntos;
    }
}

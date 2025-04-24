using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipMovement : MonoBehaviour
{
    [SerializeField] float thrustForce = 5f;
    [SerializeField] float sideForce = 5f;
    [Tooltip("Сила, с которой тянет назад")]
    [SerializeField] float backForce = 1.0f;


    Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 velocity;

    PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Move.performed += read => moveInput = read.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += offRead => moveInput = Vector2.zero;
    }
    void OnEnable() => controls.Gameplay.Enable();
    void OnDisable() => controls.Gameplay.Disable();


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.up * moveInput.y * thrustForce);
        rb.AddForce(Vector2.down * backForce);

        rb.AddForce(Vector2.right * moveInput.x * sideForce);

        //ClampToScreen();
    }

    private void ClampToScreen()
    {
        Vector3 pos = rb.position;
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(pos);

        viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f); // немного отступаем от краёв
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0.0f, 1.0f);   // по желанию

        rb.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }

}

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float panBorderThickness = 10f; // px od krawędzi ekranu
    public Vector2 panLimitX = new Vector2(0, 20); // ograniczenie ruchu w X
    public Vector2 panLimitZ = new Vector2(0, 20); // ograniczenie ruchu w Z

    [Header("Zoom")]
    public float scrollSpeed = 20f;
    public float minY = 5f;
    public float maxY = 20f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;

    private void Awake()
    {
        panLimitX = new Vector2(0, GridManager.Instance.width);
        panLimitZ = new Vector2(0, GridManager.Instance.height);
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
    }

    void HandleMovement()
    {
        Vector3 pos = transform.position;

        // Sterowanie klawiaturą WASD
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= moveSpeed * Time.deltaTime;
        }

        // Ograniczenie ruchu w granicach planszy
        pos.x = Mathf.Clamp(pos.x, panLimitX.x, panLimitX.y);
        pos.z = Mathf.Clamp(pos.z, panLimitZ.x, panLimitZ.y);

        transform.position = pos;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * scrollSpeed;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(2)) // środkowy przycisk myszy
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotX, Space.World);
            transform.Rotate(Vector3.right, -rotY, Space.Self);
        }
    }
}

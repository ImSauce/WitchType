using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private Vector3 oldPosition;

    [Header("Drag Settings")]
    public float dragSensitivity = 0.5f; 
    private bool isDragging = false;
    private Vector3 lastMousePos;

    void Start()
    {
        oldPosition = transform.position;
    }

    void Update()
    {
        HandleMouseDrag();

        if (transform.position != oldPosition)
        {
            Vector2 delta = new Vector2(oldPosition.x - transform.position.x, 
                                        oldPosition.y - transform.position.y);

            onCameraTranslate?.Invoke(delta);
            oldPosition = transform.position;
        }
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePos;
            // drag both horizontally & vertically
            transform.position += new Vector3(-mouseDelta.x, -mouseDelta.y, 0) * dragSensitivity * Time.deltaTime;
            lastMousePos = Input.mousePosition;
        }
    }
}


// using UnityEngine;

// [ExecuteInEditMode]
// public class ParallaxCamera : MonoBehaviour
// {
//     public delegate void ParallaxCameraDelegate(float deltaMovement);
//     public ParallaxCameraDelegate onCameraTranslate;

//     private float oldPosition;

//     void Start()
//     {
//         oldPosition = transform.position.x;
//     }

//     void Update()
//     {
//         if (transform.position.x != oldPosition)
//         {
//             if (onCameraTranslate != null)
//             {
//                 float delta = oldPosition - transform.position.x;
//                 onCameraTranslate(delta);
//             }

//             oldPosition = transform.position.x;
//         }
//     }
// }
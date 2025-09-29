// using UnityEngine;

// [ExecuteInEditMode]
// public class ParallaxLayer : MonoBehaviour
// {
//     public float parallaxFactor;

//     public void Move(float delta)
//     {
//         Vector3 newPos = transform.localPosition;
//         newPos.x -= delta * parallaxFactor;

//         transform.localPosition = newPos;
//     }

// }

using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public Vector2 parallaxFactor = Vector2.one;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    public void Move(Vector2 offset)
    {
        // Always relative to start position (so no drift)
        Vector3 newPos = startPos;
        newPos.x += offset.x * parallaxFactor.x;
        newPos.y += offset.y * parallaxFactor.y;
        transform.localPosition = newPos;
    }
}

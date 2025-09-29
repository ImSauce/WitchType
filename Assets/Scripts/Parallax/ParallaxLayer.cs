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
    public Vector2 parallaxFactor; // separate factors for x and y

    public void Move(Vector2 delta)
    {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta.x * parallaxFactor.x;
        newPos.y -= delta.y * parallaxFactor.y;

        transform.localPosition = newPos;
    }
}
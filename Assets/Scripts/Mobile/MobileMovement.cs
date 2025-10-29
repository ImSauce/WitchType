using UnityEngine;

public class MobileMovement : MonoBehaviour
{
    public PlayerMovement player;

    private bool up, down, left, right;

    private void Update()
    {
        Vector2 dir = Vector2.zero;
        if (up) dir += Vector2.up;
        if (down) dir += Vector2.down;
        if (left) dir += Vector2.left;
        if (right) dir += Vector2.right;
       
        player.SetMovement(dir.normalized);
    }

    public void OnUpPress() => up = true;
    public void OnUpRelease() => up = false;
    public void OnDownPress() => down = true;
    public void OnDownRelease() => down = false;
    public void OnLeftPress() => left = true;
    public void OnLeftRelease() => left = false;
    public void OnRightPress() => right = true;
    public void OnRightRelease() => right = false;
}

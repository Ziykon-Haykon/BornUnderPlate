using UnityEngine;


public class Cube : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveInput;
    void Update()
    {
        Vector2 input = GameInput.Instance.GetMoveVector();
        Vector3 movement = new Vector3(input.x, input.y, 0.0f); // X и Y для Top Down
        transform.Translate(movement * speed * Time.deltaTime);
    }
}

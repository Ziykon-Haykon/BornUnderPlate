using UnityEngine;

public class SpriteSortingOrder : MonoBehaviour
{
    [SerializeField] private int sortingOrderBase = 0;
    [SerializeField] private float offset = 0;
    [SerializeField] private bool runOnce = false;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int)(sortingOrderBase - (transform.position.y + offset) * 100);
        if (runOnce)
            Destroy(this);
    }
}
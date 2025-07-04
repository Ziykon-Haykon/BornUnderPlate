using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Cube player;
    [SerializeField] private Image[] hearts; // Массив Image для сердец
    [SerializeField] private Sprite fullHeart; // Спрайт полного сердца
    [SerializeField] private Sprite emptyHeart; // Спрайт пустого сердца

    private void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.CurrentHP)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
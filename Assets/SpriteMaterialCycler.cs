using System.Collections.Generic;
using UnityEngine;

public class SpriteCycler : MonoBehaviour
{
    public List<Sprite> spriteList; // List of sprites to cycle through
    public List<Sprite> sprites; // List of sprites to cycle through
    public float changeInterval = 0.1f; // Time between sprite changes

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private bool isReversing = false;
    private float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteList == null || spriteList.Count == 0)
        {
            Debug.LogError("Sprite list is empty or null!");
            enabled = false; // Disable the script if there's no sprite to cycle through
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;
            UpdateSprite();
        }
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = spriteList[currentIndex];

        if (isReversing)
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = 1; // Reverse to the second sprite
                isReversing = false;
            }
        }
        else
        {
            currentIndex++;
            if (currentIndex >= spriteList.Count)
            {
                currentIndex = spriteList.Count - 2; // Reverse to the second-to-last sprite
                isReversing = true;
            }
        }
    }
}

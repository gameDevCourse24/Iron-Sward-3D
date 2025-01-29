using UnityEngine;

public class ScrollingText : MonoBehaviour
{
    public float speed = 50f;  // מהירות הזזת הטקסט
    private RectTransform rectTransform;
    private float startY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;  // שומר את המיקום ההתחלתי של הטקסט
    }

    void Update()
    {
        // הזזת הטקסט כלפי מעלה
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startY + Time.time * speed);
    }
}

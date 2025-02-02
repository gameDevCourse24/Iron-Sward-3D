using UnityEngine;

public class ShowInMiniMap : MonoBehaviour
{
    [SerializeField, Tooltip("The mini-map camera")] private
    Camera miniMapCamera; // המצלמה של המיני-מפה

    [SerializeField, Tooltip("The name of the layer you want to reveal")] private
    string layerToReveal = "HiddenLayer"; // שם השכבה שצריך להוסיף

    [SerializeField, Tooltip("Would you like the layer to be displayed from the collision until the end of the game?")]
    private bool permanent = true; // האם זה צריך להישאר עד סוף המשחק

    [SerializeField, Tooltip("The object name that activate the leyer reveal")] private
    string activateObjectName = "object"; // שם האובייקט שמפעיל את השכבה
    private int originalCullingMask; // משתנה לשמירת המצב המקורי

    void Start()
    {
        if (miniMapCamera != null)
        {
            // שמירת ה-Culling Mask המקורי כדי שנוכל לחזור אליו אם צריך
            originalCullingMask = miniMapCamera.cullingMask;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (miniMapCamera != null && other.gameObject.name == activateObjectName)
        {
            int layerMask = LayerMask.GetMask(layerToReveal);
            miniMapCamera.cullingMask |= layerMask; // הוספת השכבה למצלמה
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (miniMapCamera != null && other.gameObject.name == activateObjectName && !permanent)
        {
            miniMapCamera.cullingMask = originalCullingMask; // חזרה למצב המקורי
        }
    }
}


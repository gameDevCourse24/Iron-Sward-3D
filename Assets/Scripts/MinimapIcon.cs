
using UnityEngine;
using UnityEngine.UI;

public class MinimapIcon : MonoBehaviour
{
    [SerializeField, Tooltip("The mini-map camera")] private
    string minimapCameraName = "miniMapCamera"; // המצלמה של המיני-מפה

    [SerializeField, Tooltip("The mini-map camera")] private
    string minimapCanvasName = "MiniMapUI"; // הקנבס של המיני-מפה

    [SerializeField, Tooltip("The mini-map camera")] private
    Image minimapIconPrefab; // הפריפאב של האייקון במיני-מפה


    private Camera minimapCamera;
    private Transform minimapCanvas;
    private Image minimapIconInstance; // האייקון שנוצר

    void Start()
    {
        // חיפוש אוטומטי של המצלמה לפי שם
        GameObject cameraObj = GameObject.Find(minimapCameraName);
        if (cameraObj != null)
        {
            minimapCamera = cameraObj.GetComponent<Camera>();
        }
        else
        {
            Debug.LogWarning($"MinimapIcon: לא נמצאה מצלמה בשם {minimapCameraName}");
            return;
        }

        // חיפוש אוטומטי של הקנבס לפי שם
        GameObject canvasObj = GameObject.Find(minimapCanvasName);
        if (canvasObj != null)
        {
            minimapCanvas = canvasObj.transform;
        }
        else
        {
            Debug.LogWarning($"MinimapIcon: לא נמצא קנבס בשם {minimapCanvasName}");
            return;
        }

        // יצירת אייקון במיני-מפה
        if (minimapIconPrefab != null)
        {
            minimapIconInstance = Instantiate(minimapIconPrefab, minimapCanvas);
        }
        else
        {
            Debug.LogWarning("MinimapIcon: לא הוגדר Prefab לאייקון");
        }
    }

    void Update()
    {
        if (minimapIconInstance != null && minimapCamera != null)
        {
            // המרת מיקום העולם למיקום ב-Viewport של מצלמת המיני-מפה
            Vector3 viewportPos = minimapCamera.WorldToViewportPoint(transform.position);

            // המרת ה-Viewport ל-UI
            RectTransform rectTransform = minimapIconInstance.GetComponent<RectTransform>();
            rectTransform.anchorMin = rectTransform.anchorMax = viewportPos;
        }
    }

    void OnDestroy()
    {
        if (minimapIconInstance != null)
        {
            Destroy(minimapIconInstance.gameObject); // השמדת האייקון כאשר האובייקט נהרס
        }
    }
}


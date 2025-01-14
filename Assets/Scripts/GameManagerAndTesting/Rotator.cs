using UnityEngine;

//This script rotates the object it is attached to around the selected axes.
public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 100f;
    // אפשרויות בחירה לצירי הסיבוב
    public bool rotateX = false;
    public bool rotateY = true; // ברירת מחדל: ציר Y
    public bool rotateZ = false;

    // Update is called once per frame
    void Update()
    {
        // חישוב ציר הסיבוב המבוסס על הבחירות
        Vector3 rotationAxis = new Vector3(
            rotateX ? 1 : 0,
            rotateY ? 1 : 0,
            rotateZ ? 1 : 0
        );

        // סיבוב סביב הצירים שנבחרו
        if (rotationAxis != Vector3.zero) // כדי למנוע סיבוב מיותר אם כל הצירים מבוטלים
        {
            transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
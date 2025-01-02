using Unity.VisualScripting;
using UnityEngine;
//this code will delete an object after the time that you decide
public class DeleteObjectByTime : MonoBehaviour
{
    
    [SerializeField, Tooltip("The amount of time before the object is deleted. The defult units are seconds, but you can change this to minutes with the checkbox below")]
    float timerInSeconds = 5f;
    [SerializeField, Tooltip("Change the units to minutes?")]
    private bool useMinutes = false;

    void Start()
    {
        // מחשב את זמן המחיקה על בסיס היחידות שנבחרו
        float timeInSeconds = useMinutes ? timerInSeconds * 60f : timerInSeconds;

        // קורא לפונקציה DeleteObject אחרי הזמן המחושב
        Invoke(nameof(DeleteObject), timeInSeconds);
    }

    void DeleteObject()
    {
        if (gameObject != null)
        {
        // מוחק את האובייקט
            Destroy(gameObject);
            Debug.Log(gameObject.name + "deleted");
        }
        else
        {
            Debug.LogWarning("The object is already deleted");
        }
    }
}

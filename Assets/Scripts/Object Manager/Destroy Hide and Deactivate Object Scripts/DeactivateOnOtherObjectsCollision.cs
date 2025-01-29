using UnityEngine;


// This script deactivate the object when objectA collide with objectB.
public class DeactivateOnOtherObjectsCollision  : MonoBehaviour
{
    [SerializeField, Tooltip("The first object that should trigger the deletion")]
    private GameObject objectA;

    [SerializeField, Tooltip("The second object that should collide with the first object")]
    private GameObject objectB;

    private void Start()
    {
        if (objectB == null || objectA == null)
        {
            pprint.p("Please assign Object B and Object C in the inspector.", this);
        }
    }

    private void Update()
    {
        // בודק האם שני האובייקטים עדיין קיימים
        if (objectB == null || objectA == null)
            return;

        // אם הם התנגשו זה בזה – מוחק את האובייקט שעליו הסקריפט
        if (CheckCollision(objectA, objectB))
        {
            Debug.Log($"{objectA.name} and {objectB.name} collided. Destroying {gameObject.name}");
            Destroy(gameObject);
        }
    }

    private bool CheckCollision(GameObject obj1, GameObject obj2)
    {
        Collider col1 = obj1.GetComponent<Collider>();
        Collider col2 = obj2.GetComponent<Collider>();

        if (col1 == null || col2 == null)
        {
            pprint.p("One or both objects do not have a Collider component!", this);
            return false;
        }

        return col1.bounds.Intersects(col2.bounds);
    }
}

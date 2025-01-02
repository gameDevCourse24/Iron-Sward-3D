using UnityEngine;
using UnityEngine.InputSystem;
//this code will switch between 2 weapons by clicking a key

public class WeaponsClickSwitch : MonoBehaviour
{
    [SerializeField, Tooltip("Insert one weapon here")]
    public GameObject weapon1; //public so you can change it from another script.
    [SerializeField, Tooltip("Insert another weapon here")]
    public GameObject weapon2; //public so you can change it from another script.

    [SerializeField, Tooltip("The key that you want to use to switch between the weapons")]
    InputAction switchAction;

     private void OnEnable()
    {
        switchAction.Enable();
    }
    private void OnDisable()
    {
        switchAction.Disable();
    }
    void OnValidate()
    {
        if (switchAction == null)
            switchAction = new InputAction(type: InputActionType.Button);
        if (switchAction.bindings.Count == 0)
            switchAction.AddBinding("<Keyboard>/Q");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapon1.SetActive(true);
        weapon2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (switchAction.triggered)
        {
            SwitchWeapons();
        }
    }
    private void SwitchWeapons()
    {
        weapon1.SetActive(!weapon1.activeSelf);
        weapon2.SetActive(!weapon2.activeSelf);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class PlayerController : MonoBehaviour
{
    private InputDevice leftController;
    private InputDevice rightController;
    float gripValue;
    bool isButton;
    public GameObject spellBook;



    List<InputDevice> foundControllers = new List<InputDevice>();
    void Start()
    {
        spellBook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        LeftControllerActive();
        RightControllerActive();

        leftController.TryGetFeatureValue(CommonUsages.grip, out gripValue);
        if (gripValue > 0.9f)
            spellBook.SetActive(true);
        else
            spellBook.SetActive(false);

        rightController.TryGetFeatureValue(CommonUsages.secondaryButton,out isButton);
        Debug.Log(isButton);

    }
    private void LeftControllerActive()
    {
        InputDeviceCharacteristics leftTrackedControllerFilter = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left;

        InputDevices.GetDevicesWithCharacteristics(leftTrackedControllerFilter, foundControllers);
        if (foundControllers.Count > 0)
            leftController = foundControllers[0];      
    }
    private void RightControllerActive()
    {
        InputDeviceCharacteristics leftTrackedControllerFilter = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right;

        InputDevices.GetDevicesWithCharacteristics(leftTrackedControllerFilter, foundControllers);
        if (foundControllers.Count > 0)
            rightController = foundControllers[0];
    }
}

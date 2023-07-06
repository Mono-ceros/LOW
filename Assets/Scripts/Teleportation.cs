using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor interactor;
    [SerializeField] private TeleportationProvider provider;
    private InputAction _thumbstick;
    private bool _isActive;

    void Start()
    {
        interactor.enabled = false;

        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;

        var cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;

        _thumbstick = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        _thumbstick.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
            return;
        if (_thumbstick.triggered)
            return;
        
        if (!interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            interactor.enabled = false;
            _isActive = false;
            return;
        }
        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
            //destinationRotation ?,
        };

        provider.QueueTeleportRequest(request);
        TeleportOff();
    }
    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        interactor.enabled = true;
        _isActive = true;
    }
    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        TeleportOff();
    }
    void TeleportOff()
    {
        interactor.enabled = false;
        _isActive = false;
    }
}

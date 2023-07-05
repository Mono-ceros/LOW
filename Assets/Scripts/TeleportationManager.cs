using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [Header("LeftHand Teleportation Controller")]
    [SerializeField] private XRRayInteractor interactor;
    [SerializeField] private TeleportationProvider teleportationProvider;   
    [SerializeField] private InputActionProperty teleportActivate;
    [SerializeField] private InputActionProperty teleportCancel;
    [SerializeField] private InputActionProperty thumbMove;
    [SerializeField] private InputActionProperty gripActivate;

    private bool isTeleport; 
    void Start()
    {
        teleportActivate.action.Enable();
        teleportCancel.action.Enable();
        thumbMove.action.Enable();
        gripActivate.action.Enable();
        interactor.enabled = false;
        teleportActivate.action.performed += OnTeleportActivate;
        teleportCancel.action.performed += OnTeleportCancel;
    }
    void Update()
    {
        if (!isTeleport)
            return;
        if (thumbMove.action.triggered)
            return;

        interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit);
        TeleportRequest request = new TeleportRequest();
        request.destinationPosition = hit.point;
        teleportationProvider.QueueTeleportRequest(request);
        TurnOffTeleport();
    }

    void OnTeleportActivate(InputAction.CallbackContext obj)
    {
        if(gripActivate.action.phase != InputActionPhase.Performed)
        {
            isTeleport = true;
            interactor.enabled = true;
        }

    }

    void OnTeleportCancel(InputAction.CallbackContext obj)
    {
        TurnOffTeleport();
    }
    private void TurnOffTeleport()
    {
        isTeleport = false;
        interactor.enabled = false;
    }

}

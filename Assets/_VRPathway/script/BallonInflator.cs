using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class BallonInflator : XRGrabInteractable
{
    [Header("Balloon Data")]
    public Transform attachPoint;
    public Balloon balloonPrefab;
    private Balloon m_BalloonInstance;
    private XRBaseInputInteractor m_Controller;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        if (balloonPrefab != null && m_BalloonInstance == null)
        {
            m_BalloonInstance = Instantiate(balloonPrefab, attachPoint.position, attachPoint.rotation);
            m_BalloonInstance.AttachToInflator(this);
            // The interactor that grabbed us; XRBaseInputInteractor exposes activateInput (trigger).
            m_Controller = args.interactorObject as XRBaseInputInteractor;
        }
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (isSelected && m_BalloonInstance != null && m_Controller != null)
        {
            float triggerValue = m_Controller.activateInput.ReadValue();
            m_BalloonInstance.Inflate(triggerValue);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        if (m_BalloonInstance != null)
        {
            m_BalloonInstance.ReleaseFromInflator();
            m_BalloonInstance = null;
        }
        m_Controller = null;
    }
}

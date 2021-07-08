using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class TwoHandGrabInteractable : XRGrabInteractable
{
    public enum TwoHandRotationType
    {
        None,
        First,
        Second
    }

    public List<XRSimpleInteractable> SecondHandGrabPoint = new List<XRSimpleInteractable>();
    private XRBaseInteractor secondinteractor;
    private Quaternion attachinitialrotation;
    public TwoHandRotationType twoHandRotationType;
    public bool SnapToSecondHand = true;
    private Quaternion InitialRotationOffset;

    void Start()
    {
        foreach (var item in SecondHandGrabPoint)
        {
            item.onSelectEntered.AddListener(OnSecondHandGrab);
            item.onSelectExited.AddListener(OnSecondHandRelease);

        }
    }

    //protected override void OnSelectEntered(XRBaseInteractor interactor)
    //{
    //    base.OnSelectEntered(interactor);
    //}

    //protected override void OnSelectExited(XRBaseInteractor interactor)
    //{
    //    base.OnSelectExited(interactor);
    //}

    void Update()
    {

    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (secondinteractor && selectingInteractor)
        {
            if (SnapToSecondHand)
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation();
            else
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation() *InitialRotationOffset;
           
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetrotation;

        switch (twoHandRotationType)
        {
            case TwoHandRotationType.None:
                targetrotation = Quaternion.LookRotation(secondinteractor.attachTransform.position - selectingInteractor.attachTransform.position); return targetrotation;
            case TwoHandRotationType.First:
                targetrotation = Quaternion.LookRotation(secondinteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.attachTransform.up);return targetrotation;
            case TwoHandRotationType.Second:
                targetrotation = Quaternion.LookRotation(secondinteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondinteractor.attachTransform.up);return targetrotation;
            default:
                return new Quaternion();
        }
    }

    public void OnSecondHandGrab(XRBaseInteractor interactor)
    {
        secondinteractor = interactor;
        InitialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * selectingInteractor.attachTransform.rotation;
    }

    public void OnSecondHandRelease(XRBaseInteractor interactor)
    {
        secondinteractor = null;
    }

    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntering(interactor);
        attachinitialrotation = interactor.attachTransform.localRotation;
    }
    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExiting(interactor);
        secondinteractor = null;
        interactor.attachTransform.localRotation = attachinitialrotation;
    }
    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {
        bool isalredygrabble = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor);
    }
}

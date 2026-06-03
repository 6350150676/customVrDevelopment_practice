using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [Header("Inflation")]
    [Tooltip("Scale the balloon starts at when spawned.")]
    public Vector3 minScale = Vector3.one * 0.1f;
    [Tooltip("Scale the balloon reaches when fully inflated.")]
    public Vector3 maxScale = Vector3.one;
    [Tooltip("How fast the balloon grows while the trigger is fully held.")]
    public float inflateSpeed = 0.5f;

    private Rigidbody m_Rigidbody;
    private BallonInflator m_Inflator;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = true;
        m_Rigidbody.useGravity = false;
    }

    public void AttachToInflator(BallonInflator inflator)
    {
        m_Inflator = inflator;
        transform.SetParent(inflator.attachPoint, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = minScale;
    }

    public void Inflate(float triggerValue)
    {
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            maxScale,
            inflateSpeed * triggerValue * Time.deltaTime);
    }

    public void ReleaseFromInflator()
    {
        m_Inflator = null;
        Detach();
    }

    public void Detach()
    {
        transform.SetParent(null);
        m_Rigidbody.isKinematic = false;
        var force = gameObject.AddComponent<ConstantForce>();

        force.force = Vector3.up;
    }
}

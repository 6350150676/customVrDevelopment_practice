using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;

public class Scanner : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [Header("Scanner Data")]
        public Animator animator;
        public LineRenderer laserRenderer;
        Renderer rend;
        Material[] originalMaterials;
        public Material highlightMaterial;
        protected override void Awake()
        {
                base.Awake();
                rend = GetComponent<Renderer>();
                originalMaterials = rend.sharedMaterials;
                laserRenderer.gameObject.SetActive(false);
        }

        void Start()
        {
                // animator.SetBool("opened", true);
        }
        protected override void OnSelectEntered(UnityEngine.XR.Interaction.Toolkit.SelectEnterEventArgs args)
        {
                base.OnSelectEntered(args);
                animator.SetBool("Opened", true);
        }
        protected override void OnSelectExited(UnityEngine.XR.Interaction.Toolkit.SelectExitEventArgs args)
        {
                base.OnSelectExited(args);
                animator.SetBool("Opened", false);
        }
        protected override void OnActivated(UnityEngine.XR.Interaction.Toolkit.ActivateEventArgs args)
        {
                base.OnActivated(args);
                laserRenderer.gameObject.SetActive(true);
        }
        protected override void OnDeactivated(UnityEngine.XR.Interaction.Toolkit.DeactivateEventArgs args)
        {
                base.OnDeactivated(args);
                laserRenderer.gameObject.SetActive(false);
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
                base.OnHoverEntered(args);
                rend.materials = new Material[] { highlightMaterial };
        }

        protected override void OnHoverExited(HoverExitEventArgs args)
        {
                base.OnHoverExited(args);
                rend.materials = originalMaterials;
        }


        // Update is called once per framex
        void Update()
        {

        }
}

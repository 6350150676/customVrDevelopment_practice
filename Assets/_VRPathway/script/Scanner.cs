using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class Scanner : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [Header("Scanner Data")]
        public Animator animator;
        public LineRenderer laserRenderer;
        Renderer rend;
        Material[] originalMaterials;
        public Material highlightMaterial;
        public TextMeshProUGUI targetName;
        public TextMeshProUGUI targetPosition;
        protected override void Awake()
        {
                base.Awake();
                rend = GetComponent<Renderer>();
                originalMaterials = rend.sharedMaterials;
                targetName.gameObject.SetActive(false);
                targetPosition.gameObject.SetActive(false);
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
                targetName.gameObject.SetActive(true);
                targetPosition.gameObject.SetActive(true);
                // ScanForObjects();
        }
        private void ScanForObjects()
        {
                RaycastHit hit;
                Vector3 worldHit = laserRenderer.transform.position + laserRenderer.transform.forward * 100f;
                if (Physics.Raycast(laserRenderer.transform.position, targetName.transform.forward, out hit))
                {
                        worldHit = hit.point;
                        targetName.SetText(hit.collider.gameObject.name);
                        targetPosition.SetText(hit.collider.gameObject.transform.position.ToString());
                }
                laserRenderer.SetPosition(1, laserRenderer.transform.InverseTransformPoint(worldHit));
        }
        protected override void OnDeactivated(UnityEngine.XR.Interaction.Toolkit.DeactivateEventArgs args)
        {
                base.OnDeactivated(args);
                laserRenderer.gameObject.SetActive(false);
                targetName.gameObject.SetActive(false);
                targetPosition.gameObject.SetActive(false);
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
        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
                base.ProcessInteractable(updatePhase);
                if (laserRenderer.gameObject.activeSelf)
                {
                        ScanForObjects();
                }
        }


        // Update is called once per framex
        void Update()
        {

        }
}

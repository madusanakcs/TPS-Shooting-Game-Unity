using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Keynetwork{
    public class KeyRayCast : MonoBehaviour
    {
        [Header ("Raycast Radius and Leyers")]
        [SerializeField] private float raycastRadius = 6;
        [SerializeField] private LayerMask layerMaskCollective;
        [SerializeField] private string banLayerName=null;

        private KeyObjectRegulator raycastedObject;
        [SerializeField] private KeyCode openGateButton = KeyCode.F;
        [SerializeField] private Image crosshair=null;

        private bool checkCrosshair;
        private bool Ontime;
        
        private string collectiveTag="colectiveObject";

        private void Update()
        {
            RaycastHit hitinfo;
            Vector3 forwardDirrection = transform.TransformDirection(Vector3.forward);
            int mask =1 << LayerMask.NameToLayer(banLayerName) | layerMaskCollective.value;

            if (Physics.Raycast(transform.position, forwardDirrection, out hitinfo, raycastRadius, mask))
            {  
                 
                if (hitinfo.collider.CompareTag(collectiveTag))
                {
                    if (!Ontime)
                    {
                        raycastedObject=hitinfo.collider.GetComponent<KeyObjectRegulator>();
                        ChangeCrosshair(true);
                    }
                    checkCrosshair = true;
                    Ontime = true;

                    if (Input.GetKeyDown(openGateButton))
                    {
                        raycastedObject.foundObject();
                        Debug.Log("Found key.");
                    }
                }
            }
            else
            {
                if (checkCrosshair)
                {
                    ChangeCrosshair(false);
                    Ontime = false;
                }
                
            }
        }

    void ChangeCrosshair(bool on)
    {
        if (on && !Ontime)
        {
            crosshair.color = Color.blue;
        }
        else
        {
            crosshair.color = Color.white;
            checkCrosshair = false;
        }
    }

    }
}



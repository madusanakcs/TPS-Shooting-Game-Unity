using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Keynetwork
{
    public class KeyGateRegulator : MonoBehaviour
    {
        [Header("Gate Animator")]
        private Animator gateAnimator;
        private bool gateOpen = false;
        [SerializeField] private string gateOpenTrigger = "GateOpen";
        [SerializeField] private string gateCloseTrigger = "GateClose";

        [Header("Time and UI")]
        [SerializeField] private int timeToShowUI = 1;
        [SerializeField] private GameObject ShowGateUI = null;
        [SerializeField] private KeyList keyList = null;
        [SerializeField] private int waitTime = 1;
        [SerializeField] private bool pauseInteraction = false;


        [Header("Sounds  ")]
        public AudioClip gateSound;
 
     public AudioSource audioSource;



        private void Awake()
        {
            gateAnimator = GetComponent<Animator>();
        }

        public void StatAnimation()
        {
            if (keyList.haskey)
            {
                OpenGate();
            }
            else
            {
                StartCoroutine(shaowGateLocked());
            }
        }


        private IEnumerator StopGateConnection()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTime);
            pauseInteraction = false;
        }

        void OpenGate()
        {
            if (!gateOpen && !pauseInteraction)
            {
                gateAnimator.Play(gateOpenTrigger,0,0.0f);
                gateOpen = true;
                audioSource.PlayOneShot(gateSound);
                CompleteObject.occurrence.GetObjectivesDone(true,false,false,false);
                StartCoroutine(StopGateConnection());
            }
            else if (gateOpen && !pauseInteraction)
            {
                gateAnimator.Play(gateCloseTrigger,0,0.0f);
                gateOpen = false;
            }
        }

        IEnumerator shaowGateLocked()
        {
            ShowGateUI.SetActive(true);
            yield return new WaitForSeconds(timeToShowUI);
            ShowGateUI.SetActive(false);
        }
    }
}
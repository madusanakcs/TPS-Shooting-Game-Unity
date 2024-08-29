using UnityEngine;
using UnityEngine.Playables;

public class TimelineControl : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject gameplayCamera;
    public GameObject aimpointCanvas;
    public GameObject cutsceneCamera;

    private bool timelineFinished = false;

    void Start()
    {
        // Start playing the Timeline
        timeline.Play();
        // Pause the gameplay camera
        gameplayCamera.SetActive(false);
        // aimCanvas disable
        aimpointCanvas.SetActive(false);
    }

    void Update()
    {
        // Check if the Timeline has finished playing
        if (timeline.state != PlayState.Playing && !timelineFinished)
        {   
            // Switch to the gameplay camera
            gameplayCamera.SetActive(true);
            // Disable the cutscene camera
            cutsceneCamera.SetActive(false);
            // Enable aimpointCanvas
            aimpointCanvas.SetActive(true);
            // Set the flag to true to avoid re-entering this block
            timelineFinished = true;
            // Disable this script to stop updating
            enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalObjective : MonoBehaviour
{
   [Header("Vehicle button")]
    [SerializeField] private KeyCode VehicleButton=KeyCode.F;


    [Header("Genaraor Sound Effects and radious")]
    private float radius_gen =3f;
    public playerScript player;

    private void Update()
    {
        if(Input.GetKeyDown(VehicleButton) && Vector3.Distance(transform.position ,player.transform.position)<radius_gen)
        {
            Time.timeScale=1f;
            SceneManager.LoadScene("EndGameMenu");

            // Object complete
            CompleteObject.occurrence.GetObjectivesDone(true,true,true,true);
        }
    }
}

using UnityEngine;

public class TriggerPuzzleScript : MonoBehaviour
{
 
    public bool redBox = false;


    public void OnTriggerStay(Collider other)
    {
            if (other.CompareTag("redBox"))
            {
                redBox = true;
                Debug.Log("Triggered red");
            }
    }

    public void OnTriggerExit(Collider other)
    {
            if (other.gameObject.name == "redBox")
            {
                redBox = false;
            }
            

    }
}

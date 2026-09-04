using UnityEngine;

public class TriggerPuzzleScript2 : MonoBehaviour
{
  
    public bool blueBox = false;


    public void OnTriggerStay(Collider other)
    {
            if (other.CompareTag("blueBox"))
            {
                blueBox = true;
            }
    }

    public void OnTriggerExit(Collider other)
    {
            if (other.gameObject.name == "blueBox")
            {
                blueBox = false;
            }
            

    }
}

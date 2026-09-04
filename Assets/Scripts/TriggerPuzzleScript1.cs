using UnityEngine;

public class TriggerPuzzleScript1 : MonoBehaviour
{
  
    public bool greenBox = false;


    public void OnTriggerStay(Collider other)
    {
            if (other.CompareTag("greenBox"))
            {
                greenBox = true;
            }
    }

    public void OnTriggerExit(Collider other)
    {
            if (other.gameObject.name == "greenBox")
            {
                greenBox = false;
            }
    }
}

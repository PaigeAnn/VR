using UnityEngine;

public class TriggerPuzzleScript : MonoBehaviour
{
  
    public string objTag;
    bool redBox = false;


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(objTag))
        {
            if (other.gameObject.name == "RedBox")
            {
                redBox = true;
                Debug.Log("Triggered");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(objTag))
        {
            if (other.gameObject.name == "RedBox")
            {
                redBox = false;
            }
            
        }

    }
}

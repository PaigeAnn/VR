using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("key"))
        {
            door.SetActive(false);
        }
    }
}

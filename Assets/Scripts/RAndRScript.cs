using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RAndRScript : MonoBehaviour
{
    public GameObject helmet;
    public Transform helmetReset;

    private void Start()
    {
        helmetReset = helmet.transform;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reset"))
        {
            Debug.Log("triggerReset");
            helmet.transform.position = helmetReset.position;
            helmet.transform.rotation = helmetReset.rotation;
        }
        if (other.gameObject.CompareTag("killFloor"))
        {

            Debug.Log("triggerKill1");
            StartCoroutine(KillFloor());
        } 
    }

    IEnumerator KillFloor()
    {
        Debug.Log("triggerKill2");
        helmet.SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScene");
    }

}

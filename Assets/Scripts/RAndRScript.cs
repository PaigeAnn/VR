using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RAndRScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("killFloor"))
        {

            Debug.Log("triggerKill1");
            StartCoroutine(KillFloor());
        } 
    }

    IEnumerator KillFloor()
    {
        Debug.Log("triggerKill2");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameScene");
    }

}

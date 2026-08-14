using UnityEngine;

public class LiftWall : MonoBehaviour
{

   public  TriggerPuzzleScript redBox;
    public TriggerPuzzleScript1 greenBox;
    public TriggerPuzzleScript2 blueBox;

    public Animation anim;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (redBox.redBox == true && greenBox.greenBox == true && blueBox.blueBox == true)
        {
            anim.Play();
        }

    }
}

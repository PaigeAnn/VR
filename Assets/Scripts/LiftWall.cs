using UnityEngine;

public class LiftWall : MonoBehaviour
{
    public TriggerPuzzleScript redBox;
    public TriggerPuzzleScript1 greenBox;
    public TriggerPuzzleScript2 blueBox;

    public SimpleHapticFeedback hapticFeedback;

    public Animation anim;

    private bool wallActivated = false;


    void Update()
    {
        if (!wallActivated &&
            redBox.redBox == true &&
            greenBox.greenBox == true &&
            blueBox.blueBox == true)
        {
            wallActivated = true;

            // Play wall animation
            anim.Play();

            // Play haptic feedback once
            hapticFeedback.PlayHapticFeedback(1f, 1.5f);
        }
    }
}
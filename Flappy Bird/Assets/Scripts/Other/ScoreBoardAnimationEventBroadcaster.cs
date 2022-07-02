using UnityEngine;

public class ScoreBoardAnimationEventBroadcaster : MonoBehaviour
{
    public event System.Action OnScoarBoardAnimationOver;

    public void AnimationOver()
    {
        if(OnScoarBoardAnimationOver != null) OnScoarBoardAnimationOver();
    }  
}

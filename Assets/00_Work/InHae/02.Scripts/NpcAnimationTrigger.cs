using UnityEngine;

public class NpcAnimationTrigger : MonoBehaviour
{
    [SerializeField] private Npc _npc;

    private void AnimationEnd()
    {
        _npc.AnimationEndTrigger();
    }
}

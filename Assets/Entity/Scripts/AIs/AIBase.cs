using DG.Tweening;
using UnityEngine;

public abstract class AIBase : ScriptableObject
{
    public abstract void Act(GameObject entity);

    public abstract Sequence GetSequence();
}

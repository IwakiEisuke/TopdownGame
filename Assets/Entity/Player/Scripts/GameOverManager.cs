using UnityEngine;
using UnityEngine.Events;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] UnityEvent gameOverEvent;

    public void InvokeGameOverEvent()
    {
        gameOverEvent.Invoke();
    }
}

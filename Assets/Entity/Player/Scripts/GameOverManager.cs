using UnityEngine;
using UnityEngine.Events;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] UnityEvent gameOverEvent;
    [SerializeField] UnityEvent gameRestartEvent;
    bool isGameOver;

    public void GameOverEvent()
    {
        gameOverEvent.Invoke();
        isGameOver = true;
    }

    private void GameRestartEvent()
    {
        //ínè„Ç…ãAä“
        gameRestartEvent.Invoke();
        MapManager.SetCurrentMap(0);
        PlayerController.Transform.position = Vector3.zero;
        PlayerController.Transform.rotation = Quaternion.identity;
        PlayerController.HealHP(PlayerController.Status.maxHP / 2);
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            GameRestartEvent();
            isGameOver = false;
        }
    }
}

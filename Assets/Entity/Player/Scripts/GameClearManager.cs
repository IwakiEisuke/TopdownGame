using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class GameClearManager : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] PixelPerfectCamera ppc;
    [SerializeField] UnityEvent gameClearEvents;
    [SerializeField] UnityEvent gameResetEvents;
    bool isClear;
    float t;

    // Start is called before the first frame update
    public void Enter()
    {
        gameClearEvents.Invoke();
        isClear = true;
        t = 0;
    }

    // Update is called once per frame
    public void Update()
    {
        t += Time.deltaTime / 1;
        if (isClear)
        {
            controller.Move(Mathf.Sin(Time.time * Mathf.PI * 2.5f), Mathf.Cos(Time.time * Mathf.PI * 2.5f));
            ppc.assetsPPU = (int)Mathf.Lerp(16, 32, OutEase(t));
        }

        if (isClear && Input.GetKeyDown(KeyCode.R))
        {
            Exit();
            MapManager.SetCurrentMap(0);
            PlayerController.Transform.position = Vector3.zero;
            PlayerController.Transform.rotation = Quaternion.identity;
            PlayerController.HealHP(PlayerController.Status.maxHP / 2);
        }
    }

    public void Exit()
    {
        gameResetEvents.Invoke();
        isClear = false;
    }

    private float OutEase(float x)
    {
        return 1 - Mathf.Pow(1 - x, 3);
    }
}

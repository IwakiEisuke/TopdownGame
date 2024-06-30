using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMouseEventHandler : MonoBehaviour
{
    public static event EventHandler LeftMouseClickNotUI, LeftMouseClickUI;
    [SerializeField] GameObject canvasUI;
    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;

    private void Start()
    {
        graphicRaycaster = canvasUI.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (IsPointerOverUI(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
    //        {
    //        }
    //        else
    //        {
    //            LeftMouseClickNotUI();
    //        }
    //    }
    //}

    protected virtual void OnLeftMouseClickNotUIEvent(EventArgs e)
    {
        LeftMouseClickUI?.Invoke(this, e);
    }

    /// <summary>
    /// position�i�X�N���[�����W�j��UI�����݂��邩���肷��
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    private bool IsPointerOverUI(Vector2 position) //AI����
    {
        if (graphicRaycaster != null)
        {
            // �|�C���^�̃f�[�^���Z�b�g�A�b�v
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = position;

            // ���C�L���X�g���i�[���郊�X�g���쐬
            List<RaycastResult> raycastResults = new List<RaycastResult>();

            // �O���t�B�b�N���C�L���X�^�[���g�p���ă��C�L���X�g�����s
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            // �����Ƀq�b�g�������ǂ�����Ԃ�
            return raycastResults.Count > 0;
        }
        Debug.Log("graphicRaycaster��null");
        return false;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUseController : MonoBehaviour
{
    [SerializeField] GameObject canvasUI;
    [SerializeField] AudioSource selectAS;
    public GameObject player;
    public GameObject itemBase;
    public float power;
    public float torque;
    public static int selectedItem;
    public TextMeshProUGUI text;
    public float coolTime;

    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;

    private void Start()
    {
        graphicRaycaster = canvasUI.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        ItemObjectCreator.itemBase = itemBase;
    }

    private void Update()
    {
        coolTime -= Time.deltaTime;
        //�A�C�e���g�p����
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)) && coolTime <= 0)
        {
            var item = Inventory.GetSelectedItem();

            if (item.amount > 0 && !IsPointerOverUI(Input.mousePosition))
            {
                foreach (var skill in item.skills)
                {
                    skill.Action(this, item);
                }
            }
        }

        //�����L�[�ŃA�C�e���I��
        for (int i = 0; i < Inventory.Items.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectAS.Play();
                selectedItem = i;
                break;
            }
        }

        //�}�E�X�z�C�[���ŃA�C�e���I��
        if (Input.mouseScrollDelta.y > 0)
        {
            selectAS.Play();
            selectedItem--;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            selectAS.Play();
            selectedItem++;
        }

        //�I���A�C�e���ԍ����͈͊O�ɂȂ�Ȃ�����
        selectedItem = (selectedItem + Inventory.Items.Count) % Inventory.Items.Count;
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

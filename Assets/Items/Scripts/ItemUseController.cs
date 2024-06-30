using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class ItemUseController : MonoBehaviour
{
    [SerializeField] AudioSource selectAS;
    public GameObject player;
    public GameObject itemBase;
    public float power;
    public float torque;
    public static int selectedItem;
    public TextMeshProUGUI text;
    public float coolTime;

    private void Start()
    {
        ItemObjectCreator.itemBase = itemBase;
    }

    private void Update()
    {
        coolTime -= Time.deltaTime;
        //�A�C�e���g�p����
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl)) && coolTime <= 0)
        {
            var item = Inventory.GetSelectedItem();

            if (item.amount > 0)
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
}

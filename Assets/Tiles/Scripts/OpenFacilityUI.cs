using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Object/TileClickEvents/OpenFacilityUI")]
public class OpenFacilityUI : TileClickEvent
{
    [SerializeField] GameObject UIPref;
    [SerializeField] GameObject canvasUI;
    [SerializeField] AudioClip openSE, closeSE;
    [SerializeField] float openSEVolume, closeSEVolume;
    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;


    public override void Enter(TileClickController obj, Vector3Int cellPos, TileObject tileObj)
    {
        graphicRaycaster = canvasUI.GetComponent<GraphicRaycaster>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        Setup(obj, cellPos, UIPref);
        var creator = uiInstance.GetComponentInChildren<RecipeUICreator>();
        creator.CreateTransformUI(tileObj.Tile);
        obj.audioSource.PlayOneShot(openSE, openSEVolume);
    }

    public override void UpdateEvent(TileClickController obj, Vector3Int cellPos)
    {
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(cellPos + Vector3.right);

        //�Ȃ�炩��UI���J���Ă����Ԃ�UI�ȊO���N���b�N����ƕ���
        //UI���J���ĂȂ���Ԃ�UI�������Ă���^�C�����N���b�N�����UI���J��

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI(Input.mousePosition))
        {
            Exit(obj);
        }
    }

    public override void Exit(TileClickController obj)
    {
        DestroyUI(uiInstance);
        obj.ActiveTileObject = null;
        obj.audioSource.PlayOneShot(closeSE, closeSEVolume);
    }

    public void Setup(TileClickController obj, Vector3Int cellPos, GameObject UIPref)
    {
        uiInstance = CreateUI(UIPref);
        uiInstance.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(cellPos + Vector3.right);
    }

    /// <summary>
    /// Canvas���쐬���A���̎q�Ƃ���ui���쐬����
    /// </summary>
    /// <param name="ui"></param>
    /// <returns></returns>
    public GameObject CreateUI(GameObject ui)
    {
        //����UI��p��Canvas���쐬����
        var canvas = Instantiate(canvasUI);
        canvas.name = ui.name + "Canvas";

        //var canvas = GameObject.Find("Canvas").GetComponent<Canvas>(); //���C����Canvas�ɓ��ꂽ���ꍇ������

        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        var obj = Instantiate(ui, canvas.transform);
        return obj;
    }

    public void DestroyUI(GameObject ui)
    {
        Destroy(ui.transform.parent.gameObject);
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

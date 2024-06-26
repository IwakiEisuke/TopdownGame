using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewItem", menuName = "Object/New Item")]
public class InventoryItemData : ScriptableObject
{
    [HideInInspector] public int ID, amount;
    public Sprite sprite;
    public Sprite UISprite;
    public TileBase[] itemTiles;
    public bool isThrowable;
    public Recipe[] recipes;
    public TransformRecipe[] recipesTransform;
    public ItemStatus status;
    public GameObject light;
    public ItemActionBase[] skills;
}

[Serializable]
public class ItemStatus
{
    public int atk;
    public float mass = 1;
    public float power = 10;
    public float torque = 1;
    public float linearDrag = 3;
    public float angularDrag = 3;
}

[Serializable]
public class Recipe : IEnumerable<ConsumeItemSetting>
{
    public ConsumeItemSetting[] requireItems;

    // IEnumerable<ConsumeItemSetting>��GetEnumerator������
    public IEnumerator<ConsumeItemSetting> GetEnumerator()
    {
        // requireItems�z��̗v�f��񋓂���
        foreach (var item in requireItems)
        {
            yield return item;
        }
    }

    // ��W�F�l���b�N��IEnumerator��GetEnumerator������
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

[Serializable]
public class TransformRecipe : Recipe
{
    public TileBase[] requireFacilities;
}

[Serializable]
public class ConsumeItemSetting
{
    /// <summary>
    /// �v���A�C�e��
    /// </summary>
    public InventoryItemData item;
    /// <summary>
    /// �v����
    /// </summary>
    public int amount;
}
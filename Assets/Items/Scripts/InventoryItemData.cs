using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewItem", menuName = "Object/New Item")]
public class InventoryItemData : ScriptableObject
{
    [HideInInspector] public int ID, amount;
    public Sprite sprite;
    public Sprite UISprite;
    public TileBase[] itemTiles;
    public Recipe[] recipes;
    public TransformRecipe[] recipesTransform;
    public ItemStatus status;
    public GameObject light;
    public ItemActionBase[] skills;
    public AudioClip pickupSE;
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

    /// <summary>
    /// �v�����ꂽ�A�C�e���������Ă���ꍇ����Aact�����s���܂�
    /// </summary>
    /// <param name="recipe"></param>
    /// <param name="act"></param>
    public static void RecipeProcess(Recipe recipe, Action act)
    {
        if (IsCreatable(recipe))
        {
            ConsumeItems(recipe);
            act();
        }
    }

    /// <summary>
    /// �v�����ꂽ�A�C�e���������Ă��邩�`�F�b�N���������܂�
    /// </summary>
    /// <param name="recipe"></param>
    public static void RecipeProcess(Recipe recipe)
    {
        if (IsCreatable(recipe))
        {
            ConsumeItems(recipe);
        }
    }

    /// <summary>
    /// Recipe�ŗv�����ꂽ�A�C�e���������B�����������O�Ƀ`�F�b�N����ꍇIsCreatable���\�b�h���K�v�B
    /// </summary>
    /// <param name="recipe"></param>
    public static void ConsumeItems(Recipe recipe)
    {
        foreach (var require in recipe)
        {
            require.item.amount -= require.amount;
        }
    }

    /// <summary>
    /// �v���A�C�e����S�Ď����Ă��邩�`�F�b�N����
    /// </summary>
    /// <param name="recipe"></param>
    /// <returns></returns>
    public static bool IsCreatable(Recipe recipe)
    {
        if (recipe == null)
        {
            return false;
        }
        else
        {
            foreach (var require in recipe)
            {
                //require.item.amount��item�̏������Brequire.amount�͗v�����B
                if (require.item.amount < require.amount)
                {
                    return false;
                }
            }
            return true;
        }
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
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntity", menuName = "Object/New Entity")]
public class EntityData : ScriptableObject
{
    public GameObject entityBase;
    public new string name;
    public Sprite sprite;
    public float hp;
    public float atk;
    public float def;
    public DropItemSetting[] drops;
    public AIBase AI;

    public GameObject CreateEntityInstance(Vector3 pos)
    {
        var obj = Instantiate(entityBase, pos, Quaternion.identity);
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
        obj.GetComponent<EntityController>().entityData = this;
        return obj;
    }
}

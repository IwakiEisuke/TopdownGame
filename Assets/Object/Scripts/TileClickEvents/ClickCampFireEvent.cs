using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

[CreateAssetMenu(menuName = "Object/Click Events/ClickCampFireEvent")]
public class ClickCampFireEvent : TileOnClickEvent
{
    public override void OpenEx()
    {
        var creator = uiInstance.GetComponentInChildren<RecipeUICreator>();
        creator.CreateTransformUI(Inventory.Items);
    }
}

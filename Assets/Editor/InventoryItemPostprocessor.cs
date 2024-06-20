using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class InventoryItemPostprocessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string assetPath in importedAssets)
        {
            // インポートされたアセットがInventoryItemかどうかを確認
            InventoryItemData inventoryItem = AssetDatabase.LoadAssetAtPath<InventoryItemData>(assetPath);
            if (inventoryItem != null)
            {
                // アセットのファイル名を取得
                string fileName = Path.GetFileNameWithoutExtension(assetPath);

                // InventoryItemのitemName変数にファイル名を格納
                if (inventoryItem.name != fileName)
                {
                    inventoryItem.name = fileName;

                    //var i = 1;
                    //foreach (var tile in inventoryItem.itemTiles)
                    //{
                    //    var tileBase_path = AssetDatabase.GetAssetPath(tile);

                    //    if (inventoryItem.itemTiles.Length > 1)
                    //    {
                    //        AssetDatabase.RenameAsset(tileBase_path, inventoryItem.name.ToLower() + " " + i);
                    //        i++;
                    //    }
                    //    else
                    //    {
                    //        AssetDatabase.RenameAsset(tileBase_path, inventoryItem.name.ToLower());
                    //    }
                    //}

                    EditorUtility.SetDirty(inventoryItem);
                }
            }
        }

        // 変更を保存
        AssetDatabase.SaveAssets();
    }
}


[CustomEditor(typeof(InventoryItemData))]
public class InventoryItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InventoryItemData inventoryItem = (InventoryItemData)target;

        if (GUILayout.Button("ファイル名で名前を更新する"))
        {
            string path = AssetDatabase.GetAssetPath(inventoryItem);
            string fileName = Path.GetFileNameWithoutExtension(path);

            if (inventoryItem.name != fileName)
            {
                inventoryItem.name = fileName;
                EditorUtility.SetDirty(inventoryItem);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
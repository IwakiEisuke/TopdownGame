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
            // �C���|�[�g���ꂽ�A�Z�b�g��InventoryItem���ǂ������m�F
            InventoryItemData inventoryItem = AssetDatabase.LoadAssetAtPath<InventoryItemData>(assetPath);
            if (inventoryItem != null)
            {
                // �A�Z�b�g�̃t�@�C�������擾
                string fileName = Path.GetFileNameWithoutExtension(assetPath);

                // InventoryItem��itemName�ϐ��Ƀt�@�C�������i�[
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

        // �ύX��ۑ�
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

        if (GUILayout.Button("�t�@�C�����Ŗ��O���X�V����"))
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
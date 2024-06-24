using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "GenerationAlgorithm/Object/Dungeon")]
public class O_Dungeon : ObjectGenerationAlgorithm
{
    [SerializeField] int vertexCount;
    [SerializeField] float lineWidth;
    [SerializeField] Vector2 step;

    public override Tilemap Algorithm(MapEnvironment env, int mapIndex, ref Tilemap refmap)
    {
        var map = refmap;
        var currentMapStairsPos = MapManager.GetCurStairsPos(mapIndex); //��������}�b�v�ɂ���K�i���擾
        //��������ʘH�̒��_�i�Ȃ���p�̐��j
        var count = vertexCount + currentMapStairsPos.Count;
        Vertice[] vertices = new Vertice[count];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vertice(i); //���_�𐶐����A��������id������U��
        }

        //�K�i�𒸓_�ɓ����B
        var startStairIndex = Random.Range(0, count);
        vertices[startStairIndex].Set(currentMapStairsPos[0]);
        //2�ڂ̊K�i�ɒ��_��ݒ�B
        if (currentMapStairsPos.Count == 2)
        {
            for (int i = 0; i < 10; i++)
            {
                var random = Random.Range(0, count);
                if (!vertices[random].AlreadySet)
                {
                    vertices[random].Set(currentMapStairsPos[1]);
                    break;
                }
            }
        }

        //���_�𐶐�����͈͂̌���̂��߂Ƀ}�b�v�͈͂��擾
        var mapbounds = env._mapAlgo._mapBounds;
        var shrinkedBounds = (Vector2)mapbounds * 0.8f;
        var s = shrinkedBounds * 2 / step;

        int rx = 0;
        int ry = 0;
        int loopCount = 0;
        //�����_���Ȉʒu�ɒ��_���쐬
        for (int i = 0; i < count; i++)
        {
            if (i < count)
            {
                if (!vertices[i].AlreadySet)
                {
                    //�i�s����
                    int xdir = Mathf.Round(Random.value) == 1 ? 1 : -1;
                    int ydir = Mathf.Round(Random.value) == 1 ? 1 : -1;

                    //�͈͓���10�}�X�������_���Ɉړ�
                    rx = (int)Mathf.Clamp(rx + s.x * xdir, -shrinkedBounds.x, shrinkedBounds.x);
                    ry = (int)Mathf.Clamp(ry + s.y * ydir, -shrinkedBounds.y, shrinkedBounds.y);
                    var pos = new Vector2(rx, ry);

                    var isAlreadySetPos = false;
                    foreach (var v in vertices)
                    {
                        if (v.AlreadySet && v.Pos == pos)
                        {
                            isAlreadySetPos = true;
                        }
                    }

                    if (!isAlreadySetPos)
                    {
                        vertices[i].Set(pos);
                    }
                    else
                    {
                        var loops = step.x * step.x * step.y * step.y;
                        if (loopCount > loops)
                        {
                            Debug.LogAssertion("O_Dungeon�̒��_�������Ɋ��ɒ��_�����݂���ʒu��" + loops + "��ȏ�I�΂ꂽ���ߐ������~���܂���");
                            break;
                        }
                        loopCount++;
                        i--;
                        continue;
                    }
                }
            }
        }

        //�h���l�[�O�p�`�����ŃO���t�𐶐�������A�ŏ��S��؂���ʘH�𐶐�
        var paths = MinimumSpanningTree.CreateTree(vertices);

        //�}�b�v�S�̂�ǂɂ���
        map.Fill(mapbounds, _tileSettings[0]._tile);

        //�u���[���n���̃A���S���Y�����g�p���ă^�C���}�b�v�̒ʘH����������
        foreach (var path in paths)
        {
            var v1 = Vector2Int.FloorToInt(path.start.Pos);
            var v2 = Vector2Int.FloorToInt(path.end.Pos);
            foreach (var pos in Bresenham.GetLineTiles(v1, v2))
            {
                map.SetTileNullInLine(lineWidth, pos);
            }
        }

        refmap = map;
        return refmap;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLogManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1;
        Debug.Log(
            "Todo:\n" +
            
            "�����̐ݒ�������Ƃ�ScriptableObject����ݒ�ł���悤�ɂ���\n" +
            "�G�i�����j��������B�v���C���[�Ɍ������Ă���悤�ɂ���B�U�����[�V�������s���A����̃^�C�~���O�ŐG���ƃv���C���[�̓_���[�W���󂯂�\n" +
            "�����i�����j��������B���̏�������_���ɓ������B�\�ł���΃_���[�W���󂯂�ƍU���������̂��瓦����悤�ɂ���i�G���U������\�������邽�ߒN���U�����������ʁj\n" +
            "�A�C�e���̒ǉ��F���΁i��Ɏ������A�C�e�����Ă��B���V�s�̒ǉ��j�@���i���΂ŏĂ���j�@�Ă������i�̗͂��񕜂���j\n" +
            "���A�����̃����_�������B�����̓��A�o�����̊Ԃ��Ȃ��悤�ɂ���Ƃ悳����\n" +
            "�ǂ����������̂Ő������ǂɂ߂荞�܂Ȃ��悤�ɂ���\n" +
            "�A�C�e������UI�̍쐬�B��A�C�e���ɕ����̃��V�s������ꍇ��z��B�i�C�ӁF�A�C�e��ID�܂��̓��V�sID��ǉ����\�[�g����j\n");

        Debug.Log(
            "�C��:\n" +
            "�A�C�e���𓊂������Ɠ��A���o���肷��Ƃ��̏�Œ�~����̂�h���i�Q�[���I�u�W�F�N�g�̃A�N�e�B�u��Ԃ��؂�ĉ����x�����ł��邽�߁j\n" +
            "�I�u�W�F�N�g�̃A�N�e�B�u��Ԃł͂Ȃ��\���݂̂��I���I�t����K�v������\n" +
            "�A�C�e�����E�����ۂɒu������^�C�����e�X�ݒ�ł���悤�ɂ���i�n��ƈȉ��Ŕw�i���قȂ�Ȃǂ̏󋵂ł���Γ���̏����ōs�������j\n");
    }
}

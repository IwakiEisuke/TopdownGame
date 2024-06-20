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
            
            "環境光の設定を環境ごとのScriptableObjectから設定できるようにする\n" +
            "敵（生物）を一つ実装。プレイヤーに向かってくるようにする。攻撃モーションを行い、特定のタイミングで触れるとプレイヤーはダメージを受ける\n" +
            "動物（生物）を一つ実装。その場をランダムに動き回る。可能であればダメージを受けると攻撃したものから逃げるようにする（敵が攻撃する可能性もあるため誰が攻撃したか判別）\n" +
            "アイテムの追加：焚火（手に持ったアイテムを焼く。レシピの追加）　肉（焚火で焼ける）　焼いた肉（体力を回復する）\n" +
            "洞窟内部のランダム生成。複数個の洞窟出入口の間をつなぐようにするとよさそう\n" +
            "壁が生成されるので生物が壁にめり込まないようにする\n" +
            "アイテム製作UIの作成。一アイテムに複数のレシピがある場合を想定。（任意：アイテムIDまたはレシピIDを追加しソートする）\n");

        Debug.Log(
            "任意:\n" +
            "アイテムを投げたあと洞窟を出入りするとその場で停止するのを防ぐ（ゲームオブジェクトのアクティブ状態が切れて加速度が消滅するため）\n" +
            "オブジェクトのアクティブ状態ではなく表示のみをオンオフする必要がある\n" +
            "アイテムを拾った際に置換するタイルを各々設定できるようにする（地上と以下で背景が異なるなどの状況であれば同一の処理で行いたい）\n");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 槍の駒
public class Spear : Piece
{
    public override BattleJudges judgeOppositePiece(Piece oppositePiece)
    {
        // ヘビーアクスを持っていたら必ず勝つ
        if (this.EquippingItem != null && this.EquippingItem.CurrentItemTypes == ItemTypes.Axe)
        {
            return BattleJudges.Win;
        }
        // 相手がヘビーアクスを持っていたら必ず負ける
        if (oppositePiece.EquippingItem != null && oppositePiece.EquippingItem.CurrentItemTypes == ItemTypes.Axe)
        {
            return BattleJudges.Lose;
        }
        if (oppositePiece is Spear)
        {
            return BattleJudges.Draw;
        }
        else if (oppositePiece is Sword)
        {
            return BattleJudges.Win;
        }
        else
        {
            return BattleJudges.Lose;
        }
    }
}

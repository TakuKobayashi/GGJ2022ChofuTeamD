using UnityEngine;
using System.Collections;

// 斧の駒
public class Axe : Piece
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
        if (oppositePiece is Axe)
        {
            return BattleJudges.Draw;
        }
        else if(oppositePiece is Spear)
        {
            return BattleJudges.Win;
        }
        else
        {
            return BattleJudges.Lose;
        }
    }
}

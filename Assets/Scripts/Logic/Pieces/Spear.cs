using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 槍の駒
public class Spear : Piece
{
    public override BattleJudges judgeOppositePiece(Piece oppositePiece)
    {
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

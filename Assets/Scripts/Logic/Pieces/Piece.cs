using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 駒の基底クラス
public abstract class Piece : MonoBehaviour
{
    // その駒がそもそも移動可能なマス目の一覧
    // 例1: 右に一マス移動可能な場合はx: 1,y: 0と入力
    // 例2: 左に一マス移動可能な場合はx: -1,y: 0と入力
    // 例3: 上に一マス移動可能な場合はx: 0,y: 1と入力
    // 例4: 下に一マス移動可能な場合はx: 0,y: -1と入力 
    [SerializeField] protected List<BoardPosition> defaultMovableBoardPositions;

    protected Player owner;
	public Item EquippingItem { private set; get; }

    public BoardPosition CurrentPosition { private set; get; }

    public void Initialize(Player owner, int positionX, int positionY)
    {
        this.CurrentPosition = new BoardPosition(positionX, positionY);
        this.owner = owner;
    }

    public List<Square> FilterMovableSquares(List<Square> allSquares)
    {
        List<Piece> myPieses = this.owner.CurrentPieces;
        Player oppositePlayer = GameController.Instance.OppositePlayer(this.owner);
        List<Square> pieceMovableSquares = new List<Square>();

		List<BoardPosition> movableBoardPosirtions = new List<BoardPosition>(defaultMovableBoardPositions);
		foreach (BoardPosition boardPosition in movableBoardPosirtions)
        {
            int willMoveX = CurrentPosition.x + boardPosition.x;
            int willMoveY = CurrentPosition.y + boardPosition.y;

            // 移動したい先に自分のコマがある時は移動可能なマスに含めない
            if (myPieses.Exists(piece => piece.CurrentPosition.x == willMoveX && piece.CurrentPosition.y == willMoveY)){
                continue;
            }
            // 移動先の相手とやり合っても引き分ける時は移動できない
            Piece battlePiece = oppositePlayer.CurrentPieces.Find(piece => piece.CurrentPosition.x == willMoveX && piece.CurrentPosition.y == willMoveY);
            if(battlePiece != null && this.judgeOppositePiece(battlePiece) == BattleJudges.Draw)
            {
                continue;
            }
            // マスとして存在するもののみを絞り込む
            Square willMovableSquare = allSquares.Find(square => square.boardPosition.x == willMoveX && square.boardPosition.y == willMoveY);
            if(willMovableSquare != null)
            {
                pieceMovableSquares.Add(willMovableSquare);
            }
        }
        return pieceMovableSquares;
    }

    abstract public BattleJudges judgeOppositePiece(Piece oppositePiece);

    public void Move(Square moveToSquare)
    {
        this.transform.parent = moveToSquare.transform;
        Vector3 prevLocalScale = this.transform.localScale;
        ComponentUtil.Normalize(this.transform);
        this.transform.localScale = prevLocalScale;
        this.CurrentPosition = moveToSquare.boardPosition;

		moveToSquare.CaptureItem(this);
		this.executeBattleResult(moveToSquare);
    }

    public void EquipItem(Item item)
	{
		item.transform.parent = this.transform;
		item.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		this.EquippingItem = item;
	}

	public void LostItem()
	{
		if (this.EquippingItem != null)
		{
			GameObject.Destroy(this.EquippingItem.gameObject);
		}
		this.EquippingItem = null;
	}

	private void executeBattleResult(Square willMoveToSquare)
    {
        Player oppositePlayer = GameController.Instance.OppositePlayer(this.owner);
        // 移動先の相手とやり合っても引き分ける時は移動できない
        Piece battlePiece = oppositePlayer.CurrentPieces.Find(piece => piece.CurrentPosition.x == willMoveToSquare.boardPosition.x && piece.CurrentPosition.y == willMoveToSquare.boardPosition.y);
		if (battlePiece != null)
		{
			SePlayManager.PlaySeSound(SePlayManager.SE.attack);
			BattleJudges battleJudge = this.judgeOppositePiece(battlePiece);
			if (battleJudge == BattleJudges.Win)
			{
				if (battlePiece.EquippingItem != null)
				{
					// 爆弾持っていたら共倒れ
					if (battlePiece.EquippingItem.CurrentItemTypes == ItemTypes.Bomb)
					{
						this.owner.LostPiece(this);
					}
					// 白旗を持っていたら寝返る
					else if (battlePiece.EquippingItem.CurrentItemTypes == ItemTypes.Flag)
					{
						//this.owner.SpawnPiece(, battlePiece.gameObject, false);
					}
					// 盾を持っていたら死なないでノックバック
					else if (battlePiece.EquippingItem.CurrentItemTypes == ItemTypes.Shield)
					{
						// return;
					}
					// 棺桶を持っていたら死なないでそこらへんに行く
					else if (battlePiece.EquippingItem.CurrentItemTypes == ItemTypes.Coffin)
					{
						// return;
					}
				}
				oppositePlayer.LostPiece(battlePiece);
				if (this.EquippingItem != null && this.EquippingItem.CurrentItemTypes == ItemTypes.Axe)
				{
					this.LostItem();
				}
			}
			else if (battleJudge == BattleJudges.Lose)
			{
				// 爆弾持っていたら共倒れ
				if (this.EquippingItem != null)
				{
					// 爆弾持っていたら共倒れ
					if (this.EquippingItem.CurrentItemTypes == ItemTypes.Bomb)
					{
						oppositePlayer.LostPiece(battlePiece);
					}
					// 白旗を持っていたら寝返る
					else if (this.EquippingItem.CurrentItemTypes == ItemTypes.Flag)
					{
						//oppositePlayer.SpawnPiece(, battlePiece.gameObject, false);
					}
					// 盾を持っていたら死なないでノックバック
					else if (this.EquippingItem.CurrentItemTypes == ItemTypes.Shield)
					{
						// return;
					}
					// 棺桶を持っていたら死なないでそこらへんに行く
					else if (this.EquippingItem.CurrentItemTypes == ItemTypes.Coffin)
					{
						// return;
					}
				}
				this.owner.LostPiece(this);
				if (battlePiece.EquippingItem != null && battlePiece.EquippingItem.CurrentItemTypes == ItemTypes.Axe)
				{
					battlePiece.LostItem();
				}

			}
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    // 盤面の横幅のマス数(とりあえず8マス)
    [SerializeField] private int boardWidth;
    // 盤面の縦幅のマス数(とりあえず10マス)
    [SerializeField] private int boardHeight;

    [SerializeField] private List<BoardPosition> territoryPositions;

    [SerializeField] private GridLayoutGroup boardGridLayoutGroup;
    [SerializeField] private GameObject gridSquareObject;

    // 各駒の初期配置の位置を定義
    [SerializeField] private List<PiecePosition> defaultPieces;

    private List<Square> allGridSquares = new List<Square>();
    private List<Piece> pieces = new List<Piece>();

    private Square selectingSquare = null;

    private void GenerateBoardGrid()
    {
        for(int i = 0;i < boardHeight; ++i)
        {
            for (int j = 0; j < boardWidth; ++j)
            {
                Square gridSquare = ComponentUtil.InstantiateTo<Square>(boardGridLayoutGroup.gameObject, gridSquareObject);
                gridSquare.Initialize(j, i, OnSquareClicked);
                allGridSquares.Add(gridSquare);
            }
        }
    }

    private void SpawnPieaces()
    {
        foreach(PiecePosition piecePosition in defaultPieces)
        {
            Square gridSquare = allGridSquares.Find(g => g.boardPosition.x == piecePosition.position.x && g.boardPosition.y == piecePosition.position.y);
            if(gridSquare != null)
            {
                Piece piece = ComponentUtil.InstantiateTo<Piece>(gridSquare.gameObject, piecePosition.pieceObj.gameObject);
                piece.Initialize(gridSquare.boardPosition.x, gridSquare.boardPosition.y);
                pieces.Add(piece);
            }
        }
    }

    // マス目を全て初期状態にする
	private void ChangeAllSquareNormalState()
	{
		this.selectingSquare = null;
		foreach (Square movableSquare in allGridSquares)
		{
			movableSquare.ChangeStateWithGraphic(SquareState.Normal);
		}
	}

	private void OnSquareClicked(Square clickedSquare)
    {
        Debug.Log(string.Format("{0},{1}", clickedSquare.boardPosition.x, clickedSquare.boardPosition.y));
        // 移動させようとした駒を選択したら、選択解除してクリアにする
        if(clickedSquare.CurrentState == SquareState.Selecting)
		{
			this.ChangeAllSquareNormalState();
			return;
		}
        if(this.selectingSquare == null)
		{
			Piece clickedPiece = pieces.Find(p => p.CurrentPosition.x == clickedSquare.boardPosition.x && p.CurrentPosition.y == clickedSquare.boardPosition.y);
			if (clickedPiece != null)
			{
				clickedSquare.ChangeStateWithGraphic(SquareState.Selecting);
				this.selectingSquare = clickedSquare;
				List<Square> movableSquares = clickedPiece.FilterMovableSquares(allGridSquares, pieces);
				foreach (Square movableSquare in movableSquares)
				{
					movableSquare.ChangeStateWithGraphic(SquareState.Movable);
				}
			}
		}
		else
		{
            if(clickedSquare.CurrentState == SquareState.Movable || clickedSquare.CurrentState == SquareState.MovableHovering)
			{
                // TODO: 駒の移動処理
			}
			else if(clickedSquare.CurrentState == SquareState.Normal || clickedSquare.CurrentState == SquareState.Hovering)
			{
                // 駒の選択中に移動可能範囲外を選択したら洗濯を全てきれいにする
				this.ChangeAllSquareNormalState();
				return;
			}
		}
	}

    private void Awake()
    {
        GenerateBoardGrid();
        SpawnPieaces();
		ChangeAllSquareNormalState();
	}
}

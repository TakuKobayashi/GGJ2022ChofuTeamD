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

    [SerializeField] private GridLayoutGroup boardGridLayoutGroup;
    [SerializeField] private GameObject gridSquareObject;
    [SerializeField] private GameObject territoryGridSquareObject;

    [SerializeField] private SpawnItemRange spawnItemRange;
    [SerializeField] private List<Item> candidateDropItems;

    private Square selectingSquare = null;
    private List<Square> allGridSquares = new List<Square>();

    private void GenerateBoardGrid()
    {
        for(int i = 0;i < boardHeight; ++i)
        {
            for (int j = 0; j < boardWidth; ++j)
            {
                Square gridSquare;
                if (GameController.Instance.CurrentTurnPlayer.CheckTerritoryPosition(j, i) || GameController.Instance.OppositeTurnPlayer.CheckTerritoryPosition(j, i))
                {
                    gridSquare = ComponentUtil.InstantiateTo<Square>(boardGridLayoutGroup.gameObject, territoryGridSquareObject);
                }
                else
                {
                    gridSquare = ComponentUtil.InstantiateTo<Square>(boardGridLayoutGroup.gameObject, gridSquareObject);
                }
                gridSquare.Initialize(j, i, OnSquareClicked);
                allGridSquares.Add(gridSquare);
            }
        }
    }

    private void LotSpawnItems()
    {
		int spawaItemCount = Random.Range(spawnItemRange.SpwanCountMin, spawnItemRange.SpwanCountMax);
		List<Square> candidateSquares = allGridSquares.FindAll(square => !(square is TerritorySquare));
        for(int i = 0;i < spawaItemCount; ++i)
		{
			int dropSquareIndex = Random.Range(0, (candidateSquares.Count - 1));
			int dropItemIndex = Random.Range(0, (candidateDropItems.Count - 1));
			candidateSquares[dropSquareIndex].DropItem(candidateDropItems[dropItemIndex]);
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
        // 移動させようとした駒を選択したら、選択解除してクリアにする
        if(clickedSquare.CurrentState == SquareState.Selecting)
		{
			this.ChangeAllSquareNormalState();
			return;
		}
        Player currentTurnPlayer = GameController.Instance.CurrentTurnPlayer;
        if (this.selectingSquare == null)
		{
            Piece clickedPiece = currentTurnPlayer.CurrentPieces.Find(p => p.CurrentPosition.x == clickedSquare.boardPosition.x && p.CurrentPosition.y == clickedSquare.boardPosition.y);
			if (clickedPiece != null)
			{
				clickedSquare.ChangeStateWithGraphic(SquareState.Selecting);
                this.selectingSquare = clickedSquare;
				List<Square> movableSquares = clickedPiece.FilterMovableSquares(allGridSquares);
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
                BoardPosition selectingSquareBoardPosition = this.selectingSquare.boardPosition;
                Piece movePiece = currentTurnPlayer.CurrentPieces.Find(p => p.CurrentPosition.x == selectingSquareBoardPosition.x && p.CurrentPosition.y == selectingSquareBoardPosition.y);
                movePiece.Move(clickedSquare);
				this.ChangeAllSquareNormalState();
                // 勝利判定からのTurnの切り替え処理をここで行う
                GameController.Instance.Judge();
            }
		}
	}

    private void Awake()
    {
        GameController.Instance.LotFirstPlayerTurn();
        GenerateBoardGrid();
        LotSpawnItems();
        GameController.Instance.SpawnPieaces(allGridSquares);
		ChangeAllSquareNormalState();
    }
}

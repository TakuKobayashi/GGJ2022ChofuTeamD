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

    private List<Square> gridSquares = new List<Square>();
    private List<Piece> pieces = new List<Piece>();

    private void GenerateBoardGrid()
    {
        for(int i = 0;i < boardHeight; ++i)
        {
            for (int j = 0; j < boardWidth; ++j)
            {
                Square gridSquare = ComponentUtil.InstantiateTo<Square>(boardGridLayoutGroup.gameObject, gridSquareObject);
                gridSquare.Initialize(j, i);
                gridSquares.Add(gridSquare);
            }
        }
    }

    private void SpawnPieaces()
    {
        foreach(PiecePosition piecePosition in defaultPieces)
        {
            Square gridSquare = gridSquares.Find(g => g.boardPosition.x == piecePosition.position.x && g.boardPosition.y == piecePosition.position.y);
            if(gridSquare != null)
            {
                Piece piece = ComponentUtil.InstantiateTo<Piece>(gridSquare.gameObject, piecePosition.pieceObj.gameObject);
                piece.Initialize(gridSquare.boardPosition.x, gridSquare.boardPosition.y);
                pieces.Add(piece);
            }
        }
    }

    private void Awake()
    {
        GenerateBoardGrid();
        SpawnPieaces();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

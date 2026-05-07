using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridCells : MonoBehaviour
{

    public int x, y;
    public DraggableRank currentRank;
    public SpriteRenderer cellRenderers;

    private void Awake()
    {
        cellRenderers = GetComponent<SpriteRenderer>();

    }


    public void Initialize(int gridX, int gridY)
    {
        x = gridX;
        y = gridY;
        name = "Cell_" + x + "_" + y;           //이름 설정
    }
    

    public bool isEmpty()
    {
        return currentRank == null;             //비어있으면 true 아니면 false
    }

    public bool ContainsPosition(Vector3 position)
    {
        Bounds bounds = cellRenderers.bounds;
        return bounds.Contains(position);
    }


    public void SetRank(DraggableRank rank)
    {
        currentRank = rank;

        if(rank != null)
        {
            rank.currentCell = this;
        }

        rank.originalPosition = new Vector3(transform.position.x, transform.position.y, 0);     //z 위치 0으로 초기화
        rank.transform.position = new Vector3(transform.position.x, transform.position.y, 0);   //계급장 현재 칸 위치로 이동

    }
}

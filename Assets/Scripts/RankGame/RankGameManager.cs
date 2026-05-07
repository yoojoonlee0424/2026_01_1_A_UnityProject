
using UnityEngine;
using System.Collections.Generic;

public class RankGameManager : MonoBehaviour
{
    public int gridWidth = 7;
    public int gridHeight = 7;
    public float CellSize = 1.3f;
    public GameObject cellPrefabs;
    public Transform gridContainer;

    public GameObject rankPrefabs;
    public Sprite[] rankSprites;
    public int maxRankLevel = 7;

    public GridCells[,] grid;

    void initializeGrid()
    {
        grid = new GridCells[gridWidth, gridHeight];


        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(
                    x * CellSize - (gridWidth * CellSize / 2) + CellSize / 2,
                    y * CellSize - (gridHeight * CellSize / 2) + CellSize / 2,
                    1f);


                GameObject cell0bj = Instantiate(cellPrefabs, position, Quaternion.identity, gridContainer);
                GridCells cell = cell0bj.AddComponent<GridCells>();
                cell.Initialize(x, y);

                grid[x, y] = cell;
            }
        }

            

        
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initializeGrid();

        for (int i = 0; i <4; i++)
        {
            SpawnNewRank();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            SpawnNewRank();
        }
    }


    public DraggableRank CreateRankInCell(GridCells cell, int level)
    {
        if (cell == null || !cell.isEmpty())
        {
            return null;
        }

        level = Mathf.Clamp(level, 1, maxRankLevel);

        Vector3 rankPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, 0f);

        GameObject rankObj = Instantiate(rankPrefabs, rankPosition, Quaternion.identity, gridContainer);
        rankObj.name = "Rank_Level_" + level;

        DraggableRank rank = rankObj.AddComponent<DraggableRank>();

        rank.SetRankLevel(level);

        cell.SetRank(rank);

        return rank;
    }


    public GridCells FineEmptyCell()
    {
        List<GridCells> empltyCells = new List<GridCells>();

        for(int x = 0; x < gridWidth; x++)
        {
            for(int y = 0; y < gridHeight; y++)
            {
                if(grid[x, y].isEmpty())
                {
                    empltyCells.Add(grid[x, y]);
                }


            }


        }

        if(empltyCells.Count == 0)
        {
            return null ;
        }
        
        return empltyCells[Random.Range(0, empltyCells.Count)];

    }

    public GridCells FindClosestCell(Vector3 position)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y].ContainsPosition(position))
                {
                    return grid[x, y];
                }
            }

        }

        GridCells closestCell = null;
        float closestDistance = float.MaxValue;


        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float distance = Vector3.Distance(position, grid[x, y].transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCell = grid[x, y];

                }

            }

        }

        if (closestDistance > CellSize * 2)
        {
            return null;
        }

        return closestCell;



    }







    public bool SpawnNewRank()
    {
        GridCells emptyCell = FineEmptyCell();
        if(emptyCell == null)
        {
            return false;
        }

        int rankLevel = Random.Range(0, 100) < 80 ? 1 : 2 ; //80% 레벨1 ,20% 레벨 2

        CreateRankInCell(emptyCell, rankLevel);

        return true;
    }


    public void RemoveRank(DraggableRank rank)
    {
        if(rank == null)
        {
            return;
        }

        if (rank.currentCell != null)
        {
            rank.currentCell.currentRank = null;
        }

        Destroy(rank.gameObject);
    }

    public void MergeRanks(DraggableRank draggedRank, DraggableRank targetRank)
    {
        if(draggedRank == null || targetRank == null || draggedRank.rankLevel != targetRank.rankLevel)
        {
            if(draggedRank != null) draggedRank.ReturnToOriginalPosition();
            return;
        }

        int newLevel = targetRank.rankLevel + 1;
        if(newLevel >  maxRankLevel)
        {
            RemoveRank(draggedRank);
            return;
        }


        targetRank.SetRankLevel(newLevel);
        RemoveRank(draggedRank);

    }




}

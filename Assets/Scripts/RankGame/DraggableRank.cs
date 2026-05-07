using UnityEngine;

public class DraggableRank : MonoBehaviour
{

    public int rankLevel = 1;
    public float dragSpeed = 10f;
    public float snapBackSpeed = 20f;

    public bool isDragging = false;

    
    public Vector3 originalPosition;
    public GridCells currentCell;

    public Camera mainCamera;
    public Vector3 dragOffset;
    public SpriteRenderer spriteRenderer;

    public RankGameManager gameManager;


    private void Awake()
    {
        mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindAnyObjectByType<RankGameManager>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging)
        {
            Vector3 targetPosition = GetMouseWorldPosition() + dragOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, dragSpeed* Time.deltaTime);
        }
        else if(transform.position != originalPosition && currentCell != null)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, snapBackSpeed* Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        StartDragging();
    }

    private void OnMouseUp()
    {
        if(!isDragging)
        {
            return;
        }
        StopDragging();
    }


    void StartDragging()
    {
        isDragging = true;
        dragOffset = transform.position - GetMouseWorldPosition();
        spriteRenderer.sortingOrder = 10;
    }

    void StopDragging()
    {
        isDragging = false;
        spriteRenderer.sortingOrder = 1;
        GridCells targetCell = gameManager.FindClosestCell(transform.position);

        if(targetCell != null )
        {
            if(targetCell.currentRank == null)
            {
                MovetoCell(targetCell);
            }
            else if (targetCell.currentRank != this && targetCell.currentRank.rankLevel == rankLevel)
            {
                MergeWithCell(targetCell);
            }
            else
            {
                ReturnToOriginalPosition();
            }
        }
        else
        {
            ReturnToOriginalPosition();
        }


    }


    public void MovetoCell(GridCells targetCell)
    {
        if (currentCell != null)
        {
            currentCell.currentRank = null;
        }

        currentCell = targetCell;
        targetCell.currentRank = this;

        originalPosition = new Vector3(targetCell.transform.position.x, targetCell.transform.position.y, 0f);
        transform.position = originalPosition;

    }

    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
    }

    public void MergeWithCell(GridCells targetCell)
    {
        if(targetCell.currentRank == null || targetCell.currentRank.rankLevel != rankLevel) //다른 레벨이거나 비어있으면
        {
            ReturnToOriginalPosition();
            return;
        }

        if(currentCell != null)
        {
            currentCell.currentRank = null;
        }

        gameManager.MergeRanks(this, targetCell.currentRank);

    }

    public Vector3 GetMouseWorldPosition()                  //마우스 월드 좌표 구하기
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }

    public void SetRankLevel(int level)
    {
        rankLevel = level;

        if(gameManager != null && gameManager.rankSprites.Length > level -1)
        {
            spriteRenderer.sprite = gameManager.rankSprites[level - 1];
        }
    }

    
}

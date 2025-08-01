//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BoxController : MonoBehaviour
{
    BoxCollider2D m_collider;

    public Vector2Int resolution;
    public GameObject gemPrefab;
    private Gem[,] gemGrid;
    public int minGemCount = 35;

    private void Start()
    {
        gemGrid = new Gem[resolution.x, resolution.y];
        m_collider = GetComponent<BoxCollider2D>();

        GenerateGems();
    }

    private void GenerateGems()
    {
        for (int y = 0; y < resolution.y; y++)
        {
            for (int x = 0; x < resolution.x; x++)
            {
                Vector2 pos = GridToWorldPos(x, y);

                if (Random.Range(0, 3) > 0)
                {
                    gemGrid[x, y] = Instantiate(gemPrefab, pos, Quaternion.identity).GetComponent<Gem>();
                    gemGrid[x, y].transform.localScale = Vector3.one / resolution.x * 5;

                    gemGrid[x, y].positionID = new Vector2Int(x, y);
                    gemGrid[x, y].Type = (Random.Range(0, 5));

                }
            }
        }
        int count = 0;
        foreach(Gem gem in gemGrid)
        {
            if (gem != null)
                count++;
        }

        if (count < minGemCount)
        {
            GenerateGems();
            return;
        }

        collapseGems();
        StartCoroutine(resolveAllMatches(0, true));
        foreach (var gem in gemGrid)
        {
            if(gem != null)
                gem.transform.position = GridToWorldPos(gem.positionID.x, gem.positionID.y);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) collapseGems();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(resolveAllMatches(0));
        }
    }
#endif 
    // Returns true if a collapse occurred
    private void collapseGems()
    {
        bool collapsed;
        do
        {
            collapsed = false;
            for (int y = resolution.y - 2; y >= 0; y--)
            {
                for (int x = 0; x < resolution.x; x++)
                {
                    if (gemGrid[x, y] == null) continue;

                    if (gemGrid[x, y + 1] == null)
                    {
                        gemGrid[x, y + 1] = gemGrid[x, y];
                        gemGrid[x, y] = null;
                        collapsed = true;
                    }
                }
            }
        } while (collapsed);

        updateGemPositions();

    }

    private void updateGemPositions()
    {
        for (int y = 1; y < resolution.y; y++) //exclude first layer (no possibility of moving)
        {
            for (int x = 0; x < resolution.x; x++)
            {
                if (gemGrid[x, y] == null) continue;

                gemGrid[x, y].positionID = new Vector2Int(x, y);
                gemGrid[x, y].GoTo(GridToWorldPos(x, y));
            }
        }
    }
    private IEnumerator resolveAllMatches(float startDelay, bool instant = false)
    {
        if(!instant)
            yield return new WaitForSeconds(startDelay);

        foreach (Vector2Int pos in FindMatchesAll())
        {
            Destroy(gemGrid[pos.x, pos.y].gameObject);
            gemGrid[pos.x, pos.y] = null;

            if (GemMatchManager.Instance.gameOver)
                yield break;

            if (!instant)
            {
                GemMatchManager.Instance.IncreaseScore();
                yield return new WaitForSeconds(0.1f);
            }
        }

        
        collapseGems();
        if(!instant)
            yield return new WaitForSeconds(0.5f);
        canSelect = true;

        if (FindMatchesAll().Count > 0)
        {
            canSelect = false;
            StartCoroutine(resolveAllMatches(0f, instant));
        }
    }

    

    private List<Vector2Int> FindMatchesAll()
    {
        List<Vector2Int> matchPositions = new();


        for (int y = 0; y < resolution.y; y++)
        {
            for (int x = 0; x < resolution.x; x++)
            {
                if (gemGrid[x, y] == null) continue;

                bool exists = false;
                foreach (Vector2Int pos in matchPositions)
                {
                    if (new Vector2Int(x, y) == pos)
                        exists = true;
                }

                if (!exists)
                {
                    List<Vector2Int> newRange = FindMatchesFromPos(x, y);
                    foreach (Vector2Int pos in newRange)
                    {
                        if (!matchPositions.Contains(pos)) // removes duplicates
                        {
                            matchPositions.Add(pos);
                        }
                    }
                }
            }
        }

        return matchPositions;
    }

    private List<Vector2Int> FindMatchesFromPos(int x, int y)
    {
        List<Vector2Int> matchPositions = new();

        int type = gemGrid[x, y].Type;


        matchPositions.AddRange(CheckRange(x, y, 1, 0));
        matchPositions.AddRange(CheckRange(x, y, -1, 0));
        matchPositions.AddRange(CheckRange(x, y, 0, 1));
        matchPositions.AddRange(CheckRange(x, y, 0, -1));

        if (matchPositions.Count > 0) matchPositions.Add(new Vector2Int(x, y));

        return matchPositions;
    }

    //does not add initial item
    private List<Vector2Int> CheckRange(int startX, int startY, int xChange, int yChange)
    {

        List<Vector2Int> matchPosTest = new();
        int x = startX;
        int y = startY;
        int type = gemGrid[x, y].Type;
        while (true)
        {
            x += xChange;
            y += yChange;

            if ((x >= resolution.x | y >= resolution.y | x < 0 | y < 0) || gemGrid[x, y] == null || gemGrid[x, y].Type != type)
            {
                if (System.Math.Abs(x - startX) < 3 && System.Math.Abs(y - startY) < 3) // less than 3 in a row
                    matchPosTest = new();
                break;
            }
            else
            {
                matchPosTest.Add(new Vector2Int(x, y));
            }
        }

        return matchPosTest;
    }

    private Vector2 GridToWorldPos(int x, int y)
    {
        float posX = m_collider.bounds.min.x + (x * m_collider.bounds.size.x + m_collider.bounds.extents.x) / resolution.x;
        float posY = m_collider.bounds.max.y - (y * m_collider.bounds.size.y + m_collider.bounds.extents.y) / resolution.y;

        return new Vector2(posX, posY);
    }

    Vector2Int selectedGem;
    bool gemSelected = false;
    bool canSelect = true;
    public void GemClicked(int x, int y)
    {
        if (!canSelect || GemMatchManager.Instance.gameOver)
            return;

        if (!gemSelected)
        {
            selectedGem = new Vector2Int(x, y);
            gemGrid[x, y].selected = true;
            gemSelected = true;
            return;
        }else 
        {
            if ((System.Math.Abs(selectedGem.x - x) == 1 ^ System.Math.Abs(selectedGem.y - y) == 1) && ((System.Math.Abs(selectedGem.x - x) <= 1 && System.Math.Abs(selectedGem.y - y) <= 1))) // next to
            {
                gemGrid[selectedGem.x, selectedGem.y].selected = false;
                gemSelected = false;

                Gem tmp = gemGrid[selectedGem.x, selectedGem.y];
                gemGrid[selectedGem.x, selectedGem.y] = gemGrid[x, y];
                gemGrid[x, y] = tmp;


                gemGrid[x, y].GoTo(GridToWorldPos(x, y));
                gemGrid[x, y].positionID = new Vector2Int(x, y);
                gemGrid[selectedGem.x, selectedGem.y].GoTo(GridToWorldPos(selectedGem.x, selectedGem.y));
                gemGrid[selectedGem.x, selectedGem.y].positionID = new Vector2Int(selectedGem.x, selectedGem.y);

                canSelect = false;
                StartCoroutine(resolveAllMatches(0.7f));

            }
            else
            {
                gemGrid[selectedGem.x, selectedGem.y].selected = false;
                selectedGem = new Vector2Int(x, y);
                gemGrid[x, y].selected = true;
                gemSelected = true;
            }


        }


    }
}

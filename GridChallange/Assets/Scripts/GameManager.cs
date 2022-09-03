using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int gridSize;
    public int matchCount;
    public List<GameObject> cells;
    public GameObject cellPrefab;
    public GameObject cellParent;
    public
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GenerateGrid()
    {
        if (gridSize > 0)
        {
            if (cells.Count > 0)
            {
                foreach (GameObject cell in cells)
                {
                    Destroy(cell);
                }
                cells.Clear();
            }
            float spaceBetween = 1;
            for (int i = 0; i < gridSize * gridSize; i++)
            {

                GameObject cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, cellParent.transform);

                cell.transform.localScale = new Vector3(cell.transform.localScale.x * 5f / gridSize - 0.05f, cell.transform.localScale.y, cell.transform.localScale.z * 5f / gridSize - 0.05f);
                cell.transform.localPosition = new Vector3(((spaceBetween * (i % gridSize)) * 5f / gridSize) + ((cell.transform.localScale.x + 0.05f) / gridSize / 2f) * gridSize, 0.1319122f, ((spaceBetween * (i / gridSize)) * 5f / gridSize) + ((cell.transform.localScale.x + 0.05f) / gridSize / 2f) * gridSize);
                cells.Add(cell);

            }
        }

    }

    public void ChangeGridSize(string size)
    {
        Debug.Log(size);
        gridSize = int.Parse(size);
    }
}

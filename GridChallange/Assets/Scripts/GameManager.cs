using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int gridSize;
    public int matchCount;
    public TextMeshProUGUI matchCountTextMesh;
    public List<GameObject> cells;
    public GameObject cellPrefab;
    public GameObject cellParent;
    public RaycastHit hit;

    public CinemachineVirtualCamera vCam1;
    public Camera OrtoCam;
    double screenWidth;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void GenerateGrid()
    {

        //Clean the grid
        foreach (GameObject cell in cells)
        {
            Destroy(cell);
        }
        cells.Clear();
        matchCount = 0;
        matchCountTextMesh.text = "Match Count = 0";

        //Spawn new cells to make the grid n x n
        float spaceBetween = 1;
        for (int i = 0; i < gridSize * gridSize; i++)
        {

            GameObject cell = Instantiate(cellPrefab, Vector3.zero, Quaternion.identity, cellParent.transform);
            cell.name = "Cell " + (i + 1);
            //Scales the single cell to fit the cells to the grid surface, after that position them to make them look like aligned
            cell.transform.localScale = new Vector3(cell.transform.localScale.x * 5f / gridSize - 0.05f, cell.transform.localScale.y, cell.transform.localScale.z * 5f / gridSize - 0.05f);
            cell.transform.localPosition = new Vector3(((spaceBetween * (i % gridSize)) * 5f / gridSize) + ((cell.transform.localScale.x + 0.05f) / gridSize / 2f) * gridSize, 0.1319122f, ((spaceBetween * (i / gridSize)) * 5f / gridSize) + ((cell.transform.localScale.x + 0.05f) / gridSize / 2f) * gridSize);
            cells.Add(cell);

        }

        //Sets the neighbor cells for each cell
        for (int i = 0; i < cells.Count; i += gridSize)
        {
            for (int z = i; z < i + gridSize; z++)
            {
                if (z + 1 < cells.Count && z != i + gridSize - 1)
                {
                    cells[z].GetComponent<CellScript>().neighborCells.Add(cells[z + 1]);

                }
                if (z + gridSize < cells.Count)
                {
                    cells[z].GetComponent<CellScript>().neighborCells.Add(cells[z + gridSize]);
                }
                if (z - 1 >= 0 && z != i)
                {
                    cells[z].GetComponent<CellScript>().neighborCells.Add(cells[z - 1]);
                }
                if (z - gridSize >= 0)
                {
                    cells[z].GetComponent<CellScript>().neighborCells.Add(cells[z - gridSize]);
                }

            }


        }


    }

    public void onTouchDown()
    {
        //On click to the screen, send the ray and make the cell clicked and neighbors impacted
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Cell")
            {
                if (!hit.transform.GetComponent<CellScript>().isCrossed)
                {
                    hit.transform.GetComponent<CellScript>().isCrossed = true;
                    hit.transform.GetComponent<CellScript>().Canvas.SetActive(true);
                    hit.transform.GetComponent<CellScript>().neighborAndSelfClickCount++;
                    foreach (GameObject cell in hit.transform.GetComponent<CellScript>().neighborCells)
                    {

                        cell.GetComponent<CellScript>().neighborAndSelfClickCount++;
                        /* foreach(GameObject neigborCell in cell.GetComponent<CellScript>().neighborCells)
                         {
                             if (neigborCell.GetComponent<CellScript>().isCrossed)
                             {
                                 neigborCell.GetComponent<CellScript>().neighborAndSelfClickCount++;
                             }
                         }*/
                    }
                }



            }
        }
    }

    public void onTouchUp()
    {

    }
    public void ChangeGridSize(string size)
    {
        //Input field grid size changer
        Debug.Log(size);
        gridSize = int.Parse(size);
    }
}

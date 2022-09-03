using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellScript : MonoBehaviour
{

    public List<GameObject> neighborCells;

    public bool isCrossed;
    public int neighborAndSelfClickCount;


    public GameObject Canvas;

    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(neighborAndSelfClickCount >= 3)
        {
            foreach(GameObject cell in neighborCells)
            {
                if(cell.GetComponent<CellScript>().isCrossed)
                {
                    cell.GetComponent<CellScript>().isCrossed = false;
                    cell.GetComponent<CellScript>().Canvas.SetActive(false);
                    cell.GetComponent<CellScript>().neighborAndSelfClickCount = 0;
                    foreach(GameObject neighborsNeighborCell in cell.GetComponent<CellScript>().neighborCells)
                    {
                        neighborsNeighborCell.GetComponent<CellScript>().neighborAndSelfClickCount = 0;
                    }
                }
            }

           
            isCrossed = false;
            Canvas.SetActive(false);
            neighborAndSelfClickCount = 0;
            manager.matchCount++;
            manager.matchCountTextMesh.text = "Match Count = " + manager.matchCount;
        }
    }

}

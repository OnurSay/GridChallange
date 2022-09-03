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
        //Checks if the adjacent cells and self has 3 neighbor that is clicked
        if (neighborAndSelfClickCount >= 3 && isCrossed)
        {
            foreach (GameObject cell in neighborCells)
            {
                //If any neighbor is clicked before, let's check its neighors to find other clicked cells
                if (cell.GetComponent<CellScript>().isCrossed)
                {
                    cell.GetComponent<CellScript>().isCrossed = false;
                    cell.GetComponent<CellScript>().Canvas.SetActive(false);
                    cell.GetComponent<CellScript>().neighborAndSelfClickCount = 0;
                    cell.GetComponent<CellScript>().checkNeighbor();
                }
                else
                {
                    if(cell.GetComponent<CellScript>().neighborAndSelfClickCount > 0)
                    {
                        cell.GetComponent<CellScript>().neighborAndSelfClickCount--;
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

    public void checkNeighbor()
    {
        //Finds the neighbors that is marked as a adjacent of a clicked cell.
        foreach (GameObject cell in neighborCells)
        {
            if (cell.GetComponent<CellScript>().neighborAndSelfClickCount > 0)
            {
                cell.GetComponent<CellScript>().neighborAndSelfClickCount--;
                //If the neighbor is clicked before, checks it's neighbors to find any impacted cells
                if (cell.GetComponent<CellScript>().isCrossed)
                {
                    cell.GetComponent<CellScript>().isCrossed = false;
                    cell.GetComponent<CellScript>().Canvas.SetActive(false);
                    cell.GetComponent<CellScript>().neighborAndSelfClickCount = 0;
                    cell.GetComponent<CellScript>().checkNeighbor();
                }
          
                
            }
        }
    }

}

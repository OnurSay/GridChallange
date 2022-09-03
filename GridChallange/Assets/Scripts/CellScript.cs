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
        if (neighborAndSelfClickCount >= 3 && isCrossed)
        {
            foreach (GameObject cell in neighborCells)
            {
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
        foreach (GameObject cell in neighborCells)
        {
            if (cell.GetComponent<CellScript>().neighborAndSelfClickCount > 0)
            {
                cell.GetComponent<CellScript>().neighborAndSelfClickCount--;
                if (cell.GetComponent<CellScript>().isCrossed)
                {
                    cell.GetComponent<CellScript>().isCrossed = false;
                    cell.GetComponent<CellScript>().Canvas.SetActive(false);
                    cell.GetComponent<CellScript>().neighborAndSelfClickCount = 0;
                    cell.GetComponent<CellScript>().checkNeighbor();
                }
          
                Debug.Log("Cell checked = " + cell.name + " isCrossed = " + cell.GetComponent<CellScript>().isCrossed);
            }
        }
    }

}

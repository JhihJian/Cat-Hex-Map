using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickHighLight : MonoBehaviour
{
    public HexGrid hexGrid;
    bool isDrag;
	public bool highLightMode = true;
	HexDirection dragDirection;
    HexCell previousCell;
	public HexFeatureManager features;

	// Start is called before the first frame update
	void Start()
    {
		

	}

	// Update is called once per frame
	void Update()
	{
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			
			if (Input.GetMouseButtonDown(0))
			{
				HandleInput();
				return;
			}

		}
		previousCell = null;
	}

	void HandleInput()
	{
		HexCell currentCell = GetCellUnderCursor();
		if (currentCell)
		{
			if (previousCell && previousCell != currentCell)
			{
				ValidateDrag(currentCell);
			}
			else
			{
				isDrag = false;
			}
			EditCells(currentCell);
			previousCell = currentCell;
		}
		else
		{
			previousCell = null;
		}
	}
	HexCell GetCellUnderCursor()
	{
		return
			hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
	}

	void ValidateDrag(HexCell currentCell)
	{
		for (
			dragDirection = HexDirection.NE;
			dragDirection <= HexDirection.NW;
			dragDirection++
		)
		{
			if (previousCell.GetNeighbor(dragDirection) == currentCell)
			{
				isDrag = true;
				return;
			}
		}
		isDrag = false;
	}

	void EditCells(HexCell center)
	{
		int centerX = center.coordinates.X;
		int centerZ = center.coordinates.Z;

		EditCell(hexGrid.GetCell(new HexCoordinates(centerX, centerZ)));
	}
	HexCell preHighLightCell;
	void EditCell(HexCell cell)
	{
		if (cell)
		{

			if (highLightMode)
			{
                
                if (preHighLightCell)
                {
					preHighLightCell.Walled = false;
				}
				cell.Walled = !cell.Walled;
				preHighLightCell = cell;
			}
		}
	}

}

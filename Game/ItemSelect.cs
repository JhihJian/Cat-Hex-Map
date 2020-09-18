using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSelect : MonoBehaviour, IPointerClickHandler
{
    public HexGrid hexGrid;
    ItemGridGenerator itemGridGenerator;
    private GameObject followObject;
    private PlantItem nowPlantItem;
    private void Awake()
    {
        itemGridGenerator = GetComponent<ItemGridGenerator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Vector3 getInGridWorldPosition()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
        return hit.point;
            
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (followObject != null && nowPlantItem != null)
            {
                HexCell targetCell = hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));

                if (targetCell != null)
                {
               
                    plantItemInGrid(nowPlantItem);
                }
            }
            
        }
        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClickInput();
        }
        if (followObject != null)
        {
            followObject.transform.position = getInGridWorldPosition();
        }
    }
    void HandleRightClickInput()
    {
        if (followObject != null)
        {
            Destroy(followObject);
            nowPlantItem = null;
            followObject = null;
        }
    }
    PlantItem GetItem()
    {
        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider);
           
            return null;
        }
        return null;

    }

    //只能检测ui的点击
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject gameObject=eventData.pointerCurrentRaycast.gameObject.transform.parent.gameObject;
        PlantItem plantItem=itemGridGenerator.locatePlantItem(gameObject);
        Debug.Log(plantItem);
        if (plantItem != null)
        {
            nowPlantItem = plantItem;
            initFollowObject(plantItem.prefebs[0]);
        }
    }
    public void plantItemInGrid(PlantItem plantItem)
    {
        Debug.Log("try plant");
        HexCell targetCell= hexGrid.GetCell(Camera.main.ScreenPointToRay(Input.mousePosition));
        hexGrid.plantPrefeb(targetCell, getInGridWorldPosition(),plantItem.prefebs[0]);
    }

    private void initFollowObject(GameObject gameObject)
    {
        if (followObject != null)
        {
            Destroy(followObject);
            nowPlantItem = null;
            followObject = null;
        }
        followObject = GameObject.Instantiate(gameObject);
        followObject.transform.position= getInGridWorldPosition();
    }
}

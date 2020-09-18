using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ItemGridGenerator : MonoBehaviour
{
    public GameObject itemPrefabs;
    public List<PlantItem> plantItems;
    [Header("已初始化对象")]
    public List<GameObject> items;
    private void Awake()
    {
        items = new List<GameObject>();
        for (int i = 0; i < plantItems.Count; i++)
        {
            GameObject initedItem=GameObject.Instantiate<GameObject>(itemPrefabs,transform);
            items.Add(initedItem);
            initedItem.name = plantItems[i].itemName;
            if (plantItems[i].prefebs.Count != 0)
            {
                GameObject plantPrefab = plantItems[i].prefebs[0];
                GameObject plant = GameObject.Instantiate<GameObject>(plantPrefab, initedItem.transform);
                plant.transform.localScale=new Vector3(plantItems[i].scale, plantItems[i].scale, plantItems[i].scale);
                plant.transform.localPosition=new Vector3(0, plantItems[i].yOffset, 0);
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public PlantItem locatePlantItem(GameObject gameObject)
    {
       if(items.Contains(gameObject))
        {
            return plantItems[items.IndexOf(gameObject)];
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New PlantItem",menuName ="Item/Plant")]
public class PlantItem : ScriptableObject
{

   public string itemName;
   public List<GameObject> prefebs;
   [TextArea]
   public string info;
   public float scale;
   public float yOffset;
    
    
}

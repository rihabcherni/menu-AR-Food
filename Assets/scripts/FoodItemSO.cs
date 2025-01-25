using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NewFoodItem", menuName = "Food/FoodItem")]
public class FoodItemSO : ScriptableObject
{
    [Header("Basic Information")]
    public string foodName;
    public string description;
    public string category;

    [Header("Food Details")]
    public string[] ingredients;
    [TextArea] public string recipe;
    public float price;

    [Header("3D Model")]
    public Sprite foodImage;
    public GameObject modelPrefab; 
}

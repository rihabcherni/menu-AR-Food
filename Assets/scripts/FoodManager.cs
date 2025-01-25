using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class FoodManager : MonoBehaviour
{
    [Header("List of Food Items")]
    public List<FoodItemSO> foodItems;

    public List<FoodItemSO> GetFoodItemsByCategory(string category)
    {
        return foodItems.FindAll(item => item.category == category);
    }

    void Start()
    {
        // Example: Print all food items to the console
        foreach (FoodItemSO item in foodItems)
        {
            Debug.Log($"Name: {item.foodName}, Price: {item.price}, Category: {item.category}");
        }
    }
}

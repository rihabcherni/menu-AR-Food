using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedFoods : MonoBehaviour
{
    public static SelectedFoods Instance;
    public Dictionary<FoodItemSO, int> selectedFoods = new Dictionary<FoodItemSO, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddSelectedFood(FoodItemSO food)
    {
        if (selectedFoods.ContainsKey(food))
        {
            selectedFoods[food]++;
        }
        else
        {
            selectedFoods.Add(food, 1);
        }
    }

    public List<FoodItemSO> GetSelectedFoods()
    {
        List<FoodItemSO> foods = new List<FoodItemSO>(selectedFoods.Keys);
        return foods;
    }
}

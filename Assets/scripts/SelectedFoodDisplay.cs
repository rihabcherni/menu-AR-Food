using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedFoodDisplay : MonoBehaviour
{
    [Header("3D Visualisation")]
    public Transform modelParent;
    private List<GameObject> instantiatedModels = new List<GameObject>();
    public float spacingX = 0.2f;
    public float spacingZ = 0.3f;
    public int itemsPerRow = 2;
    public Text totalPriceText;

    void Start()
    {
        DisplaySelectedFoods();
    }

    void DisplaySelectedFoods()
    {
        Dictionary<FoodItemSO, int> selectedFoods = SelectedFoods.Instance.selectedFoods; // Access dictionary with quantities
        float totalPrice = 0;
        int totalItemCount = 0; // Total quantity of all food items

        foreach (var model in instantiatedModels)
        {
            Destroy(model);
        }
        instantiatedModels.Clear();

        int currentRow = 0;
        int currentColumn = 0;

        foreach (var foodEntry in selectedFoods)
        {
            FoodItemSO food = foodEntry.Key;
            int quantity = foodEntry.Value;
            totalItemCount += quantity; 
            if (food.modelPrefab != null)
            {
                for(int i=0; i< foodEntry.Value; i++)
                {
                    GameObject newModel = Instantiate(food.modelPrefab, modelParent);
                    float posX = currentColumn * spacingX;
                    float posZ = currentRow * -spacingZ;
                    newModel.transform.localPosition = new Vector3(posX, 0, posZ);
                    newModel.transform.localRotation = Quaternion.identity;
                    instantiatedModels.Add(newModel);

                    currentColumn++;
                    if (currentColumn >= itemsPerRow)
                    {
                        currentColumn = 0;
                        currentRow++;
                    }
                }
               
            }

            float foodTotal = food.price * quantity; // Calculate total for this food item
            totalPrice += foodTotal;

            Debug.Log($"Food: {food.name}, Quantity: {quantity}, Price: ${food.price}, Total: ${foodTotal}");
        }

        totalPriceText.text = $"Total price: ${totalPrice}\nTotal items: {totalItemCount}";
        Debug.Log($"Total Items (with quantity): {totalItemCount}");
        Debug.Log($"Total Price: ${totalPrice}");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FoodListUI : MonoBehaviour
{
    private Dictionary<FoodItemSO, GameObject> foodButtons = new Dictionary<FoodItemSO, GameObject>();
    public GameObject buttonPrefab; // Bouton pour chaque plat
    public Transform contentParent; // ScrollView Content
    public FoodManager foodManager;

    [Header("Détails du Plat")]
    public Image foodImage; // Image 2D pour les détails
    public Text foodNameText;
    public Text descriptionText;
    public Text ingredientsText;
    public Text recipeText;
    public Text priceText;

    [Header("3D Visualisation")]
    public Transform modelParent; // Parent pour le modèle 3D
    private GameObject currentModel; // Référence au modèle affiché

    [Header("Facture")]
    public GameObject invoicePanel; // Panneau de facture
    public Transform invoiceContentParent; // Parent pour les éléments de la facture
    public GameObject invoiceItemPrefab; // Préfabriqué pour les éléments de la facture
    public Text totalPriceText; // Texte pour afficher le total

    [Header("Boutons")]
    public Button buttonShowInvoice; 
    public Button buttonCloseInvoice;
    public Button buttonCloseRateRes;
    public Button buttonCloseRate;
    public Button buttonGo;
    
    private Dictionary<FoodItemSO, int> foodCount = new Dictionary<FoodItemSO, int>();
    private FoodItemSO selectedFood;

    [Header("Notation et Commentaires")]
    public List<Button> starButtons; // Boutons pour les étoiles
    public InputField commentInputField; // Champ de texte pour les commentaires
    public Button submitRatingButton; // Bouton pour soumettre la notation
    private int currentRating; // Valeur actuelle de la notation
    public GameObject ratingPanel;
    public Button ratingButton;

    [Header("Res rating")]
    public Text ratingText;
    public Text commentairesText;
    public GameObject resRate;
    public Sprite star1Sprite; // Default or "unselected" star
    public Sprite star2Sprite; // "Selected" star

    void Start()
    {
        PopulateFoodList();
        invoicePanel.SetActive(false);
        if (buttonShowInvoice != null)
        {
            buttonShowInvoice.onClick.AddListener(ShowInvoice);
        }
        if (ratingButton != null)
        {
            ratingButton.onClick.AddListener(ShowRatingPanel);
        }

        if (buttonCloseInvoice != null)
        {
            buttonCloseInvoice.onClick.AddListener(CloseInvoice);
        }

        if (buttonCloseRateRes != null)
        {
            buttonCloseRateRes.onClick.AddListener(CloseRateRes);
        }
        if (buttonCloseRate != null)
        {
            buttonCloseRate.onClick.AddListener(CloseRate);
        }
        if (buttonGo != null)
        {
            buttonGo.onClick.AddListener(LoadDisplayFoodsScene);
        }
        // Initialisation des étoiles
        for (int i = 0; i < starButtons.Count; i++)
        {
            int starIndex = i + 1; // Les étoiles commencent à 1
            starButtons[i].onClick.AddListener(() => SetRating(starIndex));
        }

        // Associer le bouton de soumission
        if (submitRatingButton != null)
        {
            submitRatingButton.onClick.AddListener(SubmitRating);
        }
        if (ratingPanel != null)
        {
            ratingPanel.SetActive(false);
        }
        if (resRate != null)
        {
            resRate.SetActive(false);
        }
    }

    void PopulateFoodList()
    {
        foreach (var item in foodManager.foodItems)
        {
            GameObject button = Instantiate(buttonPrefab, contentParent);
            button.GetComponentInChildren<Text>().text = item.foodName;

            foodButtons[item] = button;

            Image buttonImage = button.transform.GetChild(0).GetComponent<Image>();
            if (buttonImage != null && item.foodImage != null)
            {
                buttonImage.sprite = item.foodImage;
            }

            button.GetComponent<Button>().onClick.AddListener(() => ShowFoodDetails(item));
        }
    }

    void ShowFoodDetails(FoodItemSO food)
    {
        selectedFood = food;

        // Mettre à jour les informations de détail
        foodNameText.text = food.foodName;
        descriptionText.text = food.description;
        ingredientsText.text = string.Join(", ", food.ingredients);
        recipeText.text = food.recipe;
        priceText.text = $"Prix : ${food.price}";

        foodImage.sprite = food.foodImage;

        // Afficher le modèle 3D
        if (currentModel != null)
        {
            Destroy(currentModel);
        }

        if (food.modelPrefab != null)
        {
            currentModel = Instantiate(food.modelPrefab, modelParent);
        }
    }

    public void AddToMenu()
    {
        if (selectedFood != null)
        {
            if (foodCount.ContainsKey(selectedFood))
            {
                foodCount[selectedFood]++;
            }
            else
            {
                foodCount[selectedFood] = 1;
            }
            SelectedFoods.Instance.AddSelectedFood(selectedFood);
            if (foodButtons.TryGetValue(selectedFood, out GameObject button))
            {
                Text[] texts = button.GetComponentsInChildren<Text>();
                if (texts.Length > 0) texts[0].text = $"{selectedFood.foodName}";
                if (texts.Length > 1) texts[1].text = $"x{foodCount[selectedFood]}";
            }

            Debug.Log($"Ajouté au menu : {selectedFood.foodName}");
        }
    }
    public void removeFromMenu()
    {
        if (selectedFood != null)
        {
            if (foodCount.ContainsKey(selectedFood))
            {
                foodCount[selectedFood]--;
            }
            else
            {
                foodCount[selectedFood] = 0;
            }
            SelectedFoods.Instance.AddSelectedFood(selectedFood);
            if (foodButtons.TryGetValue(selectedFood, out GameObject button))
            {
                Text[] texts = button.GetComponentsInChildren<Text>();
                if (texts.Length > 0) texts[0].text = $"{selectedFood.foodName}";
                if (texts.Length > 1) texts[1].text = $"x{foodCount[selectedFood]}";
            }

            Debug.Log($"Ajouté au menu : {selectedFood.foodName}");
        }
    }

    public void ShowInvoice()
    {
        invoicePanel.SetActive(true);
        foreach (Transform child in invoiceContentParent)
        {
            Destroy(child.gameObject);
        }

        float totalPrice = 0;
        foreach (var entry in foodCount)
        {
            FoodItemSO food = entry.Key;
            int count = entry.Value;
            if (count > 0)
            {
                GameObject invoiceItem = Instantiate(invoiceItemPrefab, invoiceContentParent);
                Text[] texts = invoiceItem.GetComponentsInChildren<Text>();
                Image foodImage = invoiceItem.GetComponentInChildren<Image>();
                if (foodImage != null && food.foodImage != null)
                {
                    foodImage.sprite = food.foodImage;
                    foodImage.color = new Color32(255, 255, 255, 255);
                    RectTransform imageRect = foodImage.GetComponent<RectTransform>();
                    if (imageRect != null)
                    {
                        imageRect.sizeDelta = new Vector2(100, 80);
                    }
                }
                if (texts.Length > 0) texts[0].text = $"{food.foodName}";
                if (texts.Length > 1) texts[1].text = $"Prix : ${food.price * count}";
                if (texts.Length > 2) texts[2].text = $"Quantité : x{count}";
                totalPrice += food.price * count;
            }
        }
        totalPriceText.text = $"Total : ${totalPrice}";
    }

    public void CloseInvoice()
    {
        invoicePanel.SetActive(false);
    }
    void SetRating(int rating)
    {
        currentRating = rating;
        Debug.Log($"Rating updated: {currentRating} stars");

        for (int i = 0; i < starButtons.Count; i++)
        {
            Image starImage = starButtons[i].GetComponent<Image>();
            if (starImage != null)
            {
                // Assign different sprites based on the rating
                starImage.sprite = (i < rating) ? star2Sprite : star1Sprite;
            }
        }
    }

    //void SetRating(int rating)
    //{
    //    currentRating = rating;
    //    Debug.Log($"Rating mis à jour : {currentRating}");
    //    for (int i = 0; i < starButtons.Count; i++)
    //    {
    //        Image starImage = starButtons[i].GetComponent<Image>();
    //        if (starImage != null)
    //        {
    //            starImage.color = (i < rating) ? Color.yellow : Color.gray;
    //        }
    //    }
    //}

    void SubmitRating()
    {
            string comment = commentInputField.text;

            Debug.Log($"Notation soumise : {currentRating} étoiles");

            if (ratingPanel != null)
            {
                ratingPanel.SetActive(false);
            }
            ratingText.text = $"Notation pour: {currentRating} étoiles";
            commentairesText.text = $"Commentaire : {comment}";

            if (resRate != null)
            {
                resRate.SetActive(true);
            }
    }

    public void ShowRatingPanel()
    {
        if (ratingPanel != null)
        {
            ratingPanel.SetActive(true);
        }
        ResetStars();
    }
    void ResetStars()
    {
        currentRating = 0; // Reset the rating value
        for (int i = 0; i < starButtons.Count; i++)
        {
            Image starImage = starButtons[i].GetComponent<Image>();
            if (starImage != null)
            {
                // Set to the default sprite (e.g., unselected state)
                starImage.sprite = star1Sprite; // Default star sprite
            }
        }
    }

    public void CloseRate()
    {
        ratingPanel.SetActive(false);
    }
    public void CloseRateRes()
    {
        resRate.SetActive(false);
    }
    public void LoadDisplayFoodsScene()
    {
        SceneManager.LoadScene("LastDisplayFoods");
    }
}

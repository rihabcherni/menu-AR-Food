using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
public class rotatet3D : MonoBehaviour
{

    public float rotationSpeed = 100f; // Sensibilité pour la rotation avec la souris
    private bool hasClicked = false;  // Vérifie si le premier clic a été effectué

    void Update()
    {
        // Vérifier si le bouton gauche de la souris a été cliqué
        if (Input.GetMouseButtonDown(0)) // Clic gauche
        {
            if (!hasClicked)
            {
                // Appliquer une rotation initiale spécifique
                transform.rotation = Quaternion.Euler(90f, 0f, 180f);
                hasClicked = true; // Marquer le clic comme effectué
            }
        }

        // Permettre la rotation normale avec la souris après le premier clic
        if (hasClicked && Input.GetMouseButton(1)) // Clic droit maintenu
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Appliquer la rotation basée sur le mouvement de la souris
            float rotationX = mouseY * rotationSpeed * Time.deltaTime;
            float rotationY = mouseX * rotationSpeed * Time.deltaTime;

            transform.Rotate(-rotationX, rotationY, 0f, Space.World);
        }
    }
}

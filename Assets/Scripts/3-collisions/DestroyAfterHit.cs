using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DestroyAfterHits : MonoBehaviour
{
    [SerializeField] private string triggeringTag;
    [SerializeField] private int hitPoints;
    [SerializeField] private NumberField healthField;
    [SerializeField] private int requestedSum;
    [SerializeField] private TextMeshProUGUI pickedNumbersText;

    private int calculateNumber = 0;

    private void Start()
    {
        if (healthField == null || pickedNumbersText == null)
        {
            Debug.LogError("Health field or picked numbers text not assigned.");
            return;
        }

        healthField.SetNumber(hitPoints);
        pickedNumbersText.text = ""; // Clear the text initially
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggeringTag))
        {
            string numberFall = other.gameObject.name;
            int number;

            // Extract the base name without the "(Clone)" suffix
            string baseName = numberFall.Replace("(Clone)", "");

            // Attempt to parse the base name into an integer
            if (int.TryParse(baseName, out number))
            {
                calculateNumber += number;
                Debug.LogError("calculateNumber is : " + calculateNumber);
                Debug.LogError("requestedSum is : " + requestedSum);

                if (calculateNumber == requestedSum)
                {
                    SceneManager.LoadScene("win");
                }
                else if (calculateNumber > requestedSum)
                {
                    calculateNumber -= number;

                    hitPoints--;
                    healthField.SetNumber(hitPoints);
                    Destroy(other.gameObject); // Destroy the colliding GameObject



                    if (hitPoints <= 0)
                    {
                        Destroy(other.gameObject); // Destroy the colliding GameObject
                        Destroy(this.gameObject); // Destroy the colliding GameObject

                    }
                }
                else // calculateNumber < requestedSum
                {
                    UpdatePickedNumbersText(number);
                    Destroy(other.gameObject); // Destroy the colliding GameObject

                }
            }

            else
            {
                Debug.LogError("Failed to parse number: " + numberFall); // Log an error if parsing fails
            }

        }
        else if(other.CompareTag("bomb")) {

            Debug.LogError("bomb");

            hitPoints--;
            healthField.SetNumber(hitPoints);
            Destroy(other.gameObject); // Destroy the colliding GameObject

            if (hitPoints <= 0)
            {
                Destroy(other.gameObject); // Destroy the colliding GameObject
                Destroy(this.gameObject); // Destroy the colliding GameObject

            }
        }
    }





    private void UpdatePickedNumbersText(int number)
    {
        pickedNumbersText.text += number.ToString() + "\n";
    }
}
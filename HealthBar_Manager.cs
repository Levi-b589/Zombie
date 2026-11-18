using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Manager : MonoBehaviour
{

    public Image healthfillimage;

    public float currenthealth = 100f;
    public float maxhealth = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Updatehealthbar();
    }

    // Update is called once per frame
    void Update()
    {
     //add an if statement when zombie collider hits player's the player takes damage 
     if(Input.GetKeyDown(KeyCode.Space))
        {
            takeDamage(10);
        }

    }
    public void takeDamage(float damageAmount)
    {
        currenthealth -= damageAmount;
        if (currenthealth <0)
        {
            currenthealth = 0;
        }
        Updatehealthbar();
    }

    public void Updatehealthbar()
    {
        float fillRatio = currenthealth / maxhealth;

        if (healthfillimage != null)
        {
            healthfillimage.fillAmount = fillRatio;
        }
    }
}

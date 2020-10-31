using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Hbar;
    public Slider sliderHP;

    public Text txt;

    public Gradient gradient;
    public Image fill;

    public Image face;

    public Sprite fine;
    public Sprite okay;
    public Sprite ohNo;
    public Sprite rip;

    public void SetMaxHealth(int health)
    {
        sliderHP.maxValue = health;
        sliderHP.value = health;
        txt.text = health.ToString();

        fill.color = gradient.Evaluate(1f);

        face.sprite = fine;
    }

    public void SetHealth(int health)
    {
       
        sliderHP.value = float.Parse(health.ToString());
        txt.text = health.ToString(); 
        txt.color = Color.white;

        fill.color = gradient.Evaluate(sliderHP.normalizedValue);
        if (health == 0)
        {
            face.sprite = rip;
            txt.color = Color.red;
        }
        else if(health>0 && health <= (sliderHP.maxValue / 3))
        {
            face.sprite = ohNo;
            txt.color = Color.white;
        }
        else if (health > (sliderHP.maxValue / 3) && health <= ((sliderHP.maxValue / 3)*2))
        {
            face.sprite = okay;
            txt.color = Color.green;
        }
    }

}

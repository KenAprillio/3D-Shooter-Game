using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static int score;
    public Text scoreText;
    public Text ammoText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        ammoText.text = WeaponController.ammo + "/" + WeaponController.ammoMag;
    }

    public void ResetScore()
    {
        score = 0;
    }
}

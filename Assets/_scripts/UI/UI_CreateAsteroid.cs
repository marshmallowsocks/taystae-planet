using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CreateAsteroid : GameCore {

    public GameObject asteroid;
    private static int shipCount = 0;

    void Start () {
        StartCoroutine(_GenerateRandomShip());
    }

    IEnumerator _GenerateRandomShip()
    {
        while (shipCount < 3) {
            GameObject randomShip = Instantiate(asteroid);
            RectTransform rectTransform = randomShip.transform as RectTransform;

            randomShip.transform.position = Random.insideUnitCircle * 10;
            randomShip.transform.localScale = new Vector3(0.75f, 0.75f, 1);

            rectTransform.sizeDelta = new Vector2(1, 1);

            randomShip.transform.SetParent(gameObject.transform);
            shipCount++;
            yield return new WaitForSeconds(3f);
        }

        yield break;
    }
}

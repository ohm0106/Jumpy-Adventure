using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickHandler : MonoBehaviour
{
    private enemySliderController sliderController;

    void Start()
    {
        sliderController = FindObjectOfType<enemySliderController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForEnemy();
        }
    }

    void CheckForEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                GameCanvasManager.Instance.SetEnemyHPBar(enemy.GetEnemyType(), enemy.GetCurrentHealth(), enemy.GetMaxHealth());
                sliderController.Show(true);

            }
        }
    }
}

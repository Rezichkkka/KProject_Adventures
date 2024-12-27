using UnityEngine;

public class WinSign : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnLevelCompleted();
        }
    }

    private void OnLevelCompleted()
    {

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        if (levelReached < 2)
        {
            PlayerPrefs.SetInt("levelReached", 2);
        }
        PlayerPrefs.Save();
        Debug.Log("Уровень завершен, прогресс сохранен!");
    }
}

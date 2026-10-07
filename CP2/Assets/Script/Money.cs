using UnityEngine;

public class Money : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D other)
    {
        UIgame moedas = FindAnyObjectByType<UIgame>();
        moedas.Coletado();
        gameObject.SetActive(false);
    }
}

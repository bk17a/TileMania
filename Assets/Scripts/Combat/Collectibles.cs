using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] int pointValue = 100;
    [SerializeField] GameObject collectEffect;
    [SerializeField] string coinID;

    void Start()
    {
        if (string.IsNullOrEmpty(coinID)) return;
        StartCoroutine(CheckIfCollected());
    }

    System.Collections.IEnumerator CheckIfCollected()
    {
        yield return null;
        if (GameSession.Instance != null && GameSession.Instance.IsCoinCollected(coinID))
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameSession.Instance?.CollectCoin(coinID, pointValue);
            AudioManager.Instance?.PlayCollect();
            if (collectEffect != null)
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
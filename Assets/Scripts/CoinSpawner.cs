using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject Coin;

    public void SpawnCoin()
    {
        Instantiate(Coin);
    }


}

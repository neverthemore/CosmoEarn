using UnityEngine;

public class GameManager : MonoBehaviour
{
    //+ еще сделать сохранение
    public static GameManager Instance { get; private set; }

    [SerializeField]public int Money { get; private set; }

    [SerializeField] public bool _isReturnToAngar = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int amount)
    {
        Money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (Money >= amount)
        {
            Money -= amount;
            return true;
        }
        return false;
    }

    public bool NeedToAngar()
    {
        if (_isReturnToAngar)
        {
            _isReturnToAngar = false;
            return true;
        }
        return false;
    }
}

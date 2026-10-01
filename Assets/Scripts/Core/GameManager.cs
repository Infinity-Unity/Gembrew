using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int startingGold;
    private  int currentGold;

    public static GameManager Instance;


    private void Awake()
    {

        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        SaveData data = SaveManager.Load();
        if (data != null) currentGold = data.gold;
        else currentGold = startingGold;

        

       
        

    }

    private void Start()
    {
       


    }


}

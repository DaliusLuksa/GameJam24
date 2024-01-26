using UnityEngine;

public class ClueManager : MonoBehaviour
{
    public static ClueManager instance;

    public string emperorFavoriteColor;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string addEmperorsColorClue()
    {
        string[] colors = {"green", "red", "blue"};

        int randomIndex = Random.Range(0, colors.Length);

        emperorFavoriteColor = colors[randomIndex];

        return colors[randomIndex];
    }
}

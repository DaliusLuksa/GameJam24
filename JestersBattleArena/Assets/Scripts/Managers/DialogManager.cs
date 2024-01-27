using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public string emperorFavoriteColor;
    public int maxInteractionNumber = 5;
    public int interactionNumber = 5;

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

    public void Reset() {
        interactionNumber = 5;
        emperorFavoriteColor = "";
    }
}

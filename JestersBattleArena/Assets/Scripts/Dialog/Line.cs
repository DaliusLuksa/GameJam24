using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Line")]
public class Line : ScriptableObject
{
    [TextArea(2,5)]
    public string text;
}

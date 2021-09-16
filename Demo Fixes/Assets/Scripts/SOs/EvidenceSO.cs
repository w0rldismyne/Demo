using UnityEngine;

[CreateAssetMenu(menuName="Evidence", fileName="New Evidence")]
public class EvidenceSO : ScriptableObject
{
    public string variableName;
    public Sprite sprite;
    [TextArea(1,5)]
    public string description;
}

using UnityEngine;
using View;

[CreateAssetMenu(menuName = "CellViewConfig", fileName = "CellViewConfig")]
public class CellViewConfig : ScriptableObject
{
    [SerializeField] private CellView cellView;

    public CellView CellView => cellView;
}
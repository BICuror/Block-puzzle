using View;
using UnityEngine;

public sealed class PartContainerViewContainer : MonoBehaviour
{
    [SerializeField] private PartContainerView[] _partContainerViews;

    public int Length => _partContainerViews.Length; 
    
    public PartContainerView[] Get() => _partContainerViews;
}


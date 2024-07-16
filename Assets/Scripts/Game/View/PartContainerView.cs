using UnityEngine;

namespace View
{
    public sealed class PartContainerView : MonoBehaviour
    {
        [SerializeField] private Transform _partParent;
        
        public Transform PartParent => _partParent;
    }
}


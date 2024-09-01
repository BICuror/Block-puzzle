using UnityEngine;
using ViewModel;
using Zenject;
using UniRx;

[RequireComponent(typeof(Camera))]

public sealed class CameraView : MonoBehaviour
{
    [SerializeField] private float _minimalCameraSize = 12f;
    [Inject] private TilemapViewModel _tilemapViewModel;
    private CompositeDisposable _disposable = new();
    private Camera _camera; 
    private int _width;
    private int _height;
   
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _tilemapViewModel.Width.Subscribe(SetXPosition).AddTo(_disposable);
        _tilemapViewModel.Height.Subscribe(SetYPosition).AddTo(_disposable);
    }
    
    public void SetXPosition(int width)
    {
        _width = width;

        float xPosition = (float)(width - 1) / 2;

        Vector3 newPosition = new Vector3(xPosition, transform.position.y, transform.position.z);

        transform.position = newPosition;

        UpdateCameraSize();
    }
    
    public void SetYPosition(int height)
    {
        _height = height;

        float yPosition = (float)(height - 1) / 2;

        Vector3 newPosition = new Vector3(transform.position.x, yPosition, transform.position.z);

        transform.position = newPosition;

        UpdateCameraSize();
    }

    private void UpdateCameraSize()
    {
        float maxAspect = Mathf.Max(_width, _height);

        maxAspect = Mathf.Clamp(maxAspect, _minimalCameraSize, float.MaxValue);
    
        _camera.orthographicSize = maxAspect;
    }
}

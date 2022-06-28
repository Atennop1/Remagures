using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private VectorValue _playerPosition;
    [SerializeField] private GameSaveManager _saveManager;
    
    public void OnTriggerEnter2D()
    {
        _playerPosition.Value = transform.position + Vector3.up / 2;
        _saveManager.SaveGame();
    }
}

using RitimUS.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RitimUS
{
    public class BrickLayoutGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject brick;
        [SerializeField] private Vector2 firstBrickPosition;
        [SerializeField] private Vector2Int layoutSize;
        [SerializeField] private Vector2 layoutOffset;

        private List<GameObject> _bricksList = new List<GameObject>();
        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += InitializeLevel;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= InitializeLevel;
        }

        private void InitializeLevel()
        {
            GenerateLevel();
            BreakerManager.Instance.InitializeGame(_bricksList.Count);
        }
        private void GenerateLevel()
        {
            for(int i = 0; i < _bricksList.Count; i++)
            {
                Destroy(_bricksList[i]);
            }
            _bricksList.Clear();
            for (int i = 0; i < layoutSize.x; i++)
            {
                for (int j = 0; j < layoutSize.y; j++)
                {
                    _bricksList.Add(Instantiate(brick, new Vector3(i + layoutOffset.x*i, -j  -layoutOffset.y*j, 0)+(Vector3)firstBrickPosition, Quaternion.identity));
                }
            }
        }

    }
}

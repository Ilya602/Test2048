using Assets.Scripts.Entities;
using Assets.Scripts.Inputs;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<int> OnScoreChanged;
        [SerializeField] private UnityEvent<int> OnTurnsCountChanged;
        [SerializeField] private UnityEvent OnGameOver;

        [SerializeField] private InputController _inputCont;
        [SerializeField] private ParticleSystem _mergeEffect;
        [SerializeField] private CubeEntity[] _startCubesPrefs;
        [SerializeField] private CubeEntity[] _cubesPrefs;
        [SerializeField, Header("In Milliseconds")] private int spawnCubeCD = 1000;
        private CubeEntity _currCube;
        private int _currScore = 0;
        private int _turns = 18;


        private void Start()
        {
            _inputCont.Initialize(new DragInput());
            OnScoreChanged.Invoke(0);
            SpawnCube();
        }

        private void SpawnCube()
        {
            var isCube4 = Random.Range(0f, 1f) <= 0.25f;
            _currCube = Instantiate(_startCubesPrefs[isCube4 ? 1 : 0], new Vector3(0f, .3f, -4.3f), Quaternion.identity);
            _currCube.Initialize(true);
        }

        public void MoveCube() // event of drag on input panel
        {
            if (_currCube == null) return;
            _currCube.transform.Translate(new Vector3(_inputCont.GetInput().x, 0f) * Time.deltaTime, Space.World);
            _currCube.transform.position = new Vector3(Mathf.Clamp(_currCube.transform.position.x, -1.68f, 1.68f),
                _currCube.transform.position.y, _currCube.transform.position.z);
        }

        public async void ReleaseCube() // event of end drag on input panel
        {
            if (_currCube == null) return;
            _currCube.Push(Vector3.forward);
            _currCube = null;

            _turns--;
            OnTurnsCountChanged.Invoke(_turns);

            await Task.Delay(spawnCubeCD);
            if (_turns == 0)
            {
                GameOver();
                return;
            }

            SpawnCube();
        }

        public void OnCubesMerge(CubeEntity a, CubeEntity b)
        {
            if (a == null || b == null) return;

            int cubeType = (int)a.Type;
            if (cubeType < _cubesPrefs.Length)
            {
                Vector3 spawnPos = (a.transform.position + b.transform.position) / 2;
                _mergeEffect.transform.position = spawnPos;
                _mergeEffect.Play();
                _currScore += GetScoreReward(cubeType);
                OnScoreChanged.Invoke(_currScore);

                Instantiate(_cubesPrefs[cubeType], spawnPos, Quaternion.identity).Initialize(false);
                Destroy(a.gameObject);
                Destroy(b.gameObject);
            }

            else
            {
                GameOver();
            }
        }

        private int GetScoreReward(int mergedType)
        {
            int value = (int)Mathf.Pow(2, mergedType + 1);
            int reward = value <= 2 ? 1 : (value / 4) * 2;
            return reward;
        }

        private void GameOver()
        {
            OnGameOver.Invoke();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
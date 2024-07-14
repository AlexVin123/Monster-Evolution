using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    [SerializeField] private AICharacter _prefabCharacter;
    [SerializeField] private int _maxPlayers = 5;
    [SerializeField] private float _radiusSpawn;
    [SerializeField] private Transform _centerSpawn;
    [SerializeField] private float _timeSpawn = 30;
    [SerializeField] private Exp _playerExp;

    private List<AICharacter> _allCharacters;
    private List<AICharacter> _spawnerCharacters;
    private List<AICharacter> _DiedCharacter;

    private WaitForSeconds _delay;

    public void StartSpawn()
    {
        _delay = new WaitForSeconds(_timeSpawn);
        _allCharacters = new List<AICharacter>();
        _spawnerCharacters = new List<AICharacter>();
        _DiedCharacter = new List<AICharacter>();
        _allCharacters = CreateChatecters();
        _DiedCharacter = CreateChatecters();
        StartCoroutine(SpawnCorutine());


    }

    private List<AICharacter> CreateChatecters()
    {
        List<AICharacter> characters = new List<AICharacter>();

        for (int i = 0; i < _maxPlayers; i++)
        {
            AICharacter character = Instantiate(_prefabCharacter, transform);
            character.gameObject.SetActive(false);
            characters.Add(character);
        }

        return characters;
    }

    private Vector3 CreateSpawnPoint()
    {
        Vector3 randomPoin = Vector3.zero;

        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(Random.insideUnitSphere * _radiusSpawn + transform.position, out navMeshHit, _radiusSpawn, NavMesh.AllAreas);
        randomPoin = navMeshHit.position;

        return randomPoin;
    }

    private IEnumerator SpawnCorutine()
    {
        while (true) 
        {
            yield return _delay;
            SpawnCharacter(GenerateLvl());
        }
    }

    private int GenerateLvl()
    {
        if (_playerExp.CurrentLvl >= _playerExp.MaxLvl)
            return Random.Range(1, _playerExp.MaxLvl);
        else
            return Random.Range(1, _playerExp.CurrentLvl + 1);
    }

    private void SpawnCharacter(int lvl)
    {
        if(_DiedCharacter.Count != 0)
        {
            AICharacter spawnCharacter = _DiedCharacter[_DiedCharacter.Count - 1];
            _DiedCharacter.Remove(spawnCharacter);
            _spawnerCharacters.Add(spawnCharacter);
            spawnCharacter.transform.position = CreateSpawnPoint();
            spawnCharacter.gameObject.SetActive(true);
            spawnCharacter.Init(lvl);
            spawnCharacter.Died += OnDieCharacter;
        }
    }

    private void OnDieCharacter(AICharacter character)
    {
        _spawnerCharacters.Remove(character);
        _DiedCharacter.Add(character);
        character.Died -= OnDieCharacter;
    }
}

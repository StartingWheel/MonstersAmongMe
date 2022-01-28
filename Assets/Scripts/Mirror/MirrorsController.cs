using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MirrorsController : MonoBehaviour
{

    [SerializeField] private GameObject mirrorPrefab; //Префаб зеркал
    [SerializeField] private Vector3[] _mirrorsPositions; // Позиции зеркал на карте
    [SerializeField] private float[] _mirrorsRotations; // Поворот зеркал
    [SerializeField] private ushort _countMirrors; //Кол-во зеркал

    private int _countPositions; //Кол-во позиций зеркал
    private List<GameObject> _mirrors = new List<GameObject>(); //Зеркала
    private List<ushort> _mirrorsTransformIndexes = new List<ushort>(); // индексы позиций зеркал

    // ================ Проверка индекса на использование ================ //
    private bool CheckUsable(ushort ind)
    {
        for (int i = 0; i < _mirrorsTransformIndexes.Count; ++i)
            if (_mirrorsTransformIndexes[i] == ind) return true;
        return false;
    }
    //=====================================================================//

    // ==================== Получение индекса позиции ==================== //
    private ushort GetPositionIndex()
    {
        ushort res = (ushort)UnityEngine.Random.Range(0, _countPositions);
        while(CheckUsable(res)) res = (ushort)UnityEngine.Random.Range(0, _countPositions);
        return res;
    }
    //=====================================================================//



    // Start is called before the first frame update
    void Start()
    {
        _countPositions = Math.Min(_mirrorsPositions.Length, _mirrorsRotations.Length);
        _countMirrors = (ushort)Math.Min(_countPositions-1, _countMirrors);

        // ------------------ Создание зеркал на карте ------------------ //
        ushort posIndex;
        for (ushort i = 0; i< _countMirrors; ++i)
        {
            posIndex =  GetPositionIndex();
            _mirrorsTransformIndexes.Add(posIndex);
            _mirrors.Add(Instantiate(mirrorPrefab) as GameObject);
            _mirrors[i].transform.position = _mirrorsPositions[posIndex];
            _mirrors[i].transform.Rotate(0, _mirrorsRotations[posIndex], 0);
        }
        //----------------------------------------------------------------//


    }

    // Update is called once per frame
    void Update()
    {
        for (ushort i = 0; i < _countMirrors; ++i)
        {
            if (_mirrors[i] == null)
            {
                Debug.Log("The window №" + i + " was destroied");
                ushort posIndex;
                posIndex = GetPositionIndex();
                _mirrorsTransformIndexes[i] = posIndex;
                _mirrors[i] = Instantiate(mirrorPrefab) as GameObject;
                _mirrors[i].transform.position = _mirrorsPositions[posIndex];
                _mirrors[i].transform.Rotate(0, _mirrorsRotations[posIndex], 0);
            }
        }
    }
}

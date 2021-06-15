using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Rules for this station type set up:
 * - Orders cannot share the same material type within the InfoAsset
 * - Once an item is placed, that order has to be filled in order to clear the smelter
 */
//Should be WorkStation.cs
public class RefineMaterialStation : BaseStation
{
    private TMPro.TMP_Text text;
    public WorkStationInfo InfoAsset;
    public List<Transform> MaterialPlacementPoints=new List<Transform>();
    private int _numOfCollectedMaterials;
    private EOrderTypes _currentOrderType;
    private WorkOrder _currentOrder;
    private List<GameObject> _collectedMaterials=new List<GameObject>();
    private bool _itemProcessed;
    private bool _itemPlaced;
    private bool _processing;
    public Transform ResultPlacement;
    [SerializeField] Slider progressBar;
    private float timeToReach; 
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        _currentOrderType = EOrderTypes.NULL;
        progressBar = GetComponentInChildren<Slider>();
        
        if (progressBar)
            progressBar.gameObject.SetActive(false);
        if(ResultPlacement == null)
        {
            ResultPlacement = MaterialPlacementPoints[0];
        }
    }

    protected override void StationAction()
    {
        if (_processing) return;
        if (!_currentPlayer.IsHoldingItem)
        {
            if (ResultPlacement.childCount > 0)
            {
                ResultPlacement.GetChild(0).GetComponent<Item>().GiveItem(_currentPlayer);
                _processing = true;
                StartCoroutine(DelayToPlaceItem());
                return;
            }
        }
        if(_itemProcessed)
        {
            _itemProcessed = false;
            return;
        }

        if(_currentPlayer.HoldItemPosition.childCount == 0) return; 
        var type = _currentPlayer.HoldItemPosition.GetChild(0).GetComponent<MaterialItem>();
        if(!type) return;
        _processing = true;

        if(_currentOrder != null)
        {
            for(int i = 0; i < _currentOrder.RequiredMaterials.Count;i++)
            {
                if(type.MaterialType == _currentOrder.RequiredMaterials[i])
                {
                    //Add item to this order
                    _currentPlayer.IsHoldingItem = false;
                    _itemPlaced = true;
                    type.gameObject.transform.position = MaterialPlacementPoints[_numOfCollectedMaterials].position;
                    type.gameObject.transform.parent = MaterialPlacementPoints[_numOfCollectedMaterials];
                    _numOfCollectedMaterials++;
                    _collectedMaterials.Add(type.gameObject);
                    if(_numOfCollectedMaterials >= _currentOrder.RequiredMaterials.Count)
                    {
                        StartCoroutine(DelayToSpawnResult());
                    }
                    break;
                }
            }
        }
        else
        {
            for(int i = 0; i < InfoAsset.Orders.Count; i++)
            {
                for(int j = 0; j < InfoAsset.Orders[i].RequiredMaterials.Count;j++)
                {
                    if(type.MaterialType == InfoAsset.Orders[i].RequiredMaterials[j])
                    {
                        //Add item to this order
                        _currentPlayer.IsHoldingItem = false;
                        _itemPlaced = true;
                        type.gameObject.transform.position = MaterialPlacementPoints[_numOfCollectedMaterials].position;
                        type.gameObject.transform.parent = MaterialPlacementPoints[_numOfCollectedMaterials];
                        _numOfCollectedMaterials++;
                        _collectedMaterials.Add(type.gameObject);
                        DataManager.MakeItRain<AudioHandler>(DataKeys.AUDIO)?.PlayAudio(EAudioEvents.PlayerDropItem);
                        if(type.MaterialType != EMaterialTypes.Metal)
                        {
                            _currentOrderType = InfoAsset.Orders[i].OrderType;
                            _currentOrder = InfoAsset.Orders[i];   
                        }
                        if(_currentOrder!=null && _numOfCollectedMaterials >= _currentOrder.RequiredMaterials.Count)
                        {
                            StartCoroutine(DelayToSpawnResult());
                        }
                        break;
                    }
                }
                if(_itemPlaced) break;
            }
        }
        
        
        //Wrong Item Placed
        if(!_itemPlaced)
        {
            StartCoroutine(DelayToReturnItem(type.gameObject));
        }
        _itemPlaced = false;
        StartCoroutine(DelayToPlaceItem());
    }

    IEnumerator DelayToPlaceItem()
    {
        yield return new WaitForSeconds(0.5f);
        _processing = false;
    }

    IEnumerator DelayToReturnItem(GameObject go)
    {
        go.transform.position = PlacementPosition.position;
        go.transform.parent = PlacementPosition;
        yield return new WaitForSeconds(1);
        DataManager.MakeItRain<AudioHandler>(DataKeys.AUDIO)?.PlayAudio(EAudioEvents.PlayerError);
        go.transform.position = _currentPlayer.HoldItemPosition.position;
        go.transform.parent = _currentPlayer.HoldItemPosition;
    }

    IEnumerator DelayToSpawnResult()
    {
        yield return new WaitForSeconds(1);
        bool isLooping = DataManager.MakeItRain<AudioHandler>(DataKeys.AUDIO).PlayAudio(InfoAsset.StationEvent);
        foreach(GameObject _gameObject in _collectedMaterials)
        {
            Destroy(_gameObject);
        }
        //I'm Sorry. But I'm tired
        float duration = InfoAsset.ProcessDuration;
        if(Animator)
        {
            Animator.SetBool("Building",true);
            duration = Animator.GetCurrentAnimatorStateInfo(0).length;
        }
        //add a UI element that will signify duration of result
        if (progressBar)
        {
            timeToReach = duration + Time.time;

            InvokeRepeating("ProgressBar", duration, 0.1f);
        }

        yield return new WaitForSeconds(duration);
        var resultPrefab = Instantiate(_currentOrder.ProcessedResultPrefab, ResultPlacement.position,
            ResultPlacement.rotation);
        resultPrefab.transform.parent = ResultPlacement;
        _numOfCollectedMaterials = 0;
        _currentOrder = null;
        if(Animator)
        {
            Animator.SetBool("Building", false);
        }
        if(isLooping)
        {
            DataManager.MakeItRain<AudioHandler>(DataKeys.AUDIO).StopLoopSource(InfoAsset.StationEvent);
        }
        DataManager.MakeItRain<ScoreHandler>(DataKeys.SCORE).AddToScore(_currentOrderType);
        _currentOrderType = EOrderTypes.NULL;
        _itemProcessed = true;
    }

    void ProgressBar()
    {
        progressBar.gameObject.SetActive(true);

        if (Time.time <= timeToReach)
        {
            //value needs to be an algorithm for the current time step to the value of the time to reach
            progressBar.value = Mathf.Clamp((timeToReach / Time.time), 0, 1);
        }
    }
}

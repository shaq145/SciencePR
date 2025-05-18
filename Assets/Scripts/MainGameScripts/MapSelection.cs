using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MapSelection : MonoBehaviour {

    public int counter;
    public TextMeshProUGUI mapNameText;
    public TextMeshProUGUI mapDescriptionText;

    public GameObject mapInformation;

    [System.Serializable]
    public class MapSelections {
        public int mapID;
        public string mapName;
        public string stageName;
        [TextArea ( 3, 2 )]
        public string mapDescription;
    }

    public List<MapSelections> mapSelections;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        mapNameText.text = mapSelections [ counter ].mapName;
        mapDescriptionText.text = mapSelections [ counter ].mapDescription;
    }

    public void MapSelect ( int id ) {
        counter = id;
        mapInformation.SetActive ( true );  
    }

    public void StartMap () {
        SceneManager.LoadSceneAsync ( mapSelections [ counter ].stageName );
    }
}

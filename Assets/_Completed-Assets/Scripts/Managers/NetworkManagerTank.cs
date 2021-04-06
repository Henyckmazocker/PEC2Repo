using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;
namespace Complete
{
    public class NetworkManagerTank : NetworkManager
    {
        // Start is called before the first frame update
        public List<Transform> spawns;
        private Vector3 newVec;
        public List<GameObject> players;
        public List<GameObject> npcs;

        public GameObject NPC;
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            GameObject pl = Instantiate(playerPrefab,spawns[numPlayers].position,spawns[numPlayers].rotation);
            players.Add(pl);
            NetworkServer.AddPlayerForConnection(conn,pl);
        }

        public override void OnStartHost()
        {
            base.OnStartHost();

            Mesh planeMesh = GameObject.Find("GroundPlane").GetComponent<MeshFilter>().mesh;
            Bounds bounds = planeMesh.bounds;

            float minX = gameObject.transform.position.x - gameObject.transform.localScale.x * bounds.size.x * 0.5f;
            float minZ = gameObject.transform.position.z - gameObject.transform.localScale.z * bounds.size.z * 0.5f;
            
            for(int i = 0; i<4;i++){

                while(Physics.CheckSphere (newVec, 0.1f) != false){
                    newVec = new Vector3(Random.Range (minX, -minX),0.2f,Random.Range (minZ, -minZ));
                }
                GameObject pl = Instantiate(NPC,newVec,Quaternion.identity);
                pl.tag = "NPC";
                npcs.Add(pl);
                NetworkServer.Spawn(pl);
            }

        }

    }
}

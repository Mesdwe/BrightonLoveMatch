// KHOGDEN
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBorders : MonoBehaviour
{
    [System.Serializable]
    public class Borders
    {
        [SerializeField][Range(0f, 6f)] float yPosTop = 5f;
        [SerializeField][Range(-6f, 0f)] float yPosBottom = -5f;

        public float YPosTop
        {
            get { return yPosTop; }
            set { yPosTop = value; }
        }

        public float YPosBottom
        {
            get { return yPosBottom; }
            set { yPosBottom = value; }
        }
    }
    [SerializeField] Borders borders = new Borders();

    private List<Transform> players = new List<Transform>();

    // KH - Called before 'void Start()'.
    private void Awake()
    {
        // KH - Add the player game objects into the 'players' list for referencing.
        Player[] plr = FindObjectsOfType<Player>();
        foreach (Player p in plr)
            players.Add(p.transform);
    }

    // Update is called once per frame
    void Update()
    {
        // KH - Prevent players from going beyond the Y position borders.
        foreach(Transform plr in players)
        {
            Vector3 pos = plr.position;

            if (plr.transform.position.y > borders.YPosTop)
                plr.transform.position = new Vector3(pos.x, borders.YPosTop, pos.z);
            else if(plr.transform.position.y < borders.YPosBottom)
                plr.transform.position = new Vector3(pos.x, borders.YPosBottom, pos.z);
        }
    }
}

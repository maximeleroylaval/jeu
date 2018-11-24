﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public float speed = 2.0f;
    public int bombLimit = 1;
    public GameObject bombPrefab;
    public Dictionary<ControlInput, string> control;
    public List<GameObject> bombs;


    private bool dead = false;
    private string pseudo = "Player";

	// Use this for initialization
	void Start () {
        ChooseCommand(0);           
	}

    public virtual bool Dead()
    {
        return this.dead;
    }

    public virtual void ChooseCommand(int nb)
    {
        if (nb == 0)
        {
            control = Controls.Control1;
        }
        if (nb == 1)
        {
            control = Controls.Control2;
        }
    }

    public virtual void Die()
    {
        string toDisplay = this.GetPseudo() + " just died like a noob";
        GameObject.Find("Canvas").GetComponent<Canvas>().GetComponent<TextAnnouncer>().Display(toDisplay);
        this.dead = true;
        this.gameObject.SetActive(false);
    }

    public virtual Vector3 GetDirection()
    {
        if (Input.GetKey(control[ControlInput.RIGHT]))
            return Vector3.right;
        else if (Input.GetKey(control[ControlInput.LEFT]))
            return Vector3.left;
        else if (Input.GetKey(control[ControlInput.UP]))
            return Vector3.forward;
        else if (Input.GetKey(control[ControlInput.DOWN]))
            return -Vector3.forward;
        return Vector3.zero;
    }

    public virtual bool CanBomb()
    {
        if (Input.GetKey(control[ControlInput.BOMB]))
            return true;
        return false;
    }

    [Command]
    public virtual void CmdBomb()
    {
        if (this.bombs.Count < this.bombLimit)
        {
            GameObject bomb = Instantiate(bombPrefab, new Vector3(this.transform.position.x - 1.14f, 0.087f, this.transform.position.z), Quaternion.identity);
            bomb.transform.parent = transform.parent;
            this.bombs.Add(bomb);
            NetworkServer.Spawn(bomb);
        }

        for (int i = this.bombs.Count - 1; i >= 0; i--)
        {
            if (this.bombs[i] == null)
                this.bombs.RemoveAt(i);
        }
    }

    public virtual void Move()
    {
        Vector3 direction = this.GetDirection();
        if (direction != Vector3.zero)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
            return;

        this.Move();

        if (this.CanBomb())
            this.CmdBomb();
    }

    public string GetPseudo()
    {
        return this.pseudo;
    }

    public void SetPseudo(string name)
    {
        this.pseudo = name;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Explosive")
            this.Die();
    }
}
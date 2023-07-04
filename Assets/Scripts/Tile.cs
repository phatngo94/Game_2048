using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public Image backgroundColor;
    public TextMeshProUGUI textColor;
    public int number;
    public bool locked { get; set; }

    private void Awake()
    {
        backgroundColor = GetComponent<Image>();
        textColor = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTileCell(TileState tileState,int number)
    {
        backgroundColor.color = tileState.backgroundColor;
        textColor.color = tileState.textColor;
        textColor.text = number.ToString();
    }

    public void SetState(TileState state, int number)
    {
        this.state = state;
        this.number = number;

        backgroundColor.color = state.backgroundColor;
        textColor.color = state.textColor;
        textColor.text = number.ToString();
    }

    public void Spaw(TileCell cell)
    {
        if(this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;
        transform.position = cell.transform.position;
    }

    public void MoveTo(TileCell cell)
    {
        if(this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;
        //.position = cell.transform.position;

        StartCoroutine(Animate(cell.transform.position,false));
    }

    public void Merge(TileCell cell)
    {
        if (this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = null;
        cell.tile.locked = true;
        StartCoroutine(Animate(cell.transform.position,true));
    }

    private IEnumerator Animate(Vector3 to,bool merging)
    {
        float elapsed = 0f;
        float duration = 0.1f;

        Vector3 from = transform.position;
        while(elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;

        if (merging)
        {
            Destroy(gameObject);
        }
    }
}

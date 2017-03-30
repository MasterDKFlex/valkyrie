using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Content;

public class EditorComponentActivation : EditorComponent
{
    QuestData.Activation activationComponent;
    DialogBoxEditable abilityDBE;
    DialogBoxEditable moveButtonDBE;
    DialogBoxEditable masterActionsDBE;
    DialogBoxEditable minionActionsDBE;
    DialogBoxEditable moveDBE;

    public EditorComponentActivation(string nameIn) : base()
    {
        Game game = Game.Get();
        activationComponent = game.quest.qd.components[nameIn] as QuestData.Activation;
        component = activationComponent;
        name = component.sectionName;
        Update();
    }
    
    override public void Update()
    {
        base.Update();
        Game game = Game.Get();

        TextButton tb = new TextButton(new Vector2(0, 0), new Vector2(4, 1), 
            CommonStringKeys.ACTIVATION, delegate { QuestEditorData.TypeSelect(); });
        tb.button.GetComponent<UnityEngine.UI.Text>().fontSize = UIScaler.GetSmallFont();
        tb.button.GetComponent<UnityEngine.UI.Text>().alignment = TextAnchor.MiddleRight;
        tb.ApplyTag("editor");

        tb = new TextButton(new Vector2(4, 0), new Vector2(15, 1), 
            new StringKey(name.Substring("Activation".Length),false), 
            delegate { QuestEditorData.ListActivation(); });
        tb.button.GetComponent<UnityEngine.UI.Text>().fontSize = UIScaler.GetSmallFont();
        tb.button.GetComponent<UnityEngine.UI.Text>().alignment = TextAnchor.MiddleLeft;
        tb.ApplyTag("editor");

        tb = new TextButton(new Vector2(19, 0), new Vector2(1, 1), CommonStringKeys.E, delegate { Rename(); });
        tb.button.GetComponent<UnityEngine.UI.Text>().fontSize = UIScaler.GetSmallFont();
        tb.ApplyTag("editor");

        if (game.gameType is MoMGameType)
        {
            MoMActivation();
        }
        else
        {
            Activation();
        }
    }

    public void Activation()
    {
        DialogBox db = new DialogBox(new Vector2(0, 1), new Vector2(20, 1), "Ability:");
        db.ApplyTag("editor");

        abilityDBE = new DialogBoxEditable(new Vector2(0, 2), new Vector2(20, 8), activationComponent.ability.Translate(), delegate { UpdateAbility(); });
        abilityDBE.ApplyTag("editor");
        abilityDBE.AddBorder();

        db = new DialogBox(new Vector2(0, 10), new Vector2(15, 1), "Master:");
        db.ApplyTag("editor");
        TextButton tb = null;
        if (activationComponent.masterFirst)
        {
            tb = new TextButton(new Vector2(15, 10), new Vector2(5, 1), "First", delegate { ToggleMasterFirst(); });
        }
        else
        {
            tb = new TextButton(new Vector2(15, 10), new Vector2(5, 1), "Not First", delegate { ToggleMasterFirst(); });
        }
        tb.button.GetComponent<UnityEngine.UI.Text>().fontSize = UIScaler.GetSmallFont();
        tb.ApplyTag("editor");

        masterActionsDBE = new DialogBoxEditable(new Vector2(0, 11), new Vector2(20, 8), activationComponent.masterActions.Translate(), delegate { UpdateMasterActions(); });
        masterActionsDBE.ApplyTag("editor");
        masterActionsDBE.AddBorder();

        db = new DialogBox(new Vector2(0, 19), new Vector2(15, 1), "Minion:");
        db.ApplyTag("editor");
        if (activationComponent.minionFirst)
        {
            tb = new TextButton(new Vector2(15, 19), new Vector2(5, 1), "First", delegate { ToggleMinionFirst(); });
        }
        else
        {
            tb = new TextButton(new Vector2(15, 19), new Vector2(5, 1), "Not First", delegate { ToggleMinionFirst(); });
        }
        tb.button.GetComponent<UnityEngine.UI.Text>().fontSize = UIScaler.GetSmallFont();
        tb.ApplyTag("editor");

        minionActionsDBE = new DialogBoxEditable(new Vector2(0, 20), new Vector2(20, 8), activationComponent.minionActions.Translate(), delegate { UpdateMinionActions(); });
        minionActionsDBE.ApplyTag("editor");
        minionActionsDBE.AddBorder();
    }

    public void MoMActivation()
    {
        DialogBox db = new DialogBox(new Vector2(0, 1), new Vector2(20, 1), "Initial Message:");
        db.ApplyTag("editor");

        abilityDBE = new DialogBoxEditable(new Vector2(0, 2), new Vector2(20, 8), activationComponent.ability.Translate(), delegate { UpdateAbility(); });
        abilityDBE.ApplyTag("editor");
        abilityDBE.AddBorder();

        db = new DialogBox(new Vector2(0, 10), new Vector2(10, 1), "Unable Button:");

        moveButtonDBE = new DialogBoxEditable(new Vector2(10, 10), new Vector2(10, 1), activationComponent.moveButton.Translate(), delegate { UpdateMoveButton(); });
        moveButtonDBE.ApplyTag("editor");
        moveButtonDBE.AddBorder();

        db = new DialogBox(new Vector2(0, 11), new Vector2(20, 1), "Attack Message:");
        db.ApplyTag("editor");

        masterActionsDBE = new DialogBoxEditable(new Vector2(0, 12), new Vector2(20, 8), activationComponent.masterActions.Translate(), delegate { UpdateMasterActions(); });
        masterActionsDBE.ApplyTag("editor");
        masterActionsDBE.AddBorder();

        db = new DialogBox(new Vector2(0, 20), new Vector2(20, 1), "No Attack Message:");
        db.ApplyTag("editor");

        moveDBE = new DialogBoxEditable(new Vector2(0, 21), new Vector2(20, 8), activationComponent.move.Translate(), delegate { UpdateMove(); });
        moveDBE.ApplyTag("editor");
        moveDBE.AddBorder();
    }

    public void UpdateAbility()
    {
        if (!abilityDBE.uiInput.text.Equals(""))
        {
            activationComponent.ability = new StringKey(abilityDBE.uiInput.text);
        }
    }

    public void UpdateMoveButton()
    {
        if (!moveButtonDBE.uiInput.text.Equals(""))
        {
            activationComponent.moveButton = new StringKey(moveButtonDBE.uiInput.text);
        }
    }

    public void UpdateMasterActions()
    {
        if (!masterActionsDBE.uiInput.text.Equals(""))
        {
            activationComponent.masterActions = new StringKey(masterActionsDBE.uiInput.text);
        }
    }

    public void UpdateMinionActions()
    {
        if (!minionActionsDBE.uiInput.text.Equals(""))
        {
            activationComponent.minionActions = new StringKey(minionActionsDBE.uiInput.text);
        }
    }

    public void UpdateMove()
    {
        if (!moveDBE.uiInput.text.Equals(""))
        {
            activationComponent.move = new StringKey(moveDBE.uiInput.text);
        }
    }

    public void ToggleMasterFirst()
    {
        activationComponent.masterFirst = !activationComponent.masterFirst;
        Update();
    }

    public void ToggleMinionFirst()
    {
        activationComponent.minionFirst = !activationComponent.minionFirst;
        Update();
    }
}

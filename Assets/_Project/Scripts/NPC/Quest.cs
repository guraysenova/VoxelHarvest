using System;
using System.Collections.Generic;

[Serializable]
public class Quest
{
    public string ID;
    public string Title;
    public string Description;
    public string[] StartDialogue;
    public string[] EndDialogue;
    public List<Reward> Rewards;
    public List<Task> Tasks;
}
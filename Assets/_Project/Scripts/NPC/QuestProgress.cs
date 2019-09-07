public enum QuestProgress
{
    Finished    = 1, // Completed the quest then talked with the NPC one last time for that quest
    Completed   = 2, // Completed when all the tasks are done but not talked with the NPC yet
    InProgress  = 3, // Quest is taken
    Available   = 4, // Quest is available to be taken
    UnAvailable = 5  // Quest needs some prerequisites to be taken
}
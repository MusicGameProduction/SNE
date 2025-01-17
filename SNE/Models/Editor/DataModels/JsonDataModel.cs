﻿using System.Collections.Generic;

namespace SNE.Models.Editor.DataModels
{
    public class JsonDataModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BPM { get; set; }
        public string GUID { get; set; }
        public List<NoteDataModel> NotesData { get; set; }
    }

    public class NoteDataModel
    {
        public double Time { get; set; }
        public int LaneID { get; set; }
        public bool IsActionNote { get; set; }
        public int DifficultyLevel { get; set; }
    }
}

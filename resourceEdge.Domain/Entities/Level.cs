﻿using System;

namespace resourceEdge.Domain.Entities
{
    public class Level
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int levelNo { get; set; }
        public string LevelName { get; set; }
        public int EligibleYears { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Group Group { get; set; }

    }
}
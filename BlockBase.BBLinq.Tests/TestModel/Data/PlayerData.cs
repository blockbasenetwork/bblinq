using System;
using System.Collections.Generic;
using System.Text;

namespace BlockBase.BBLinq.Tests.TestModel.Data
{
    public class PlayerData
    {
        public Guid PlayerId { get; set; }
        public string CountryName { get; set; }
        public string TeamName { get; set; }
        public int TeamWebsite { get; set; }
        public string PlayerName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

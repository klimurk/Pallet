﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Pallet.DatabaseGenerated
{
    public partial class AlarmLog
    {
        public long Id { get; set; }
        public DateTime Timestamp1 { get; set; }
        public DateTime? Timestamp2 { get; set; }
        public bool Gone { get; set; }
        public int AlmNr { get; set; }
        public string AlmName { get; set; }
        public string Device { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Prio { get; set; }
        public string Stype { get; set; }
        public string AlmAddress { get; set; }
        public bool Inverted { get; set; }
    }
}
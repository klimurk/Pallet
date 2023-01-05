using Pallet.ExternalDatabase.Models.NotMapped;
using Pallet.ExternalDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pallet.Services.Models;

public class Profile
{
    public PackageItem? Task { get; internal set; }

    public IQueryable<NailingData> Nails { get; internal set; }

    public IQueryable<WoodenPart> Parts { get; internal set; }

    public RobotTaskItem RobotTaskItem { get; internal set; }
    public Verpackung CrateCharacteristics { get; internal set; }
    public Auftrag Contract { get; internal set; }
    public Firma Firm { get; internal set;  }


}

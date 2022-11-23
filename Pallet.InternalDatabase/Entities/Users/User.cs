using Pallet.BaseDatabase.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pallet.InternalDatabase.Entities.Users;

[Table("PROG_USER")]
public class User : NamedEntity
{
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new int ID { get; set; }

    [Required]
    [Column("USER_NAME", Order = 1, TypeName = "varchar(50)")]
    public new string Name
    {
        get => _Name; set
        {
            if (_Name != value)
            {
                _Name = value;
                OnUserChanged();
            }
        }
    }

    private string _Name;

    [Column("USER_DESC", TypeName = "nvarchar(200)")]
    public string? Description
    {
        get => _Description; set
        {
            if (_Description != value)
            {
                _Description = value;
                OnUserChanged();
            }
        }
    }

    private string _Description;

    [Column("USER_ROLE")]
    public int RoleNum
    {
        get => _RoleNum; set
        {
            if (_RoleNum != value)
            {
                _RoleNum = value;
                OnUserChanged();
            }
        }
    }

    private int _RoleNum;

    [Column("USER_HASH", TypeName = "char(64)")]
    public string Hashcode
    {
        get => _Hashcode; set
        {
            if (_Hashcode != value)
            {
                _Hashcode = value;
                OnUserChanged();
            }
        }
    }

    private string _Hashcode;

    [Column("Registration Date")]
    public DateTime RegistrationTime { get; set; }

    [Column("Admin registered")]
    public string AdminRegisteredName { get; set; }

    public event EventHandler UserChanged;

    private void OnUserChanged() => UserChanged?.Invoke(this, new());
}
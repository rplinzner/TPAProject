using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DataModel;

namespace DBData.DBMetadata
{
    [Table("MethodMetadata")]
    public class DBMethodMetadata : BaseMethodMetadata
    {
        #region Constructor

        public DBMethodMetadata()
        {
            GenericArguments = new List<DBTypeMetadata>();
            Parameters = new List<DBParameterMetadata>();
            TypeConstructors = new HashSet<DBTypeMetadata>();
            TypeMethods = new HashSet<DBTypeMetadata>();
        }

        #endregion

        #region Propeties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public override string Name { get; set; }

        public override bool Extension { get; set; }
        public override Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        public new DBTypeMetadata ReturnType { get; set; }
        public new List<DBTypeMetadata> GenericArguments { get; set; }
        public new List<DBParameterMetadata> Parameters { get; set; }

        #endregion

        #region Inverse Properties

        public virtual ICollection<DBTypeMetadata> TypeConstructors { get; set; }

        public virtual ICollection<DBTypeMetadata> TypeMethods { get; set; }

        #endregion
    }
}
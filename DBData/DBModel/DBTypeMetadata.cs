using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DataModel;

namespace DBData.DBModel
{
    public class DBTypeMetadata : BaseTypeMetadata
    {
        #region Constructor

        public DBTypeMetadata()
        {
            MethodGenericArguments = new HashSet<DBMethodMetadata>();
            TypeGenericArguments = new HashSet<DBTypeMetadata>();
            TypeImplementedInterfaces = new HashSet<DBTypeMetadata>();
            TypeNestedTypes = new HashSet<DBTypeMetadata>();
            Constructors = new List<DBMethodMetadata>();
            Fields = new List<DBParameterMetadata>();
            GenericArguments = new List<DBTypeMetadata>();
            ImplementedInterfaces = new List<DBTypeMetadata>();
            Methods = new List<DBMethodMetadata>();
            NestedTypes = new List<DBTypeMetadata>();
            Properties = new List<DBPropertyMetadata>();

        }

        #endregion

        #region Properties

        [Key, StringLength(150)]
        public override string Name { get; set; }

        public new DBTypeMetadata BaseType { get; set; }

        public override TypeEnum Type { get; set; }
        public new DBTypeMetadata DeclaringType { get; set; }

        public override Tuple<AccessLevel, SealedEnum, AbstractEnum, StaticEnum> Modifiers { get; set; }

        public int? NamespaceId { get; set; }

        public new List<DBMethodMetadata> Constructors { get; set; }

        public new List<DBParameterMetadata> Fields { get; set; }

        public new List<DBTypeMetadata> GenericArguments { get; set; }

        public new List<DBTypeMetadata> ImplementedInterfaces { get; set; }

        public new List<DBMethodMetadata> Methods { get; set; }

        public new List<DBTypeMetadata> NestedTypes { get; set; }

        public new List<DBPropertyMetadata> Properties { get; set; }

        #endregion

        #region Inverse Properties

        [InverseProperty("BaseType")]
        public virtual ICollection<DBTypeMetadata> TypeBaseTypes { get; set; }

        [InverseProperty("DeclaringType")]
        public virtual ICollection<DBTypeMetadata> TypeDeclaringTypes { get; set; }

        public virtual ICollection<DBMethodMetadata> MethodGenericArguments { get; set; }

        public virtual ICollection<DBTypeMetadata> TypeGenericArguments { get; set; }

        public virtual ICollection<DBTypeMetadata> TypeImplementedInterfaces { get; set; }

        public virtual ICollection<DBTypeMetadata> TypeNestedTypes { get; set; }

        #endregion

    }
}
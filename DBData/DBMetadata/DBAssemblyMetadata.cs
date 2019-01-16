using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DataModel;

namespace DBData.DBMetadata
{
    [Table("AssemblyMetadata")]
    [Export(typeof(DBAssemblyMetadata))]
    public class DBAssemblyMetadata : BaseAssemblyMetadata
    {
        #region Properties

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public override string Name { get; set; }

        public new List<DBNamespaceMetadata> Namespaces { get; set; }

        #endregion

    }
}


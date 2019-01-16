using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.DataModel;

namespace DBData.DBMetadata
{
    [Table("NamespaceMetadata")]
    public class DBNamespaceMetadata : BaseNamespaceMetadata
    {
        #region Properties

        public int Id { get; set; }

        [Required, StringLength(150)]
        public override string Name { get; set; }

        public new List<DBTypeMetadata> Types { get; set; }

        #endregion
    }
}
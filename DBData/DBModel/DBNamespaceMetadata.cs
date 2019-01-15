using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.DataModel;

namespace DBData.DBModel
{
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